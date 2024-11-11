using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FabricController : MonoBehaviour
{
    [SerializeField] private TMP_InputField TMP_InputField;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;

    float MoneyPay = 0;

    void Update()
    {
        if(TMP_InputField!= null)
            MoneyPay = (float)Math.Pow(double.Parse(TMP_InputField.text),0.9);
        textMeshProUGUI.text = $"{(int)MoneyPay}";
    }

    public void Production()
    {
        DBValues.Player.Money -= (int)MoneyPay;
        DBValues.Player.Save();
        DBValues.CountItem = int.Parse(TMP_InputField.text);
    }
}
