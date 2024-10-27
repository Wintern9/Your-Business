using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewCreditCheckInputValue : MonoBehaviour
{
    TMP_InputField inputField;
    private void Start()
    {
        inputField = GetComponent<TMP_InputField>();
        inputField.onValueChanged.AddListener(ValidateInput);
        inputField.onValueChanged.AddListener(Check);
    }

    void Check(string input)
    {
        if((input != null || input != ""))
        {
            int.TryParse(input, out int s);
            if (10000 < s)
                inputField.text = "10000";
        }
    }

    void ValidateInput(string input)
    {
        string filteredInput = "";
        foreach (char c in input)
        {
            if (char.IsDigit(c))
            {
                filteredInput += c;
            }
        }

        if (input != filteredInput)
        {
            inputField.text = filteredInput;
        }
    }
}
