using System;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
	private Camera _mainCamera;

	private const float ENABLE_SHOOTABLENESS_DELAY = 0.01f;
	private const float CUBE_MOVE_LIMIT = 1.9f;
	private const float CUBE_MOVE_MULTIPLIER = 1.5f;
	private const int SHOOT_FORCE = 30;
	private const int CAMERA_DEPTH_POSITION_Z = 10;

	private float _positionX;

	private static bool _canShoot = false;

	private GameCube _currentCube = null;

	public static event Action Shooted;
	public static event Action BombShooted;
	public static event Action MulticubeShooted;

	private void Awake()
	{
		_mainCamera = Camera.main;
	}

	private void OnEnable()
	{
		GameCubeSpawner.NewGameCubeSpawned += SetTargetCube;
		GameState.Paused += DisableShootableness;
		GameState.Unpaused += EnableShootablenessAfterDelay;
		MenuButton._pointerDown += DisableShootableness;
		MenuButton._pointerUp += EnableShootablenessAfterDelay;
	}

	private void OnDisable()
	{
		GameCubeSpawner.NewGameCubeSpawned -= SetTargetCube;
		GameState.Paused -= DisableShootableness;
		GameState.Unpaused -= EnableShootablenessAfterDelay;
		MenuButton._pointerDown -= DisableShootableness;
		MenuButton._pointerUp -= EnableShootablenessAfterDelay;
	}

	private void Update()
	{
		if (!_canShoot)
		{
			return;
		}

		if (_currentCube == null)
		{
			return;
		}

		if (Input.GetMouseButtonDown(0))
		{
			_positionX = GetCurrentMouseXPosition();
		}

		if (Input.GetMouseButton(0))
		{
			float currentPositionX = GetCurrentMouseXPosition();

			float distanceX = currentPositionX - _positionX;
			Vector3 newPos = _currentCube.transform.position + new Vector3(distanceX, 0, 0) * CUBE_MOVE_MULTIPLIER;
			newPos.x = Mathf.Clamp(newPos.x, -CUBE_MOVE_LIMIT, CUBE_MOVE_LIMIT);
			_currentCube.transform.position = newPos;
			_positionX = currentPositionX;
		}

		if (Input.GetMouseButtonUp(0))
		{
			Shoot();
		}
	}

	public static void DisableShootableness()
	{
		_canShoot = false;
	}

	private void EnableShootableness()
	{
		_canShoot = true;
	}

	private void EnableShootablenessAfterDelay()
	{
		Invoke(nameof(EnableShootableness), ENABLE_SHOOTABLENESS_DELAY);
	}

	private float GetCurrentMouseXPosition()
	{
		return _mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, CAMERA_DEPTH_POSITION_Z)).x;
	}

	public void SetTargetCube(GameCube cube)
	{
		_currentCube = cube;
		EnableShootableness();
	}

	private void Shoot()
	{
		_currentCube.Rigidbody.linearVelocity = Vector3.forward * SHOOT_FORCE;

		_currentCube.View.SetActiveAim(false);

		DisableShootableness();

		Shooted?.Invoke();

		if (_currentCube is BombCube)
		{
			BombShooted?.Invoke();
		}

		if (_currentCube is MultiCube)
		{
			MulticubeShooted?.Invoke();
		}

		_currentCube = null;
	}
}
