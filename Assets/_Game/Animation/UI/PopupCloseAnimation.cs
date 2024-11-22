using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupCloseAnimation : MonoBehaviour
{
    public GameObject popUp;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ClosePopup()
    {
        StartCoroutine(PlayAnimationAndWait());
    }

    private IEnumerator PlayAnimationAndWait()
    {
        animator.enabled = true;
        animator.Play("PopupClose");

        // ����, ���� �������� ��������� �����������
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // ��������� ������
        popUp.SetActive(false);
    }
}