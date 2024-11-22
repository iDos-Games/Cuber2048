using IDosGames;
using UnityEngine;

[RequireComponent(typeof(ButtonActivateSideBar))]
public class BurgerButtonHideOnShoot : MonoBehaviour
{
	[SerializeField] private bool _hideOnShoot;
	private ButtonActivateSideBar _burgerButton;

	private void Awake()
	{
		_burgerButton = GetComponent<ButtonActivateSideBar>();
	}

	private void OnEnable()
	{
		PlayerControl.Shooted += OnCubeShooted;
	}

	private void OnDisable()
	{
		PlayerControl.Shooted -= OnCubeShooted;
	}

	private void OnCubeShooted()
	{
		if (_hideOnShoot)
		{
			if (_burgerButton.IsSideBarActive)
			{
				_burgerButton.SetActiveSideBar(false);
			}
		}
	}
}
