using System;
using System.Collections.Generic;
using System.Linq;

namespace EscapeGame
{
    public abstract class Room
    {
        protected int points = 100; // each room starts with 100 points
        protected static int currentLevel = 1;
        public abstract void Play();

        protected void DeductPoints(int amount)
        {
            points = Math.Max(0, points - amount);
            Console.WriteLine($"Lost {amount} points. Current room points: {points}");
        }
    }

    public class Hallway : Room
    {
        private KitchenDoor kitchenDoor = new KitchenDoor();
        private LibraryDoor libraryDoor = new LibraryDoor();
        private FrontDoor frontDoor = new FrontDoor();

        public override void Play()
        {
            Console.WriteLine("You are in the Hallway. Where do you want to go?");
            Console.WriteLine("1. Kitchen");
            Console.WriteLine("2. Library");
            Console.WriteLine("3. Front Door");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    kitchenDoor.Unlock();
                    Kitchen kitchen = new Kitchen();
                    kitchen.Play();
                    break;
                case "2":
                    libraryDoor.Unlock();
                    Library library = new Library();
                    library.Play();
                    break;
                case "3":
                    frontDoor.Unlock();
                    Study study = new Study();
                    study.Play();
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    Play();
                    break;
            }
        }
    }

    public class Kitchen : Room
    {
        private List<string> puzzles = new List<string>
        {
            "What has keys but no locks, space but no room, and you can enter but not go in?",
            "What gets wetter and wetter the more it dries?",
            "Feed me and I live, give me water and I die. What am I?"
        };

        private List<string> answers = new List<string>
        {
            "keyboard",
            "towel",
            "fire"
        };

        public override void Play()
        {
            Console.WriteLine($"\n=== Level {currentLevel}: The Kitchen ===");
            Console.WriteLine("You need to solve the kitchen puzzles to proceed.");

            for (int i = 0; i < puzzles.Count; i++)
            {
                int attempts = 0;
                while (attempts < 3)
                {
                    Puzzle puzzle = new Puzzle(puzzles[i]);
                    string answer = answers[i];

                    Console.WriteLine($"\nPuzzle {i + 1}:");
                    if (puzzle.Solve(answer))
                    {
                        break;
                    }
                    attempts++;
                    DeductPoints(10);
                }
            }

            Console.WriteLine($"\nKitchen completed! Points earned: {points}");
            currentLevel++;

            Hallway hallway = new Hallway();
            hallway.Play();
        }
    }

    public class Library : Room
    {
        private HiddenDoor hiddenDoor = new HiddenDoor();
        private List<Book> books = new List<Book>();

        public Library()
        {
            books.Add(new Book("Red Book", false));
            books.Add(new Book("Blue Book", true));
            books.Add(new Book("Green Book", false));
        }

        public override void Play()
        {
            Console.WriteLine($"\n=== Level {currentLevel}: The Library ===");
            Console.WriteLine("Find the correct book to reveal the hidden door!");

            while (hiddenDoor.IsHidden)
            {
                Console.WriteLine("\nAvailable books:");
                for (int i = 0; i < books.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {books[i].Title}");
                }

                Console.Write("Choose a book (1-3): ");
                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= 3)
                {
                    if (books[choice - 1].IsKey)
                    {
                        hiddenDoor.Reveal();
                        hiddenDoor.Unlock();
                    }
                    else
                    {
                        Console.WriteLine("Wrong book!");
                        DeductPoints(10);
                    }
                }
            }

            Console.WriteLine($"\nLibrary completed! Points earned: {points}");
            currentLevel++;
            Hallway hallway = new Hallway();
            hallway.Play();
        }
    }

    public class Study : Room
    {
        private List<string> riddles = new List<string>
        {
            "I speak without a mouth and hear without ears. I have no body, but I come alive with wind. What am I?",
            "The more you take, the more you leave behind. What am I?"
        };

        private List<string> answers = new List<string>
        {
            "echo",
            "footsteps"
        };

        public override void Play()
        {
            Console.WriteLine($"\n=== Level {currentLevel}: The Study ===");
            Console.WriteLine("Solve the final riddles to escape!");

            foreach (var (riddle, answer) in riddles.Zip(answers, (r, a) => (r, a)))
            {
                int attempts = 0;
                while (attempts < 3)
                {
                    Puzzle puzzle = new Puzzle(riddle);
                    if (puzzle.Solve(answer))
                    {
                        break;
                    }
                    attempts++;
                    DeductPoints(15);
                }
            }

            Console.WriteLine("\nCongratulations! You've completed all levels!");
            Console.WriteLine($"Final room points: {points}");
            Console.WriteLine("You successfully escaped the manor!");

            // reset the level counter for the next game
            currentLevel = 1;
        }
    }
}

