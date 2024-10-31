using System;
using Microsoft.Data.Sqlite;

namespace EscapeGame
{
    public class Database
    {
        private readonly string connectionString = "Data Source=./Data/EscapeGame.db;";
        public Database()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string createTableQuery = @"
                    CREATE TABLE IF NOT EXISTS Users (
                        Username TEXT PRIMARY KEY,
                        Password TEXT,
                        NumberOfPlays INTEGER,
                        AverageScore REAL
                    );";
                using (var command = new SqliteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public void AddUser(string username, string password)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string insertQuery = "INSERT INTO Users (Username, Password, NumberOfPlays, AverageScore) VALUES (@username, @password, 0, 0.0);";
                using (var command = new SqliteCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (SqliteException err)
                    {
                        Console.WriteLine("Error: " + err.Message);
                    }
                }
            }
        }

        public User GetUser(string username, string password)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string selectQuery = "SELECT * FROM Users WHERE Username = @username AND Password = @password;";
                using (var command = new SqliteCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User(
                                reader["Username"].ToString(),
                                reader["Password"].ToString()
                            )
                            {
                                NumberOfPlays = Convert.ToInt32(reader["NumberOfPlays"]),
                                AverageScore = Convert.ToDouble(reader["AverageScore"])
                            };
                        }
                    }
                }
            }
            return null; // returns null if no user is found
        }

        public void UpdateUserStats(string username, int numberOfPlays, double averageScore)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string updateQuery = "UPDATE Users SET NumberOfPlays = @numberOfPlays, Average = @averageScore WHERE Username = @username;";
                using (var command = new SqliteCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@numberOfPlays", numberOfPlays);
                    command.Parameters.AddWithValue("@averageScore", averageScore);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}