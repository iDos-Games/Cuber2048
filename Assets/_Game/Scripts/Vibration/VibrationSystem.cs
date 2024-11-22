//using MoreMountains.NiceVibrations;
using UnityEngine;

public class VibrationSystem : MonoBehaviour
{
	//[Header("Vibration Types")]

	//[SerializeField] private HapticTypes _cubeMerge = HapticTypes.MediumImpact;
	//[SerializeField] private HapticTypes _shoot = HapticTypes.LightImpact;
	//[SerializeField] private HapticTypes _explodeImpact = HapticTypes.HeavyImpact;
	//[SerializeField] private HapticTypes _gameOver = HapticTypes.HeavyImpact;

	private void OnEnable()
	{
		PlayerControl.Shooted += VibrateOnShoot;
		GameCubeSpawner.NewCubeSpawnedAfterMerge += VibrateOnCubeMerge;
		Playground.GameOver += VibrateOnGameOver;
		BombCube.Exploded += VibrateOnExplode;
	}

	private void OnDisable()
	{
		PlayerControl.Shooted -= VibrateOnShoot;
		GameCubeSpawner.NewCubeSpawnedAfterMerge -= VibrateOnCubeMerge;
		Playground.GameOver -= VibrateOnGameOver;
		BombCube.Exploded -= VibrateOnExplode;
	}

	private void VibrateOnShoot()
	{
		if (GameSettings.VibrationEnabled == false)
		{
			return;
		}

		//MMVibrationManager.Haptic(_shoot, false, true, this);
	}

	private void VibrateOnCubeMerge()
	{
		if (GameSettings.VibrationEnabled == false)
		{
			return;
		}

		//MMVibrationManager.Haptic(_cubeMerge, false, true, this);
	}
	private void VibrateOnExplode()
	{
		if (GameSettings.VibrationEnabled == false)
		{
			return;
		}

		//MMVibrationManager.Haptic(_explodeImpact, false, true, this);
	}

	private void VibrateOnGameOver()
	{
		if (GameSettings.VibrationEnabled == false)
		{
			return;
		}

		//MMVibrationManager.Haptic(_gameOver, false, true, this);
	}

	public void Selection()
	{
		if (GameSettings.VibrationEnabled == false)
		{
			return;
		}

		//MMVibrationManager.Haptic(HapticTypes.Selection, false, true, this);
	}

	public void Success()
	{
		if (GameSettings.VibrationEnabled == false)
		{
			return;
		}

		//MMVibrationManager.Haptic(HapticTypes.Success, false, true, this);
	}
	public void Warning()
	{
		if (GameSettings.VibrationEnabled == false)
		{
			return;
		}

		//MMVibrationManager.Haptic(HapticTypes.Warning, false, true, this);
	}

	public void Failure()
	{
		if (GameSettings.VibrationEnabled == false)
		{
			return;
		}

		//MMVibrationManager.Haptic(HapticTypes.Failure, false, true, this);
	}

	public void LightImpact()
	{
		if (GameSettings.VibrationEnabled == false)
		{
			return;
		}

		//MMVibrationManager.Haptic(HapticTypes.LightImpact, false, true, this);
	}

	public void MediumImpact()
	{
		if (GameSettings.VibrationEnabled == false)
		{
			return;
		}

		//MMVibrationManager.Haptic(HapticTypes.MediumImpact, false, true, this);
	}

	public void HeavyImpact()
	{
		if (GameSettings.VibrationEnabled == false)
		{
			return;
		}

		//MMVibrationManager.Haptic(HapticTypes.HeavyImpact, false, true, this);
	}

	public void RigidImpact()
	{
		if (GameSettings.VibrationEnabled == false)
		{
			return;
		}

		//MMVibrationManager.Haptic(HapticTypes.RigidImpact, false, true, this);
	}

	public void SoftImpact()
	{
		if (GameSettings.VibrationEnabled == false)
		{
			return;
		}

		//MMVibrationManager.Haptic(HapticTypes.SoftImpact, false, true, this);
	}

	public void None()
	{
		if (GameSettings.VibrationEnabled == false)
		{
			return;
		}

		//MMVibrationManager.Haptic(HapticTypes.None, false, true, this);
	}
}
