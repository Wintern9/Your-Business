using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BalanceView : MonoBehaviour
{
    static private bool balanceChange = false;

    [SerializeField] private TextMeshProUGUI TextBalanceMoney;

    void Start()
    {
        TextBalanceMoney.text = $"Ваш баланс: {DBValues.Player.Money}$";
    }

    void Update()
    {
        if (balanceChange)
        {
            TextBalanceMoney.text = $"Ваш баланс: {DBValues.Player.Money}$";
            balanceChange = false;
        }
    }

    static public void BalanceChange()
    {
        balanceChange = true;
    }
}
