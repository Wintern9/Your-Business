using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

public class MySQLConnection : MonoBehaviour
{
    private string server = "localhost"; 
    private string user = "root"; 
    private string password = "";
    private string connectionString;

    private MySqlConnection connection;

    void Start()
    {
        connectionString = $"Server={server}; port=3307; UID={user}; password={password};";
        IntelizationDataBase();
    }

    void IntelizationDataBase()
    {
        CreateDatabase();

        CreateTable(@$"
                CREATE TABLE IF NOT EXISTS credits (
                    id INT AUTO_INCREMENT PRIMARY KEY,
                    money FLOAT(20, 2)
                );", "credits");
    }

    void CreateDatabase()
    {
        try
        {
            connection = new MySqlConnection(connectionString);
            connection.Open();
            Debug.Log("���������� � MySQL �������!");

            string dbName = "unity_db";

            string createDatabaseQuery = $"CREATE DATABASE IF NOT EXISTS {dbName};";
            MySqlCommand cmd = new MySqlCommand(createDatabaseQuery, connection);
            cmd.ExecuteNonQuery();
            Debug.Log($"���� ������ '{dbName}' ������� ��� ��� ����������.");

            connection.ChangeDatabase(dbName);
        }
        catch (MySqlException ex)
        {
            Debug.LogError("������ �������� ���� ������: " + ex.Message);
        }
        finally
        {
            if (connection != null)
                connection.Close();
        }
    }

    void CreateTable(string CreateQuery, string nameTable)
    {
        try
        {
            connection.Open();
            Debug.Log("���������� � ���� ������!");

            MySqlCommand cmd = new MySqlCommand(CreateQuery, connection);
            cmd.ExecuteNonQuery();
            Debug.Log($"������� '{nameTable}' ������� ��� ��� ����������.");
        }
        catch (MySqlException ex)
        {
            Debug.LogError("������ �������� �������: " + ex.Message);
        }
        finally
        {
            if (connection != null)
                connection.Close();
        }
    }

    /// <summary>
    /// ��������� ������ �� ������� ��
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="Queries">������ ���������� ���������� ������</param>
    /// <param name="nameTable">�������� �������</param>
    /// <returns>���������� ���� ������ �������</returns>
    public List<T> LoadUserData<T>(string[] Queries, string nameTable) where T : new()
    {
        List<T> userList = new List<T>();

        try
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = $"SELECT {string.Join(", ", Queries)} FROM {nameTable}";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            T user = new T();

                            foreach (string s in Queries)
                            {
                                var idProperty = typeof(T).GetProperty($"{s}");
                                idProperty.SetValue(user, reader.GetInt32($"{s}"));
                            }

                            userList.Add(user);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return userList;
    }

    /// <summary>
    /// ��������� ������ � ������� ��
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dataList">���� ������, ������� ����� ���������</param>
    /// <param name="nameTable">�������� �������, ���� ����������� ������</param>
    /// <returns>���������� ���������� ����������� �����</returns>
    public int SaveUserData<T>(List<T> dataList, string nameTable)
    {
        int rowsInserted = 0;

        try
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                foreach (T data in dataList)
                {
                    // ��������� ������ ����� � �������� ��� �������
                    var properties = typeof(T).GetProperties();
                    var fieldNames = string.Join(", ", properties.Select(p => p.Name));
                    var paramNames = string.Join(", ", properties.Select(p => "@" + p.Name));

                    string query = $"INSERT INTO {nameTable} ({fieldNames}) VALUES ({paramNames})";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        // ��������� ��������� � �������
                        foreach (var prop in properties)
                        {
                            cmd.Parameters.AddWithValue($"@{prop.Name}", prop.GetValue(data) ?? DBNull.Value);
                        }

                        rowsInserted += cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return rowsInserted;
    }


}

