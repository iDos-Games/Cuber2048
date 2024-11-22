using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundSystem : MonoBehaviour
{
	private static SoundSystem _instance;

	private AudioSource _audioSource;

	[SerializeField] private AudioSource _backgroundMusic;

	[SerializeField] private AudioClip _cubeMergeSound;
	[Range(0, 1.0f)][SerializeField] private float _cubeMergeSoundVolume = 1.0f;

	[SerializeField] private AudioClip _shootSound;
	[Range(0, 1.0f)][SerializeField] private float _shootSoundVolume = 1.0f;

	[SerializeField] private AudioClip _gameOverSound;
	[Range(0, 1.0f)][SerializeField] private float _gameOverSoundVolume = 1.0f;

	private void Awake()
	{
		if (_instance == null || ReferenceEquals(this, _instance))
		{
			_instance = this;
		}

		_audioSource = GetComponent<AudioSource>();

		UpdateBackgroundMusicState();
	}

	private void OnEnable()
	{
		GameSettings.SettingsChanged += UpdateBackgroundMusicState;
		GameCubeSpawner.NewCubeSpawnedAfterMerge += PlayCubeMergeSound;
		PlayerControl.Shooted += PlayShootSound;
		Playground.GameOver += PlayGameOverSound;
	}

	private void OnDisable()
	{
		GameSettings.SettingsChanged -= UpdateBackgroundMusicState;
		GameCubeSpawner.NewCubeSpawnedAfterMerge -= PlayCubeMergeSound;
		PlayerControl.Shooted -= PlayShootSound;
		Playground.GameOver -= PlayGameOverSound;
	}

	public void UpdateBackgroundMusicState()
	{
		_backgroundMusic.gameObject.SetActive(GameSettings.BackgroundMusicEnabled);
	}

	public void PlayCubeMergeSound()
	{
		if (GameSettings.SoundFXEnabled)
		{
			_audioSource.PlayOneShot(_cubeMergeSound, _cubeMergeSoundVolume);
		}
	}

	public void PlayShootSound()
	{
		if (GameSettings.SoundFXEnabled)
		{
			_audioSource.PlayOneShot(_shootSound, _shootSoundVolume);
		}
	}

	public void PlayGameOverSound()
	{
		if (GameSettings.BackgroundMusicEnabled)
		{
			_audioSource.PlayOneShot(_gameOverSound, _gameOverSoundVolume);
		}
	}
}