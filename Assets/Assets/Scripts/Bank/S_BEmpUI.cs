using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_BEmpUI : MonoBehaviour
{
    [SerializeField] private GameObject OCredit;
    [SerializeField] private GameObject OInvest;

    [SerializeField] private Button BCredit;
    [SerializeField] private Button BInvest;

    public void CreditButton()
    {
        OCredit.SetActive(true);
        OInvest.SetActive(false);
        BCredit.interactable = false;
        BInvest.interactable = true;
    }

    public void InvestButton()
    {
        OCredit.SetActive(false);
        OInvest.SetActive(true);
        BCredit.interactable = true;
        BInvest.interactable = false;
    }

    public void ExitButton()
    {
        OCredit.SetActive(false);
        OInvest.SetActive(false);
        BCredit.interactable = true;
        BInvest.interactable = true;
    }
}
