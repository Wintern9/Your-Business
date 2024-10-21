using MySql.Data.MySqlClient;
using UnityEngine;

public class MySQLConnection : MonoBehaviour
{
    private string server = "localhost"; 
    private string user = "root"; 
    private string password = "";
    private string connectionString;

    private MySqlConnection connection;

    void Start()
    {
        connectionString = $"Server={server}; UID={user}; password={password};";

        CreateDatabase();

        CreateTable();
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

    void CreateTable()
    {
        try
        {
            connection.Open();
            Debug.Log("Подключено к базе данных!");

            string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS players (
                    id INT AUTO_INCREMENT PRIMARY KEY,
                    name VARCHAR(100),
                    score INT,
                    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
                );
            ";

            MySqlCommand cmd = new MySqlCommand(createTableQuery, connection);
            cmd.ExecuteNonQuery();
            Debug.Log("Таблица 'players' создана или уже существует.");
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
}
