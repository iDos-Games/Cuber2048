using UnityEngine;

public class MergeableCube : GameCube
{
	public CubeNumber.Index Index;

	public bool CanMerge { get; set; }

	private const float RIGIDBODY_FORCE = 500f;
	private const float RIGIDBODY_TORQUE_FORCE = 50f;
	private const float RIGIDBODY_DIRECTION_FORCE_REDUCTION_COEFFICIENT = 0.25f;

	private const float AFTER_SPAWN_MERGE_DELAY = 0.075f;
	private const float COLLIDER_SCALE_ON_CANT_MERGEABLE = 1.25f;

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.TryGetComponent(out MergeableCube otherCube))
		{
			if (!CanMerge || !otherCube.CanMerge)
			{
				return;
			}

			if (otherCube.Index != Index)
			{
				return;
			}

			CanMerge = false;
			otherCube.CanMerge = false;

			CubesPool.Destroy(otherCube);
			CubesPool.Destroy(this);

			Vector3 position = collision.contacts[0].point;

			GameCubeSpawner.Instance.SpawnAfterMerge(Index, position, Quaternion.identity);
		}
	}

	public void ForcingUp()
	{
		SetActiveMergeableness(false);

		Rigidbody.linearVelocity = Vector3.zero;

		var nearestSameIndexCubePosition = CubesPool.GetNearestMergeableCubePosition(this);

		if (nearestSameIndexCubePosition == Vector3.zero)
		{
			Rigidbody.AddForce((Vector3.up + Vector3.forward * RIGIDBODY_DIRECTION_FORCE_REDUCTION_COEFFICIENT) * RIGIDBODY_FORCE);
			Rigidbody.AddTorque(Vector3.up * RIGIDBODY_TORQUE_FORCE);
		}
		else
		{
			Vector3 startPos = nearestSameIndexCubePosition;
			Vector3 endPos = new(transform.position.x, startPos.y, transform.position.z);
			Vector3 flyingDir = (startPos - endPos).normalized;

			Rigidbody.AddForce((Vector3.up + flyingDir * RIGIDBODY_DIRECTION_FORCE_REDUCTION_COEFFICIENT) * RIGIDBODY_FORCE);
			Rigidbody.AddTorque((Vector3.up + flyingDir * RIGIDBODY_DIRECTION_FORCE_REDUCTION_COEFFICIENT) * RIGIDBODY_TORQUE_FORCE);
		}

		Invoke(nameof(MakeCubeMergeable), AFTER_SPAWN_MERGE_DELAY);
	}

	private void MakeCubeMergeable()
	{
		SetActiveMergeableness(true);
	}

	public void SetActiveMergeableness(bool canMerge)
	{
		CanMerge = canMerge;

		if (canMerge)
		{
			BoxCollider.size = Vector3.one;
		}
		else
		{
			BoxCollider.size = Vector3.one * COLLIDER_SCALE_ON_CANT_MERGEABLE;
		}
	}
}
