using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBGetValues : MonoBehaviour
{
    void Awake()
    {
        string[] Queries = {"id", "money"};

        DBValues.Credit = MySQLConnection.LoadCredits(MySQLConnection.connectionString);
        DBValues.Player = MySQLConnection.LoadPlayer(MySQLConnection.connectionString);
        Debug.Log($"{DBValues.Player.ID}, {DBValues.Player.Money}");
    }
}
