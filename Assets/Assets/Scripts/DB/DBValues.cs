using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Credit
{
    public int ID { get; set; }
    public float Money { get; set; }

    public Credit(int id, float money)
    {
        ID = id;
        Money = money;
    }
}


public struct HistoryCredit
{
    public int ID { get; set; }
    public float Money { get; set; }
}

public class DBValues : MonoBehaviour
{
    public static int idCounter = -1;

    static public float MoneyValue;
    static public List<Credit> Credit = new List<Credit>();
    static public List<HistoryCredit> HistoryCredit = new List<HistoryCredit>();
}
