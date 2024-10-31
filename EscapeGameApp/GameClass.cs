using System;

namespace EscapeGame
{
    public class Game
    {
        private User currentUser;
        private Database database;
        private Hallway hallway;
        private ScoreManager scoreManager;

        public Game()
        {
            scoreManager = new ScoreManager();
            database = new Database();
            hallway = new Hallway();
        }

        public void MainMenu()
        {
            Console.WriteLine("Welcome to the Escape Game!");
            Console.WriteLine("1. Log In");
            Console.WriteLine("2. Create User");
            Console.WriteLine("3. Exit");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    LogIn();
                    break;
                case "2":
                    CreateUser();
                    break;
                case "3":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    MainMenu();
                    break;
            }
        }

        private void LogIn()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            currentUser = database.GetUser(username, password);
            if (currentUser != null)
            {
                UserMenu(); // takes user to another menu once logged in
            }
            else
            {
                Console.WriteLine("Invalid login. Try again.");
                MainMenu();
            }
        }

        private void CreateUser()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            database.AddUser(username, password);
            Console.WriteLine("User created successfully! You can now log in to play the game.");
            MainMenu();
        }

        private void ShowUserStats()
        {
            Console.WriteLine($"\nUser Statistics for {currentUser.Username}");
            Console.WriteLine($"Number of plays: {currentUser.NumberOfPlays}");
            Console.WriteLine($"Average score: {currentUser.AverageScore:F2}");
        }

        private void UserMenu()
        {
            while (true)
            {
                Console.WriteLine("\n=== Main Menu ===");
                Console.WriteLine("1. Play Game");
                Console.WriteLine("2. View Statistics");
                Console.WriteLine("3. Logout");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("\nWelcome to the Haunted Manor Escape!");
                        Console.WriteLine("You find yourself trapped in an old manor house.");
                        Console.WriteLine("Your goal is to escape by solving puzzles in each room.");
                        Console.WriteLine("Good luck!\n");
                        hallway.Play();
                        break;
                    case "2":
                        ShowUserStats();
                        break;
                    case "3":
                        currentUser = null;
                        MainMenu();
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }
    }
}