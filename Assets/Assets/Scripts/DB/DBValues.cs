using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Player
{
    public int ID { get; set; }
    private float money;

    public float Money
    {
        get { return money; }
        set
        {
            money = value;
            BalanceView.BalanceChange();
        }
    }

    public string Settings { get; set; }

    public Player(int id, float initialMoney, string settings)
    {
        ID = id;
        money = initialMoney;
        Settings = settings;
    }

    public void Save()
    {
        MySQLConnection.SetPlayer(MySQLConnection.connectionString, this);
    }
}



public struct Credit
{
    public int ID { get; set; }
    public float Money { get; set; }
    private int repaid { get; set; }

    public int Repaid {
        get { return repaid; } 
        set
        { 
            repaid = value;
        } 
    }

    public Credit(int id, float money, int repaids)
    {
        ID = id;
        Money = money;
        repaid = repaids;
    }

    public Credit(float money, int repaids)
    {
        ID = DBValues.Credit.Count+1;
        Money = money;
        repaid = repaids;
    }

    public Credit(float money)
    {
        ID = DBValues.Credit.Count+1;
        Money = money;
        repaid = 0;
    }

    public void UpdateRepaid()
    {
        MySQLConnection.UpdateCredits(MySQLConnection.connectionString, this);
    }
}

public struct HistoryCredit
{
    public int ID { get; set; }
    public float Money { get; set; }
}

public enum Job
{
    ///
}

public struct Jobs
{
    public int ID { get; set; }
    public Job job { get; set; }
    public int buff { get; set; }

    public Jobs(int id, Job job, int repaids)
    {
        ID = id;
        this.job = job;
        buff = repaids;
    }
}

public enum Room
{
    Room,
    RoomNT,
}
public enum TypeRoom
{
    Empty,
    Accountant,
    Marketing,
    Programmer
}


public struct Rooms
{
    public int ID { get; set; }
    public Room room { get; set; }
    public TypeRoom type { get; set; }
    public int level { get; set; }
    public Vector2 posRoom { get; set; }

    public Rooms(int id, Room room, TypeRoom type, int level, Vector2 posRoom)
    {
        ID = id;
        this.room = room;
        this.type = type;
        this.level = level;
        this.posRoom = posRoom;
    }
}

public class DBValues : MonoBehaviour
{
    public static int idCounter = -1;

    static public float MoneyValue;
    static public List<Credit> Credit = new List<Credit>();
    static public List<Rooms> Rooms = new List<Rooms>();
    static public Player Player = new Player();
    static public List<HistoryCredit> HistoryCredit = new List<HistoryCredit>();
}
