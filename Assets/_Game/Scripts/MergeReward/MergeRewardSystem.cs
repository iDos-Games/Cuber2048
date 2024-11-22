using IDosGames;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class MergeRewardSystem : MonoBehaviour
{
	private const int EVENT_REWARD_AMOUNT = 1;

	[SerializeField] private float _showPopUpDealyInSeconds = 3.5f;

	[SerializeField] private PopUpMergeReward _popUp;
	[SerializeField] private ScoreData _scoreData;

    [SerializeField] private GameObject _popupSpin;
    public SpinWheel _spinWheel;

    private bool showedSpin = false;

	private CubeNumber.Index _maxSpawnedIndex = CubeNumber.MIN;

	private void OnEnable()
	{
		GameCubeSpawner.NewCubeSpawnedAfterMergeWithIndex += OnNewCubeSpawnedAfterMerge;
		Playground.GameOver += StopAllCoroutines;
        _spinWheel.SpinEnded += OnSpinEnded;
    }

    private void OnSpinEnded(int currentSectorIndex)
    {
        _popupSpin.SetActive(false);
    }

    private void OnDisable()
	{
		GameCubeSpawner.NewCubeSpawnedAfterMergeWithIndex -= OnNewCubeSpawnedAfterMerge;
		Playground.GameOver -= StopAllCoroutines;
        _spinWheel.SpinEnded -= OnSpinEnded;
    }

	private void OnNewCubeSpawnedAfterMerge(CubeNumber.Index index)
	{
		if (index < CubeNumber.Index._512)
		{
			return;
		}

		if (index <= _maxSpawnedIndex)
		{
			return;
		}

		_maxSpawnedIndex = index;

		StopAllCoroutines();
		StartCoroutine(ShowMergeWindow(_maxSpawnedIndex));
	}

	private IEnumerator ShowMergeWindow(CubeNumber.Index index)
	{
		yield return new WaitForSeconds(_showPopUpDealyInSeconds);

		if (!UserInventory.HasVIPStatus)
		{
            if (!showedSpin)
            {
                int coinReward = _scoreData.Scores.FirstOrDefault(x => x.CubeIndex == index).CoinRewardAmount;

                _popUp.Show($"{index.ToString().Replace("_", string.Empty)}", coinReward, EVENT_REWARD_AMOUNT);

                _maxSpawnedIndex = CubeNumber.MIN;

                showedSpin = true;
            }
            else
            {
                _popupSpin.SetActive(true);

                _maxSpawnedIndex = CubeNumber.MIN;

                showedSpin = false;
            }
        }
		else
		{
            int coinReward = _scoreData.Scores.FirstOrDefault(x => x.CubeIndex == index).CoinRewardAmount;

            _popUp.Show($"{index.ToString().Replace("_", string.Empty)}", coinReward, EVENT_REWARD_AMOUNT);

            _maxSpawnedIndex = CubeNumber.MIN;
        }
	}
}
