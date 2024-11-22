using TMPro;
using UnityEngine;

public class PlayerScoreView : MonoBehaviour
{
	[SerializeField] private TMP_Text _record;
	[SerializeField] private TMP_Text _current;

	public void SetRecordText(int value)
	{
		if (_record == null)
		{
			return;
		}

		_record.text = value.ToString();
	}

	public void SetCurrentText(int value)
	{
		if (_current == null)
		{
			return;
		}

		_current.text = value.ToString();
	}
}
