using UnityEngine;

public class GameStateSwitcher : MonoBehaviour
{
	private GameStateType _stateWhenEnabled;

	private void OnEnable()
	{
		_stateWhenEnabled = GameState.CurrentState;

		if (_stateWhenEnabled == GameStateType.Active)
		{
			GameState.Pause();
		}
	}

	private void OnDisable()
	{
		if (_stateWhenEnabled == GameStateType.Active)
		{
			GameState.UnPause();
		}
	}
}
