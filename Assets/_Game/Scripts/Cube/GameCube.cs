using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public abstract class GameCube : MonoBehaviour
{
	[SerializeField] private GameCubeView _view;
	public GameCubeView View => _view;

	public Rigidbody Rigidbody => _rigidbody;

	private Rigidbody _rigidbody;

	public BoxCollider BoxCollider => _boxCollider;

	private BoxCollider _boxCollider;

	public virtual void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
		_boxCollider = GetComponent<BoxCollider>();
	}

	public void SetToReadyState(bool showAim)
	{
		transform.localScale = Vector3.one;
		Rigidbody.isKinematic = false;
		View.SetActiveAim(showAim);
	}

	public void SetToUnreadyState()
	{
		transform.localScale = Vector3.zero;
		Rigidbody.isKinematic = true;
		View.SetActiveAim(false);
	}
}