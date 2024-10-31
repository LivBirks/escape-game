using System;

namespace EscapeGame
{
    public class User
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public int NumberOfPlays { get; set; }
        public double AverageScore { get; set; }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
            NumberOfPlays = 0;
            AverageScore = 0.0;
        }

        public void UpdateStats(int score)
        {
            NumberOfPlays++;
            AverageScore = (AverageScore * (NumberOfPlays - 1) + score) / NumberOfPlays;
        }
    }
}