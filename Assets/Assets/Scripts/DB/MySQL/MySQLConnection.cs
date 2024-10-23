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

    void Awake()
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

        CreateTable(@$"
                CREATE TABLE IF NOT EXISTS player (
                    id INT AUTO_INCREMENT PRIMARY KEY,
                    money FLOAT(20, 2),
                    settings VARCHAR(100)
                );", "player");
    }

    void CreateDatabase()
    {
        try
        {
            connection = new MySqlConnection(connectionString);
            connection.Open();
            Debug.Log("Подключено к MySQL серверу!");

            string dbName = "unity_db";

            string createDatabaseQuery = $"CREATE DATABASE IF NOT EXISTS {dbName};";
            MySqlCommand cmd = new MySqlCommand(createDatabaseQuery, connection);
            cmd.ExecuteNonQuery();
            Debug.Log($"База данных '{dbName}' создана или уже существует.");

            connection.ChangeDatabase(dbName);
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Ошибка создания базы данных: " + ex.Message);
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
            Debug.Log("Подключено к базе данных!");

            MySqlCommand cmd = new MySqlCommand(CreateQuery, connection);
            cmd.ExecuteNonQuery();
            Debug.Log($"Таблица '{nameTable}' создана или уже существует.");
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Ошибка создания таблицы: " + ex.Message);
        }
        finally
        {
            if (connection != null)
                connection.Close();
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
                        Console.WriteLine("Данные успешно вставлены.");
                    }
                    else
                    {
                        Console.WriteLine("Не удалось вставить данные.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    public static Player LoadPlayer(string connectionString)
    {
        Player player = new Player();

        try
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT ID, Money, Settings FROM player";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Player playerI = new Player
                            {
                                ID = reader.GetInt32("ID"),
                                Money = reader.GetFloat("Money"),
                                Settings = reader.GetString("Settings")
                            };

                            player = playerI;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        return player;
    }

    public static void SetPlayer(string connectionString, Player credit)
    {
        try
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = "INSERT INTO player (ID, Money, Settings) VALUES (@ID, @Money, @Settings)";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", credit.ID);
                    cmd.Parameters.AddWithValue("@Money", credit.Money);
                    cmd.Parameters.AddWithValue("@Settings", credit.Money);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Данные успешно вставлены.");
                    }
                    else
                    {
                        Console.WriteLine("Не удалось вставить данные.");
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

