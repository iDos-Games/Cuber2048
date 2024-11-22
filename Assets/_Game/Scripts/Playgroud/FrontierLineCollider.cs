using System;
using System.Collections;
using UnityEngine;

public class FrontierLineCollider : MonoBehaviour
{
	public static event Action Collided;

	private bool _collided;

	private const float DELAY_TO_CHECK_COLLISION = 0.1f;

	private Collider _enteredObjectCollider;

	private void OnTriggerEnter(Collider other)
	{
		if (!other.TryGetComponent(out MergeableCube cube))
		{
			return;
		}

		_collided = true;

		_enteredObjectCollider = other;

		StopCoroutine(nameof(CheckCollision));
		StartCoroutine(nameof(CheckCollision));
	}

	private IEnumerator CheckCollision()
	{
		yield return new WaitForSeconds(DELAY_TO_CHECK_COLLISION);

		if (_collided)
		{
			_collided = false;
			Collided?.Invoke();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (_enteredObjectCollider == other)
		{
			_collided = false;
		}
	}
}
