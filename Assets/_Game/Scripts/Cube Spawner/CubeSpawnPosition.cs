using UnityEngine;

public class CubeSpawnPosition : MonoBehaviour
{
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.magenta;
		Gizmos.DrawWireCube(transform.position, new Vector3(1, 1, 1));
	}
}
