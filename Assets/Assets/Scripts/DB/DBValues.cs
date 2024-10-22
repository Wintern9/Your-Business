using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Credit
{
    public int ID { get; private set; }
    private float _money;

    public float Money
    {
        get { return _money; }
        set
        {
            _money = value;
            ID = ++DBValues.idCounter; 
        }
    }

    public Credit(float money)
    {
        _money = money;
        ID = ++DBValues.idCounter;
    }

    public Credit(int IDC, float money)
    {
        _money = money;
        ID = IDC;
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
