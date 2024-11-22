using UnityEngine;

public class SettingsPopUp : PopUp
{
	[SerializeField] private SwitchToggle _soundFXToggle;
	[SerializeField] private SwitchToggle _musicToggle;
	[SerializeField] private SwitchToggle _vibrationToggle;

	private void OnEnable()
	{
		GameSettings.SettingsChanged += UpdateToggles;
		UpdateToggles();
	}

	private void OnDisable()
	{
		GameSettings.SettingsChanged -= UpdateToggles;
	}

	private void UpdateToggles()
	{
		_soundFXToggle.Switch(GameSettings.SoundFXEnabled);
		_musicToggle.Switch(GameSettings.BackgroundMusicEnabled);
		_vibrationToggle.Switch(GameSettings.VibrationEnabled);
	}

	public void SwitchSoundFXState()
	{
		GameSettings.SwitchSoundFX();
	}

	public void SwitchBackgroundMusicState()
	{
		GameSettings.SwitchBackgroundMusic();
	}

	public void SwitchVibrationState()
	{
		GameSettings.SwitchVibration();
	}
}
