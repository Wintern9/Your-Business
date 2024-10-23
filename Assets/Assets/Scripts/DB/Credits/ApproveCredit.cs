using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ApproveCredit : MonoBehaviour
{
    public TMP_InputField inputField;

    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(Approve);
    }

    void Approve()
    {
        string connectionString = $"Server=localhost; port=3307; database=unity_db ;UID=root; password=;";
        MySQLConnection.SetCredits(connectionString, new Credit(float.Parse(inputField.text)));
        DBValues.Credit.Add(new Credit(float.Parse(inputField.text)));
    }
}
