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

    void CreateTable()
    {
        try
        {
            connection.Open();
            Debug.Log("���������� � ���� ������!");

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
            Debug.Log("������� 'players' ������� ��� ��� ����������.");
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
}
