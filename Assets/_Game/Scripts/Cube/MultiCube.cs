using UnityEngine;

public class MultiCube : GameCube
{
	private bool _canMerge;

	private void OnEnable()
	{
		_canMerge = true;
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (!_canMerge)
		{
			return;
		}

		if (collision.gameObject.TryGetComponent(out TableBorder _))
		{
			DestroyThisCube();
			return;
		}

		if (collision.gameObject.TryGetComponent(out MergeableCube otherCube))
		{
			_canMerge = false;

			otherCube.CanMerge = false;

			CubesPool.Destroy(otherCube);

			Vector3 position = collision.contacts[0].point;

			GameCubeSpawner.Instance.SpawnAfterMerge(otherCube.Index, position, Quaternion.identity);

			DestroyThisCube();
		}
	}

	private void DestroyThisCube()
	{
		ParticleEffectSystem.Instance.PlayExplodeRainbowEffect(transform.position, transform.rotation);
		Destroy(gameObject);
	}
}
