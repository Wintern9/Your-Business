using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLimitCredits : MonoBehaviour
{
    [SerializeField] private GameObject ButtonNewCredit;

    private bool LimiteCredits;
    private int creditCountRepaid = 0;

    void Start()
    {
        foreach (var credit in DBValues.Credit)
        {
            if (credit.Repaid == 0)
                ++creditCountRepaid;
        }
        if (creditCountRepaid >= 3)
            LimiteCredits = true;
    }

    void Update()
    {
        DisableButtonNewCredit();
        CheckLimit();
    }

    void DisableButtonNewCredit()
    {
        if (ButtonNewCredit != null && LimiteCredits)
        {
            ButtonNewCredit.SetActive(false);
        }
        else if (ButtonNewCredit != null)
        {
            ButtonNewCredit.SetActive(true);
        }

    }

    void CheckLimit()
    {
        if (creditCountRepaid >= 3)
            LimiteCredits = true;
        else
            LimiteCredits = false;
    }

    public void AddCountRepaid()
    {
        creditCountRepaid++;
    }

    public void SubCountRepaid()
    {
        creditCountRepaid--;
    }
}
