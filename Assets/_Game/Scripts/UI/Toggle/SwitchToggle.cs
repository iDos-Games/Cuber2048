using UnityEngine;
using UnityEngine.UI;

public class SwitchToggle : MonoBehaviour
{
	[SerializeField] private Image _enabledState;
	[SerializeField] private Image _disabledState;

	public void Switch(bool enabled)
	{
		_enabledState.gameObject.SetActive(enabled);
		_disabledState.gameObject.SetActive(!enabled);
	}
}
