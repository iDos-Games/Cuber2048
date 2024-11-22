using System.Collections.Generic;
using UnityEngine;

public class ScoreData : ScriptableObject
{
	[SerializeField] private List<ScoreInfo> _scores;
	public List<ScoreInfo> Scores => _scores;
}
