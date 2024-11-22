using IDosGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button), typeof(MenuButton))]
public class MulticubeMenuButton : MonoBehaviour
{
	public const string MULTICUBE_CURRENCY_ID = "MC";

	[SerializeField] private GameCubeSpawner _cubeSpawner;
	[SerializeField] private OfferPopUp _buyMulticubesPopUp;
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
		PlayerControl.MulticubeShooted += OnMulticubeShooted;
	}

	private void OnDisable()
	{
		UserInventory.InventoryUpdated -= UpdateButtonView;
		PlayerControl.MulticubeShooted -= OnMulticubeShooted;
	}

	private void ResetButtonListener()
	{
		_button.onClick.RemoveAllListeners();
		_button.onClick.AddListener(OnClick);
	}

	private int GetMulticubesAmount()
	{
		return UserInventory.GetVirtualCurrencyAmount(MULTICUBE_CURRENCY_ID);
	}

	private void UpdateButtonView()
	{
		int amount = GetMulticubesAmount();

		_amount.text = amount.ToString();

		_menuButton.enabled = amount > 0;
	}

	private void OnClick()
	{
		int bombAmount = GetMulticubesAmount();

		if (bombAmount > 0)
		{
			_cubeSpawner.SpawnMulticubeOrDestroyIfSpawned();
		}
		else
		{
			_buyMulticubesPopUp.gameObject.SetActive(true);
		}
	}

	private void OnMulticubeShooted()
	{
		UserInventory.SubtractVirtualCurrency(MULTICUBE_CURRENCY_ID, 1);
		Loading.HideAllPanels();
	}
}
