using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace IDosGames
{
	public class TransactionHistoryItem : MonoBehaviour
	{
		[SerializeField] private Button _exploreButton;
		[SerializeField] private TMP_Text _transactionash;
		[SerializeField] private TMP_Text _itemAmount;
		[SerializeField] private Image _itemIcon;
		[SerializeField] private TMP_Text _itemName;
		[SerializeField] private TMP_Text _directionTo;

		public void Set(string hash, TransactionDirection direction, string itemName, int amount, string imagePath, string urlToOpen)
		{
			ResetButton(urlToOpen);

			_transactionash.text = hash;
			_itemAmount.text = amount.ToString();
			_itemIcon.sprite = Resources.Load<Sprite>(imagePath);
			_itemName.text = itemName;
			_directionTo.text = GetDirectionText(direction);
		}

		private void ResetButton(string url)
		{
			_exploreButton.onClick.RemoveAllListeners();
			_exploreButton.onClick.AddListener(() => Application.OpenURL(url));
		}

		private string GetDirectionText(TransactionDirection transactionDirection)
		{
			var text = transactionDirection.ToString();

			if (transactionDirection == TransactionDirection.Game)
			{
				text = "GAME"; //LocalizationSystem
            }
			else if (transactionDirection == TransactionDirection.UsersCryptoWallet)
			{
				text = "CRYPTO WALLET"; //LocalizationSystem
            }

			return text;
		}
	}
}