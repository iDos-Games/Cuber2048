using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MenuButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
	public static event Action _pointerDown;
	public static event Action _pointerUp;

	public void OnPointerDown(PointerEventData eventData)
	{
		_pointerDown?.Invoke();
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		_pointerUp?.Invoke();
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		_pointerDown?.Invoke();
	}
}
