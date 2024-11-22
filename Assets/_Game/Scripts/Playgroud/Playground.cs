using System;
using UnityEngine;

public class Playground : MonoBehaviour
{
	public static event Action GameOver;

	public static event Action NewGame;

	private void OnEnable()
	{
		FrontierLineCollider.Collided += OnGameOver;
	}

	private void OnDisable()
	{
		FrontierLineCollider.Collided -= OnGameOver;
	}

	public static void StartNewGame()
	{
		NewGame?.Invoke();
	}

	private void OnGameOver()
	{
		GameOver?.Invoke();
	}
}
