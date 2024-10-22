using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBGetValues : MonoBehaviour
{
    static private string server = "localhost";
    static private string user = "root";
    static private string password = "";
    static private string connectionString;

    static private MySqlConnection connection;

    void Awake()
    {
        connectionString = $"Server={server}; port=3307; database=unity_db ;UID={user}; password={password};";

        string[] Queries = {"id", "money"};

        //MySQLConnection.SetCredits(connectionString, new Credit(2, 10000f));

        DBValues.Credit = MySQLConnection.LoadCredits(connectionString);
        
    }
}
