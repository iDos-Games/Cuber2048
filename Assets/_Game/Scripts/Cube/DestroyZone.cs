using UnityEngine;


[RequireComponent(typeof(BoxCollider))]
public class DestroyZone : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (!other.TryGetComponent(out MergeableCube cube))
		{
			return;
		}

		CubesPool.Destroy(cube);
	}
}
