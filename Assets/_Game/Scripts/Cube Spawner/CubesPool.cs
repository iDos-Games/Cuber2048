using System;
using System.Collections.Generic;
using UnityEngine;

public class CubesPool : MonoBehaviour
{
	[Range(5, 200)]
	[SerializeField] private int _size = 50;
	[SerializeField] private MergeableCube _cubePrefab;

	private static CubesPool _instance;

	private static readonly Queue<MergeableCube> _pool = new();

	private static readonly List<MergeableCube> _activeCubes = new();

	public static List<MergeableCube> ActiveCubes => _activeCubes;

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

		InitializePool();
	}

	private void InitializePool()
	{
		for (int i = 0; i < _size; i++)
		{
			AddNewObjectToPool();
		}
	}

	private void AddNewObjectToPool()
	{
		var cube = Instantiate(_cubePrefab, transform);
		cube.gameObject.SetActive(false);
		_pool.Enqueue(cube);
	}

	public static MergeableCube Instantiate(Vector3 position, Quaternion rotation)
	{
		if (_instance == null)
		{
			return null;
		}

		if (_pool.Count == 0)
		{
			_instance.AddNewObjectToPool();
		}

		while (_pool.Peek().gameObject.activeSelf)
		{
			_pool.Dequeue();
		}

		var cube = _pool.Dequeue();

		cube.transform.SetPositionAndRotation(position, rotation);
		cube.gameObject.SetActive(true);

		_activeCubes.Add(cube);

		return cube;
	}

	public static void Destroy(MergeableCube cube, bool needToRemoveFromActiveCubesList = true)
	{
		if (_instance == null)
		{
			return;
		}

		if (cube == null)
		{
			return;
		}

		cube.gameObject.SetActive(false);
		_pool.Enqueue(cube);

		if (needToRemoveFromActiveCubesList)
		{
			ActiveCubes.Remove(cube);
		}
	}

	public static Vector3 GetNearestMergeableCubePosition(MergeableCube cube)
	{
		Vector3 nearestPosition = Vector3.zero;

		float nearestDistance = float.MaxValue;

		foreach (var activeCube in ActiveCubes)
		{
			if (activeCube.Index != cube.Index)
			{
				continue;
			}

			if (activeCube == cube)
			{
				continue;
			}

			if (GameCubeSpawner.CurrentGameCube == activeCube)
			{
				continue;
			}

			float distanceToCube = Vector3.Distance(activeCube.transform.position, cube.transform.position);

			if (nearestDistance > distanceToCube)
			{
				nearestDistance = distanceToCube;
				nearestPosition = activeCube.transform.position;
			}
		}

		return nearestPosition;
	}

	public static void DestroyAllActiveCubes()
	{
		foreach (var cube in ActiveCubes)
		{
			Destroy(cube, false);
		}

		ActiveCubes.Clear();
	}
}
