using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

public class MySQLConnection : MonoBehaviour
{
    static private string server = "localhost";
    static private string user = "root";
    static private string password = "root";
    static public string connectionString;

    static private MySqlConnection connection;

    bool FirstBool = false;

    void Awake()
    {
        connectionString = $"Server={server}; port=3307; UID={user}; password={password}; Database=unity_db;";
        IntelizationDataBase();
    }

    void IntelizationDataBase()
    {
        CreateDatabase();

        CreateTable(@$"
                CREATE TABLE IF NOT EXISTS credits (
                    id INT AUTO_INCREMENT PRIMARY KEY,
                    money FLOAT(20, 2),
                    repaid INT
                );", "credits");

        CreateTable(@$"
                CREATE TABLE IF NOT EXISTS player (
                    id INT AUTO_INCREMENT PRIMARY KEY,
                    money FLOAT(20, 2),
                    settings VARCHAR(100)
                );", "player");

        CreateTable(@$"
                CREATE TABLE IF NOT EXISTS Names (
                male_name VARCHAR(50),
                female_name VARCHAR(50)
            );", "Names");

        CreateTable(@$"
                CREATE TABLE IF NOT EXISTS Surnames (
                male_surname VARCHAR(50),
                female_surname VARCHAR(50)
            );", "Surnames");
        
        CreateTable(@$"
                CREATE TABLE IF NOT EXISTS  rooms (
                        ID INT PRIMARY KEY,
                        Room INT NOT NULL,
                        Type INT NOT NULL,
                        Level INT NOT NULL,
                        PosX FLOAT NOT NULL,
                        PosY FLOAT NOT NULL
                    );", "rooms");

        if (FirstBool) {
            Rooms[] Roomz = new Rooms[18];
            for(int i = 0; i < 6; i++) 
            {              
                Roomz[i] = new Rooms(
                    id: i, 
                    room: (Room)1, 
                    type: (TypeRoom)1,
                    level: 0,  
                    posRoom: new Vector2(-10f * i, 0)    
                );  
            }

            for (int i = 0; i < 6; i++)
            {
                Roomz[i + 6] = new Rooms(
                    id: i + 6,
                    room: (Room)1,
                    type: (TypeRoom)1,
                    level: 0,
                    posRoom: new Vector2(-10f * i, 10)
                );
            }

            for (int i = 0; i < 6; i++)
            {
                Roomz[i + 12] = new Rooms(
                    id: i + 12,
                    room: (Room)1,
                    type: (TypeRoom)1,
                    level: 0,
                    posRoom: new Vector2(-10f * i, 20)
                );
            }
            SetRooms(Roomz);

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                InsertNames(conn);
                InsertSurnames(conn);
            }
        }
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

                string query = "SELECT ID, Money, Repaid FROM credits";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            Credit credit = new Credit
                            {
                                ID = reader.GetInt32("ID"),
                                Money = reader.GetFloat("Money"),
                                Repaid = reader.GetInt32("Repaid")
                            };

                            credits.Add(credit);
                        }
                    }
                }

                conn.Close();
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }
        credits.Sort((x, y) =>
        {
            int result = x.Repaid.CompareTo(y.Repaid);
            if (result == 0)
            {
                return x.ID.CompareTo(y.ID);
            }
            return result;
        });
        return credits;
    }

    public static void SetCredits(string connectionString, Credit credit)
    {
        try
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = "INSERT INTO credits (ID, Money, Repaid) VALUES (@ID, @Money, @Repaid)";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", credit.ID);
                    cmd.Parameters.AddWithValue("@Money", credit.Money);
                    cmd.Parameters.AddWithValue("@Repaid", credit.Repaid);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Debug.Log("Данные успешно вставлены.");
                    }
                    else
                    {
                        Debug.Log("Не удалось вставить данные.");
                    }
                }
                conn.Close();
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }
    }

    public static void UpdateCredits(string connectionString, Credit credit)
    {
        try
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = $"UPDATE credits SET Repaid = @Repaid WHERE ID = {credit.ID}";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Repaid", credit.Repaid);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Debug.Log("Данные успешно обновлены.");
                    }
                    else
                    {
                        Debug.Log("Не удалось обновить данные.");
                    }
                }
                conn.Close();
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error: " + ex.Message);
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

                string query = "SELECT ID, Money, Settings FROM player WHERE ID = 1";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) // Используем if, чтобы получить только первую запись
                        {
                            Player playerI = new Player
                            {
                                ID = reader.GetInt32("ID"),
                                Money = reader.IsDBNull(reader.GetOrdinal("Money")) ? 0 : reader.GetFloat("Money"),
                                Settings = reader.IsDBNull(reader.GetOrdinal("Settings")) ? string.Empty : reader.GetString("Settings")
                            };

                            player = playerI;
                        }
                        else
                        {
                            Debug.LogError("No player found with ID = 1.");
                        }
                    }
                }
                conn.Close();
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error: " + ex.Message);
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

                // Проверяем, существует ли запись с ID = 1
                string checkQuery = "SELECT COUNT(*) FROM player WHERE ID = 1";
                using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn))
                {
                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        // Если запись существует, обновляем её
                        string updateQuery = "UPDATE player SET Money = @Money, Settings = @Settings WHERE ID = 1";
                        using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn))
                        {
                            updateCmd.Parameters.AddWithValue("@Money", credit.Money);
                            updateCmd.Parameters.AddWithValue("@Settings", credit.Settings);

                            int rowsAffected = updateCmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                Debug.Log("Данные успешно обновлены.");
                            }
                            else
                            {
                                Debug.Log("Не удалось обновить данные.");
                            }
                        }
                    }
                    else
                    {
                        // Если запись не существует, вставляем новую
                        string insertQuery = "INSERT INTO player (ID, Money, Settings) VALUES (1, @Money, @Settings)";
                        using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn))
                        {
                            insertCmd.Parameters.AddWithValue("@Money", credit.Money);
                            insertCmd.Parameters.AddWithValue("@Settings", credit.Settings);

                            int rowsAffected = insertCmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                Debug.Log("Новая запись успешно вставлена.");
                            }
                            else
                            {
                                Debug.Log("Не удалось вставить новую запись.");
                            }
                        }
                    }
                }
                conn.Close();
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Ошибка: " + ex.Message);
        }
    }

    static void InsertNames(MySqlConnection connection)
    {
        string insertNamesQuery = @"
                INSERT INTO Names (male_name, female_name) VALUES
                ('Александр', 'Анна'),
                ('Дмитрий', 'Мария'),
                ('Сергей', 'Елена'),
                ('Михаил', 'Ольга'),
                ('Андрей', 'Наталья'),
                ('Иван', 'Татьяна'),
                ('Алексей', 'Екатерина'),
                ('Николай', 'Ирина'),
                ('Владимир', 'Светлана'),
                ('Павел', 'Людмила'),
                ('Артём', 'Юлия'),
                ('Максим', 'Оксана'),
                ('Олег', 'Анастасия'),
                ('Руслан', 'Вера'),
                ('Кирилл', 'Валентина'),
                ('Константин', 'Виктория'),
                ('Евгений', 'Алёна'),
                ('Роман', 'Ксения'),
                ('Илья', 'Полина'),
                ('Антон', 'Надежда');";

        using (var command = new MySqlCommand(insertNamesQuery, connection))
        {
            command.ExecuteNonQuery();
        }
    }

    static void InsertSurnames(MySqlConnection connection)
    {
        string insertSurnamesQuery = @"
                INSERT INTO Surnames (male_surname, female_surname) VALUES
                ('Иванов', 'Смирнова'),
                ('Кузнецов', 'Орлова'),
                ('Волков', 'Морозова'),
                ('Соколов', 'Васильева'),
                ('Новиков', 'Ковалева'),
                ('Попов', 'Павлова'),
                ('Лебедев', 'Григорьева'),
                ('Морозов', 'Андреева'),
                ('Петров', 'Михайлова'),
                ('Васильев', 'Николаева'),
                ('Гусев', 'Захарова'),
                ('Фёдоров', 'Дмитриева'),
                ('Киселёв', 'Соловьева'),
                ('Ефимов', 'Козлова'),
                ('Щербаков', 'Савельева'),
                ('Шишкин', 'Жукова'),
                ('Яковлев', 'Миронова'),
                ('Зуев', 'Игнатьева'),
                ('Голубев', 'Чернова'),
                ('Егоров', 'Рябова');";

        using (var command = new MySqlCommand(insertSurnamesQuery, connection))
        {
            command.ExecuteNonQuery();
        }
    }

    public static JobsNS LoadJobsNS(string connectionString)
    {
        List<JobsNames> jobsNames = new List<JobsNames>();
        List<JobsSurnames> jobsSurnames = new List<JobsSurnames>();

        try
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT male_name, female_name FROM Names";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            JobsNames names = new JobsNames
                            {
                                Male_name = reader.GetString("male_name"),
                                Female_name = reader.GetString("female_name")
                            };

                            jobsNames.Add(names);
                        }
                    }
                }

                string query2 = "SELECT male_surname, female_surname FROM Surnames";

                using (MySqlCommand cmd = new MySqlCommand(query2, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            JobsSurnames surnames = new JobsSurnames
                            {
                                Male_surname = reader.GetString("male_surname"),
                                Female_surname = reader.GetString("female_surname")
                            };

                            jobsSurnames.Add(surnames);
                        }
                    }
                }

                conn.Close();
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        JobsNS jobsNS = new JobsNS(jobsNames.ToArray(), jobsSurnames.ToArray());

        return jobsNS;
    }

    public static Rooms[] LoadRooms()
    {
        List<Rooms> roomsList = new List<Rooms>();

        try
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT ID, Room, Type, Level, PosX, PosY FROM rooms";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Rooms room = new Rooms
                            (
                                reader.GetInt32("ID"),
                                (Room)reader.GetInt32("Room"),
                                (TypeRoom)reader.GetInt32("Type"),
                                reader.GetInt32("Level"),
                                new Vector2(reader.GetFloat("PosX"), reader.GetFloat("PosY"))
                            );
                            roomsList.Add(room);
                        }
                    }
                }
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Ошибка загрузки комнат: " + ex.Message);
        }

        return roomsList.ToArray();
    }

    public static void SetRooms(Rooms[] rooms)
    {
        try
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = "INSERT INTO rooms (ID, Room, Type, Level, PosX, PosY) VALUES (@ID, @Room, @Type, @Level, @PosX, @PosY) " +
                               "ON DUPLICATE KEY UPDATE Room=@Room, Type=@Type, Level=@Level, PosX=@PosX, PosY=@PosY";

                foreach (Rooms room in rooms)
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", room.ID);
                        cmd.Parameters.AddWithValue("@Room", (int)room.room);
                        cmd.Parameters.AddWithValue("@Type", (int)room.type);
                        cmd.Parameters.AddWithValue("@Level", room.level);
                        cmd.Parameters.AddWithValue("@PosX", room.posRoom.x);
                        cmd.Parameters.AddWithValue("@PosY", room.posRoom.y);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Ошибка сохранения комнат: " + ex.Message);
        }
    }
}

