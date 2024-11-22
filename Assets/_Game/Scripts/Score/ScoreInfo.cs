using System;
using UnityEngine;

[Serializable]
public class ScoreInfo
{
	[SerializeField] public string _name;
	[SerializeField] private CubeNumber.Index _cubeIndex;
	public CubeNumber.Index CubeIndex => _cubeIndex;

	[Range(0, 2500)][SerializeField] private int _score;
	public int Score => _score;

	[SerializeField] private int _coinRewardAmount;
	public int CoinRewardAmount => _coinRewardAmount;
}
