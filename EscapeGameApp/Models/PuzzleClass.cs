using System;

namespace EscapeGame
{
    public class Puzzle
    {
        public string Description { get; set; }

        public Puzzle(string description)
        {
            Description = description;
        }

        public bool Solve(string correctAnswer)
        {
            Console.WriteLine("Puzzle: " + Description);
            Console.WriteLine("Enter your answer:");
            string answer = Console.ReadLine().ToLower();

            return answer == correctAnswer;
        }
    }
}