using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Player
{
    public int ID { get; set; }
    public float Money { get; set; }
    public string Settings { get; set; }

    public Player(int id, float money, string settings)
    {
        ID = id;
        Money = money;
        Settings = settings;
    }
}

public struct Credit
{
    public int ID { get; set; }
    public float Money { get; set; }
    public int Repaid { get; set; }

    public Credit(int id, float money, int repaid)
    {
        ID = id;
        Money = money;
        Repaid = repaid;
    }

    public Credit(float money, int repaid)
    {
        ID = DBValues.Credit.Count+1;
        Money = money;
        Repaid = repaid;
    }

    public Credit(float money)
    {
        ID = DBValues.Credit.Count+1;
        Money = money;
        Repaid = 0;
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
    static public Player Player = new Player();
    static public List<HistoryCredit> HistoryCredit = new List<HistoryCredit>();
}
