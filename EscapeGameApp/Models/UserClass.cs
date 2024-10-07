using System;
using System.IO;

namespace EscapeGameApp.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int NumberOfPlays { get; set; }
        public double AverageScore { get; set; }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
            NumberOfPlays = 0;
            AverageScore = 0.0;
        }

        // You can overload methods here, for example:
        public void UpdateStats(int score)
        {
            NumberOfPlays++;
            AverageScore = (AverageScore * (NumberOfPlays - 1) + score) / NumberOfPlays;
        }
    }
}
