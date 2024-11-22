using System;
using UnityEngine;

public class BombCube : GameCube
{
	[SerializeField][Range(1, 5)] private float _destroyCubesRadius = 2.0f;
	[SerializeField][Range(1, 5)] private float _explosionRadius = 3.0f;

	[SerializeField][Range(100, 3000)] private float _explosionForce = 700.0f;

	public static event Action Exploded;
	public static event Action<CubeNumber.Index> CubeDestroyedByExplosionWithIndex;

	private void OnEnable()
	{
		Playground.NewGame += DestroyThisCube;
	}

	private void OnDisable()
	{
		Playground.NewGame -= DestroyThisCube;
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.TryGetComponent(out MergeableCube otherCube) ||
			collision.gameObject.TryGetComponent(out TableBorder border))
		{
			Collider[] cubesToDestroy = Physics.OverlapSphere(transform.position, _destroyCubesRadius);
			Collider[] cubesToAddForce = Physics.OverlapSphere(transform.position, _explosionRadius);

			ParticleEffectSystem.Instance.PlayExplodeEffect(transform.position, transform.rotation);

			DestroyNearByCubes(cubesToDestroy);
			AddExplodeForceToCubes(cubesToAddForce);

			DestroyThisCube();

			Exploded?.Invoke();
		}
	}

	private void DestroyNearByCubes(Collider[] colliders)
	{
		foreach (Collider collider in colliders)
		{
			if (collider.TryGetComponent(out MergeableCube cube))
			{
				CubesPool.Destroy(cube);
				CubeDestroyedByExplosionWithIndex?.Invoke(cube.Index);
			}
		}
	}

	private void AddExplodeForceToCubes(Collider[] colliders)
	{
		foreach (Collider collider in colliders)
		{
			if (collider.TryGetComponent(out Rigidbody rb))
			{
				rb.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
			}
		}
	}

	private void DestroyThisCube()
	{
		Destroy(gameObject);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, _destroyCubesRadius);
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, _explosionRadius);
	}
}
