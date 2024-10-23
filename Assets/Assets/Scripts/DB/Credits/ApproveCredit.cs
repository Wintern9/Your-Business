using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ApproveCredit : MonoBehaviour
{
    public TMP_InputField inputField;

    [SerializeField] private LoadOnViewCredit loadOnViewCreditObject;

    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(Approve);
        gameObject.GetComponent<Button>().onClick.AddListener(UpdateView);
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
        }
    }

    void UpdateView()
    {
        loadOnViewCreditObject.UpdateView();
    }

    //bool Check(string s)
    //{
    //    if(s == null)
    //        return false;
        
    //    if()
    //        return true;
    //}
}
