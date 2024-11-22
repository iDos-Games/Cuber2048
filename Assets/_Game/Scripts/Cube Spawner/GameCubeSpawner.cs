using System;
using System.Collections;
using UnityEngine;

public class GameCubeSpawner : MonoBehaviour
{
	private static GameCubeSpawner _instance;
	public static GameCubeSpawner Instance => _instance;

	[SerializeField] private CubeSpawnPosition _spawnPosition;

	[SerializeField, Range(0.1f, 1.0f)] private float _spawnDelay = 0.1f;
	private const float SPAWN_ANIMATION_SPEED = 0.21f;

	public static event Action NewCubeSpawned;
	public static event Action<GameCube> NewGameCubeSpawned;
	public static event Action NewCubeSpawnedAfterMerge;
	public static event Action<CubeNumber.Index> NewCubeSpawnedAfterMergeWithIndex;

	[SerializeField] private SkinData _skinData;
	[SerializeField] private GameCube _bombCube;
	[SerializeField] private GameCube _multiCube;

	private static GameCube _currentGameCube;
	public static GameCube CurrentGameCube => _currentGameCube;

	private bool _canSpawnNewCube = true;

	private CubeNumber.Index _cubeIndexBeforeSpecialCube = CubeNumber.MIN;

	private void Awake()
	{
		if (_instance == null || ReferenceEquals(this, _instance))
		{
			_instance = this;
		}
		else
		{
			Destroy(this);
		}
	}

	private void OnEnable()
	{
		PlayerControl.Shooted += SpawnRandomMergeableCube;
		Playground.NewGame += OnNewGame;
		Playground.GameOver += OnGameOver;
	}

	private void OnDisable()
	{
		PlayerControl.Shooted -= SpawnRandomMergeableCube;
		Playground.NewGame += OnNewGame;
		Playground.GameOver -= OnGameOver;
	}

	private void Start()
	{
		SpawnRandomMergeableCube();
	}

	public void SpawnRandomMergeableCube()
	{
		if (!_canSpawnNewCube)
		{
			return;
		}

		StartCoroutine(StartSpawnMergeableCube(CubeNumber.GetRandomShootableIndex()));
	}

	public void SpawnMergeableCube(CubeNumber.Index index)
	{
		if (!_canSpawnNewCube)
		{
			return;
		}

		StartCoroutine(StartSpawnMergeableCube(index));
	}

	public void SpawnAfterMerge(CubeNumber.Index index, Vector3 position, Quaternion rotation)
	{
		var cube = CubesPool.Instantiate(position, rotation);

		cube.Index = CubeNumber.GetNextIndex(index);

		SetMergeableCubeView(cube);

		cube.SetToReadyState(false);

		ParticleEffectSystem.Instance.PlayMergeEffect(position, rotation);
		cube.ForcingUp();

		NewCubeSpawnedAfterMergeWithIndex?.Invoke(cube.Index);
		NewCubeSpawnedAfterMerge?.Invoke();
	}

	public void SpawnSavedMergeableCube(CubeNumber.Index index, Vector3 position, Quaternion rotation)
	{
		var cube = CubesPool.Instantiate(position, rotation);

		cube.Index = index;
		SetMergeableCubeView(cube);

		cube.SetToReadyState(false);
		cube.SetActiveMergeableness(true);
	}

	public void SpawnBombOrDestroyIfSpawned()
	{
		if (!_canSpawnNewCube)
		{
			return;
		}

		if (CurrentGameCube is BombCube)
		{
			DestroyCurrentGameCube();
			SpawnMergeableCube(_cubeIndexBeforeSpecialCube);
		}
		else
		{
			if (CurrentGameCube is MergeableCube mergeableCube)
			{
				_cubeIndexBeforeSpecialCube = mergeableCube.Index;
			}

			DestroyCurrentGameCube();
			StartCoroutine(nameof(StartSpawnBomb));
		}
	}

	public void SpawnMulticubeOrDestroyIfSpawned()
	{
		if (!_canSpawnNewCube)
		{
			return;
		}

		if (CurrentGameCube is MultiCube)
		{
			DestroyCurrentGameCube();
			SpawnMergeableCube(_cubeIndexBeforeSpecialCube);
		}
		else
		{
			if (CurrentGameCube is MergeableCube mergeableCube)
			{
				_cubeIndexBeforeSpecialCube = mergeableCube.Index;
			}

			DestroyCurrentGameCube();
			StartCoroutine(nameof(StartSpawnMulticube));
		}
	}

	private void OnNewGame()
	{
		CubesPool.DestroyAllActiveCubes();
		DestroyCurrentGameCube();

		_canSpawnNewCube = true;
		SpawnRandomMergeableCube();
	}

	private void OnGameOver()
	{
		_canSpawnNewCube = false;
		StopAllCoroutines();
		DestroyCurrentGameCube();
	}

	private IEnumerator StartSpawnMergeableCube(CubeNumber.Index cubeIndex)
	{
		_canSpawnNewCube = false;

		yield return new WaitForEndOfFrame();
		yield return new WaitForSeconds(_spawnDelay);

		var cube = CubesPool.Instantiate(_spawnPosition.transform.position, _spawnPosition.transform.rotation);

		_currentGameCube = cube;

		cube.SetActiveMergeableness(false);

		cube.Index = cubeIndex;

		SetMergeableCubeView(cube);

		yield return AnimateSpawnOfGameCube(cube);

		cube.SetActiveMergeableness(true);
		_canSpawnNewCube = true;

		NewCubeSpawned?.Invoke();
		NewGameCubeSpawned?.Invoke(cube);
	}

	private IEnumerator StartSpawnBomb()
	{
		_canSpawnNewCube = false;

		var bomb = Instantiate(_bombCube, _spawnPosition.transform.position, _spawnPosition.transform.rotation);

		_currentGameCube = bomb;

		yield return AnimateSpawnOfGameCube(bomb);

		_canSpawnNewCube = true;

		NewCubeSpawned?.Invoke();
		NewGameCubeSpawned?.Invoke(bomb);
	}

	private IEnumerator StartSpawnMulticube()
	{
		_canSpawnNewCube = false;

		var multicube = Instantiate(_multiCube, _spawnPosition.transform.position, _spawnPosition.transform.rotation);

		_currentGameCube = multicube;

		yield return AnimateSpawnOfGameCube(multicube);

		_canSpawnNewCube = true;

		NewCubeSpawned?.Invoke();
		NewGameCubeSpawned?.Invoke(multicube);
	}

	private IEnumerator AnimateSpawnOfGameCube(GameCube cube)
	{
		cube.SetToUnreadyState();

		while (cube.transform.localScale.x < 1)
		{
			cube.transform.localScale += Vector3.one * SPAWN_ANIMATION_SPEED;
			yield return new WaitForFixedUpdate();
		}

		cube.SetToReadyState(true);
	}

	private void DestroyCurrentGameCube()
	{
		if (_currentGameCube == null)
		{
			return;
		}

		if (_currentGameCube is MergeableCube mergeableCube)
		{
			CubesPool.Destroy(mergeableCube);
		}
		else
		{
			Destroy(_currentGameCube.gameObject);
		}

		_currentGameCube = null;
	}

	private void SetMergeableCubeView(MergeableCube cube)
	{
		cube.View.SetMaterials(_skinData.GetCubeMaterials(cube.Index));
		cube.View.SetFaces(cube.Index);
	}
}
