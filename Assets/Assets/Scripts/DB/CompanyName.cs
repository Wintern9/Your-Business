using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CompanyName : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TMP_InputField inputField;

    void Start()
    {
        button.onClick.AddListener(SetName);

        if(DBValues.CompanyName != null )
        {
            gameObject.SetActive(false);
        }
    }

    void SetName()
    {
        DBValues.CompanyName = inputField.name;
    }
}
