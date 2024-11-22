using System;
using UnityEngine;

public class GameState
{
	public static event Action Paused;
	public static event Action Unpaused;

	public static GameStateType CurrentState { get; private set; } = GameStateType.Active;

	public static bool IsPaused => CurrentState == GameStateType.Pause;

	public static void Pause()
	{
		Time.timeScale = 0;
		CurrentState = GameStateType.Pause;
		Paused?.Invoke();
	}

	public static void UnPause()
	{
		Time.timeScale = 1;
		CurrentState = GameStateType.Active;
		Unpaused?.Invoke();
	}
}
