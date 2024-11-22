using IDosGames;
using UnityEngine;

public class InGamePopUpSystem : MonoBehaviour
{
	[SerializeField] private PopUp _gameOverPopUp;
	[SerializeField] private PopUp _quitPopUp;

    private void OnEnable()
	{
		Playground.GameOver += OnGameOver;
	}

	private void OnDisable()
	{
		Playground.GameOver -= OnGameOver;
	}

    private void Start()
	{
		HideAllPopUp();
	}

    public void ShowQuitPopUp()
	{
		if (_quitPopUp != null)
		{
			SetActivatePopUp(_quitPopUp, true);
		}
	}

	private void OnGameOver()
	{
		SetActivatePopUp(_gameOverPopUp, true);
	}

	private void HideAllPopUp()
	{
		SetActivatePopUp(_gameOverPopUp, false);
	}

	private void SetActivatePopUp(PopUp popUp, bool active)
	{
		popUp.gameObject.SetActive(active);
	}
}