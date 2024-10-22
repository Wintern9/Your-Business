using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

public class MySQLConnection : MonoBehaviour
{
    static private string server = "localhost";
    static private string user = "root";
    static private string password = "";
    static private string connectionString;

    static private MySqlConnection connection;

    void Start()
    {
        connectionString = $"Server={server}; port=3307; UID={user}; password={password};";
        //IntelizationDataBase();
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
    /// ��������� ������ �� ������� �� �� ������ ���� T
    /// </summary>
    /// <typeparam name="T">��� ������, ��������������� ��������� �������</typeparam>
    /// <param name="nameTable">�������� �������</param>
    /// <returns>���������� ���� ������ �������</returns>
    static public List<T> LoadUserData<T>(string nameTable, string connectionString) where T : new()
    {
        List<T> userList = new List<T>();

        try
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                var properties = typeof(T).GetProperties();
                var fieldNames = string.Join(", ", properties.Select(p => p.Name));

                string query = $"SELECT {fieldNames} FROM {nameTable}";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            T item = new T();

                            foreach (var prop in properties)
                            {
                                // ���������, ����� �������� �� ���� NULL
                                if (!reader.IsDBNull(reader.GetOrdinal(prop.Name)))
                                {
                                    var propType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

                                    // ������������� �������� � ����������� �� ���� ��������
                                    if (propType == typeof(int))
                                        prop.SetValue(item, reader.GetInt32(reader.GetOrdinal(prop.Name)));
                                    else if (propType == typeof(string))
                                        prop.SetValue(item, reader.GetString(reader.GetOrdinal(prop.Name)));
                                    else if (propType == typeof(DateTime))
                                        prop.SetValue(item, reader.GetDateTime(reader.GetOrdinal(prop.Name)));
                                    else if (propType == typeof(float))
                                        prop.SetValue(item, (float)reader.GetDouble(reader.GetOrdinal(prop.Name)));
                                    else if (propType == typeof(double))
                                        prop.SetValue(item, reader.GetDouble(reader.GetOrdinal(prop.Name)));
                                    else if (propType == typeof(decimal))
                                        prop.SetValue(item, reader.GetDecimal(reader.GetOrdinal(prop.Name)));
                                    else if (propType == typeof(bool))
                                        prop.SetValue(item, reader.GetBoolean(reader.GetOrdinal(prop.Name)));
                                    else if (propType == typeof(long))
                                        prop.SetValue(item, reader.GetInt64(reader.GetOrdinal(prop.Name)));
                                    else if (propType == typeof(short))
                                        prop.SetValue(item, reader.GetInt16(reader.GetOrdinal(prop.Name)));
                                    else if (propType == typeof(byte))
                                        prop.SetValue(item, reader.GetByte(reader.GetOrdinal(prop.Name)));
                                    else
                                        throw new Exception($"Unsupported type: {propType}");
                                }
                            }

                            userList.Add(item);
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
    static public void SaveUserData<T>(List<T> dataList, string nameTable)
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
    }

    public static List<Credit> LoadCredits(string connectionString)
    {
        List<Credit> credits = new List<Credit>();

        try
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT ID, Money FROM credits";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            Credit credit = new Credit
                            {
                                ID = reader.GetInt32("ID"),
                                Money = reader.GetFloat("Money")
                            };

                            credits.Add(credit);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        credits.Sort((x, y) => x.ID.CompareTo(y.ID));
        return credits;
    }

    public static void SetCredits(string connectionString, Credit credit)
    {
        try
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = "INSERT INTO credits (ID, Money) VALUES (@ID, @Money)";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", credit.ID);
                    cmd.Parameters.AddWithValue("@Money", credit.Money);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("������ ������� ���������.");
                    }
                    else
                    {
                        Console.WriteLine("�� ������� �������� ������.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}

