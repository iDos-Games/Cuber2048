using IDosGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpMergeReward : PopUp
{
	[SerializeField] private TMP_Text _collectedCubes;
	[SerializeField] private TMP_Text _coinRewardAmount;
	[SerializeField] private TMP_Text _eventRewardAmount;
	[SerializeField] private TMP_Text _skinProfitAmount;

	private void OnEnable()
	{
		UpdateSkinProfitAmountText();
	}

	public void Show(string collectedCubes, int coinReward, int eventReward)
	{
        RewardMultiplicator._currentCoinReward = coinReward;
        RewardMultiplicator._currentEventReward = eventReward;

		UpdateCollectedCubesText(collectedCubes);
		UpdateCoinRewardAmountText(coinReward);
		UpdateEventRewardAmountText(eventReward);

		SetActivatePopUp(true);
	}

	public void ShowInterstitial()
	{
        if (AdMediation.Instance != null)
        {
            if (!UserInventory.HasVIPStatus)
            {
                AdMediation.Instance.ShowInterstitialAd();
            }
        }
    }

	public void CloseRewardPopup()
	{
		ShowInterstitial();
		//ClaimRewardSystem.ClaimCoinReward(RewardMultiplicator._currentCoinReward);
        WeeklyEventSystem.AddEventPoints(RewardMultiplicator._currentEventReward);

		SetActivatePopUp(false);
	}

	public void ClaimX3Reward()
	{
		try
		{
			if (AdMediation.Instance != null)
			{
				if (AdMediation.Instance.ShowRewardedVideo(OnFinishedWatchingRewardedVideoForX3Reward))
				{
					Debug.Log("Show rewarded video.");
				}
				else
				{
					Message.Show(MessageCode.AD_IS_NOT_READY);
					ShopSystem.PopUpSystem.ShowVIPPopUp();
				}
			}
			else
			{
				Message.Show(MessageCode.AD_IS_NOT_READY);
				ShopSystem.PopUpSystem.ShowVIPPopUp();
			}
		}
		catch
		{
			Message.Show(MessageCode.AD_IS_NOT_READY);
			ShopSystem.PopUpSystem.ShowVIPPopUp();
		}
	}

	public void ClaimX5Reward()
	{
		if (UserInventory.HasVIPStatus)
		{
			ClaimRewardSystem.ClaimX5CoinReward(RewardMultiplicator._currentCoinReward, RewardMultiplicator._currentEventReward);
			//WeeklyEventSystem.AddEventPoints(RewardMultiplicator._currentEventReward * 5);

			SetActivatePopUp(false);
		}
		else
		{
			ShopSystem.PopUpSystem.ShowVIPPopUp();
		}
	}

	private void OnFinishedWatchingRewardedVideoForX3Reward(bool finished)
	{
		ClaimRewardSystem.ClaimX3CoinReward(RewardMultiplicator._currentCoinReward, RewardMultiplicator._currentEventReward);
		//WeeklyEventSystem.AddEventPoints(RewardMultiplicator._currentEventReward * 3);

		SetActivatePopUp(false);
	}

	private void UpdateCollectedCubesText(string cubes)
	{
		_collectedCubes.text = cubes;
	}

	private void UpdateSkinProfitAmountText()
	{
		_skinProfitAmount.text = ClaimRewardSystem.GetSkinProfitAmount().ToString();
	}

	private void UpdateEventRewardAmountText(int amount)
	{
		_eventRewardAmount.text = amount.ToString();
	}

	private void UpdateCoinRewardAmountText(int amount)
	{
		_coinRewardAmount.text = amount.ToString();
	}

    public void SetActivatePopUp(bool active)
	{
		gameObject.SetActive(active);
	}
}