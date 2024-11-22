using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAndShow : MonoBehaviour
{
    [SerializeField] private GameObject _object;
    private bool _show = false;

    private void OnEnable()
    {
        if (_show)
        {
            _object.SetActive(true);
            _show = false;
        }
        else
        {
            _object.SetActive(false);
            _show = true;
        }
    }
}
