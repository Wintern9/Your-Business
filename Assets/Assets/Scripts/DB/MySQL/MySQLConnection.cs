using MySql.Data.MySqlClient;
using UnityEngine;

public class MySQLConnection : MonoBehaviour
{
    private string server = "localhost";
    private string database = "yourbusiness";
    private string user = "user_business";
    private string password = "business.123";
    private string connectionString;

    private MySqlConnection connection;

    void Start()
    {
        connectionString = $"Server={server}; database={database}; UID={user}; password={password};";
        ConnectToDatabase();
    }

    void ConnectToDatabase()
    {
        try
        {
            connection = new MySqlConnection(connectionString);
            connection.Open();
            Debug.Log("Подключение успешно!");

            string query = "SELECT * FROM credits";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Debug.Log(reader.GetString(0));
            }
            reader.Close();
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Ошибка подключения: " + ex.Message);
        }
        finally
        {
            if (connection != null)
                connection.Close();
        }
    }
}
