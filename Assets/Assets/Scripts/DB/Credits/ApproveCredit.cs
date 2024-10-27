using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ApproveCredit : MonoBehaviour
{
    public TMP_InputField inputField;

    [SerializeField] private LoadOnViewCredit loadOnViewCreditObject;
    [SerializeField] private Button buttonApprove;

    private void Start()
    {
        buttonApprove = gameObject.GetComponent<Button>();

        buttonApprove.onClick.AddListener(Approve);
        buttonApprove.onClick.AddListener(UpdateView);
    }

    void Approve()
    {
        if (Check(inputField.text))
        {
            string connectionString = $"Server=localhost; port=3307; database=unity_db ;UID=root; password=;";
            MySQLConnection.SetCredits(connectionString, new Credit(float.Parse(inputField.text)));
            DBValues.Credit.Add(new Credit(float.Parse(inputField.text)));
            DBValues.Credit.Sort((x, y) =>
            {
                int result = x.Repaid.CompareTo(y.Repaid);
                if (result == 0)
                {
                    return x.ID.CompareTo(y.ID);
                }
                return result;
            });

            DBValues.Player.Money += float.Parse(inputField.text);
            DBValues.Player.Save();

            ButtonClick();
        }
    }

    void UpdateView()
    {
        loadOnViewCreditObject.UpdateView();
    }

    bool Check(string s)
    {
        if (s == null || s == "")
            return false;
        if (s != null)
            return true;

        return false;
    }

    [SerializeField] private GameObject NewCredit;

    private void ButtonClick()
    {
        if (NewCredit != null)
            NewCredit.SetActive(false);
    }

    private void Update()
    {
        if(inputField.text == null || inputField.text == "")
        {
            buttonApprove.enabled = false;
        }
        else
        {
             buttonApprove.enabled = true;
        }
    }
}
