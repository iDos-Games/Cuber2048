using System;
using UnityEngine;

public static class GameSettings
{
	public const string VIBRATION_ENABLED_PLAYER_PREFS = "VIBRATION_ENABLED";
	public const string SOUND_FX_ENABLED_PLAYER_PREFS = "SOUND_FX_ENABLED";
	public const string BACKGROUND_MUSIC_ENABLED_PLAYER_PREFS = "BACKGROUND_MUSIC_ENABLED";

	public static bool VibrationEnabled { get; private set; }
	public static bool SoundFXEnabled { get; private set; }
	public static bool BackgroundMusicEnabled { get; private set; }

	public static event Action SettingsChanged;

	[RuntimeInitializeOnLoadMethod]
	public static void RefreshStates()
	{
		VibrationEnabled = PlayerPrefs.GetInt(VIBRATION_ENABLED_PLAYER_PREFS, 1) == 1;
		SoundFXEnabled = PlayerPrefs.GetInt(SOUND_FX_ENABLED_PLAYER_PREFS, 1) == 1;
		BackgroundMusicEnabled = PlayerPrefs.GetInt(BACKGROUND_MUSIC_ENABLED_PLAYER_PREFS, 1) == 1;
	}

	public static void SwitchVibration()
	{
		int newState = VibrationEnabled ? 0 : 1;

		PlayerPrefs.SetInt(VIBRATION_ENABLED_PLAYER_PREFS, newState);

		VibrationEnabled = !VibrationEnabled;

		SettingsChanged?.Invoke();
	}

	public static void SwitchSoundFX()
	{
		int newState = SoundFXEnabled ? 0 : 1;

		PlayerPrefs.SetInt(SOUND_FX_ENABLED_PLAYER_PREFS, newState);

		SoundFXEnabled = !SoundFXEnabled;

		SettingsChanged?.Invoke();
	}

	public static void SwitchBackgroundMusic()
	{
		int newState = BackgroundMusicEnabled ? 0 : 1;

		PlayerPrefs.SetInt(BACKGROUND_MUSIC_ENABLED_PLAYER_PREFS, newState);

		BackgroundMusicEnabled = !BackgroundMusicEnabled;

		SettingsChanged?.Invoke();
	}
}
