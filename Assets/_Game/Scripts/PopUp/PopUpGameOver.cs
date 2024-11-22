using TMPro;
using UnityEngine;

public class PopUpGameOver : PopUp
{
	[SerializeField] private TMP_Text _newRecord;

	private void OnEnable()
	{
		_newRecord.gameObject.SetActive(PlayerScoreSystem.RecordBeated);
	}
}