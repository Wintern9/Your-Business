using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RepayCredit : MonoBehaviour
{
    public int idRepay;

    private Button buttonCredit;

    [SerializeField] GameObject TextRepay;

    void Start()
    {
        buttonCredit = GetComponentInChildren<Button>();
        buttonCredit.onClick.AddListener(Repay);

        if (DBValues.Credit[idRepay].Repaid == 1)
        {
            HideButtonRepay();
        }
    }

    private void Update()
    {
        if (buttonCredit != null && DBValues.Player.Money < DBValues.Credit[idRepay].Money)
        {
            buttonCredit.gameObject.GetComponentInChildren<TextMeshProUGUI>().color = Color.gray;
            buttonCredit.interactable = false;
        } else
        {
            buttonCredit.gameObject.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
            buttonCredit.interactable = true;
        }
    }

    private void Repay()
    {
        float moneyRepay = DBValues.Credit[idRepay].Money;

        if (CheckBalance(moneyRepay))
        {
            DBValues.Player.Money -= moneyRepay;
            DBValues.Player.Save();

            Credit credit = DBValues.Credit[idRepay];
            credit.Repaid = 1;
            DBValues.Credit[idRepay] = credit;
            DBValues.Credit[idRepay].UpdateRepaid();

            FindFirstObjectByType<CheckLimitCredits>().SubCountRepaid();

            HideButtonRepay();
        }
    }

    bool CheckBalance(float money)
    {
        if(DBValues.Player.Money >= money)
        {
            return true;
        }

        return false;
    }

    void HideButtonRepay()
    {
        buttonCredit.gameObject.SetActive(false);
        TextRepay.SetActive(true);
    }
}
