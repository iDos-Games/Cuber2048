using UnityEngine;

public class SoundBySettings : MonoBehaviour
{
	[SerializeField] private AudioSource _audioSource;

	private void OnEnable()
	{
		if (_audioSource == null)
		{
			return;
		}

		_audioSource.enabled = GameSettings.SoundFXEnabled;
	}
}
