using IDosGames;
using System.Collections;
using UnityEngine;

public class ControlTutorialText : MonoBehaviour
{
	private void OnEnable()
	{
		PlayerControl.Shooted += OnShooted;
	}

	private void OnDisable()
	{
		PlayerControl.Shooted -= OnShooted;
	}

	private void OnShooted()
	{
		gameObject.SetActive(false);

		if(!UserInventory.HasVIPStatus)
		{
            //ShopSystem.PopUpSystem.ShowVIPPopUp();
        }
    }
}
