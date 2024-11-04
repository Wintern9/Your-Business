using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBGetValues : MonoBehaviour
{
    void Awake()
    {
        DBValues.Credit = MySQLConnection.LoadCredits(MySQLConnection.connectionString);
        DBValues.JobsNS = MySQLConnection.LoadJobsNS(MySQLConnection.connectionString);
        DBValues.Player = MySQLConnection.LoadPlayer(MySQLConnection.connectionString);

        DBValues.CompanyJobPlaces = new CompanyJobPlaces(0,0,0);
        Debug.Log($"{DBValues.Player.ID}, {DBValues.Player.Money}");
        Debug.Log($"---- {DBValues.JobsNS.Names.Length}, {DBValues.JobsNS.Surnames.Length}");
    }
}
