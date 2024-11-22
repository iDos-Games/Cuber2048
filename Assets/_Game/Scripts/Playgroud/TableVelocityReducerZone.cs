using UnityEngine;

public class TableVelocityReducerZone : MonoBehaviour
{
	[SerializeField][Range(.1f, 1)] private float _reduceCoefficient = 0.1f;

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent(out Rigidbody rb))
		{
			if (rb.linearVelocity.z < 0)
			{
				rb.linearVelocity = (Vector3.one * _reduceCoefficient);
			}
		}
	}
}
