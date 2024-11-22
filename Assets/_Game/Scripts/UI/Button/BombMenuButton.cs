using IDosGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button), typeof(MenuButton))]
public class BombMenuButton : MonoBehaviour
{
	public const string BOMB_CURRENCY_ID = "BO";

	[SerializeField] private GameCubeSpawner _cubeSpawner;
	[SerializeField] private OfferPopUp _buyBombsPopUp;
	[SerializeField] private TMP_Text _amount;

	private Button _button;
	private MenuButton _menuButton;

	private void Awake()
	{
		_button = GetComponent<Button>();
		_menuButton = GetComponent<MenuButton>();
		ResetButtonListener();
	}

	private void OnEnable()
	{
		UpdateButtonView();
		UserInventory.InventoryUpdated += UpdateButtonView;
		PlayerControl.BombShooted += OnBombShooted;
	}

	private void OnDisable()
	{
		UserInventory.InventoryUpdated -= UpdateButtonView;
		PlayerControl.BombShooted -= OnBombShooted;
	}

	private void ResetButtonListener()
	{
		_button.onClick.RemoveAllListeners();
		_button.onClick.AddListener(OnClick);
	}

	private int GetBombsAmount()
	{
		return UserInventory.GetVirtualCurrencyAmount(BOMB_CURRENCY_ID);
	}

	private void UpdateButtonView()
	{
		int amount = GetBombsAmount();

		_amount.text = amount.ToString();

		_menuButton.enabled = amount > 0;
	}

	private void OnClick()
	{
		int bombAmount = GetBombsAmount();

		if (bombAmount > 0)
		{
			_cubeSpawner.SpawnBombOrDestroyIfSpawned();
		}
		else
		{
			_buyBombsPopUp.gameObject.SetActive(true);
		}
	}

	private void OnBombShooted()
	{
		UserInventory.SubtractVirtualCurrency(BOMB_CURRENCY_ID, 1);
		Loading.HideAllPanels();
	}
}
