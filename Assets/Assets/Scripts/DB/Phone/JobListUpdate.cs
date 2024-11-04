using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JobListUpdate : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshProUGUI text;

    bool ButtonEnable = true;

    public void UpdateViewStartAnimation()
    {
        if (ButtonEnable)
        {
            animator.SetBool("ButtonClick", true);
            StartCoroutine(DelayedAction());
            StartCoroutine(DelayedButton());
        }

        ButtonEnable = false;
        text.color = Color.gray;
    }

    IEnumerator DelayedAction()
    {
        yield return new WaitForSeconds(3f);

        UpdateInfo();
    }

    IEnumerator DelayedButton()
    {
        yield return new WaitForSeconds(8f);
        Button();
    }

    void UpdateInfo()
    {
        Debug.Log("Coroutine");
        animator.SetBool("ButtonClick", false);

        ///////
    }

    void Button()
    {
        ButtonEnable = true;
        text.color = Color.white;
    }
}
