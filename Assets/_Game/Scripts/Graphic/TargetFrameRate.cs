using UnityEngine;

public class TargetFrameRate : MonoBehaviour
{
	[SerializeField] private int _target = 60;
	private void Start()
	{
		Application.targetFrameRate = _target;
	}
}
