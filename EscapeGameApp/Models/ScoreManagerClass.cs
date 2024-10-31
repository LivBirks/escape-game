using System;

namespace EscapeGame
{
    public class ScoreManager
    {
        private int currentScore;
        private int currentLevel;
        private const int BASE_LEVEL_POINTS = 100;
        private const int PENALTY_POINTS = 10;
        private const int HINT_PENALTY = 5;

        public ScoreManager()
        {
            currentScore = 0;
            currentLevel = 1;
        }

        public void StartLevel(int level)
        {
            currentLevel = level;
        }

        public int CompleteLevel(int attempts, int hintsUsed)
        {
            // Level multiplyer calculates the points
            int levelPoints = BASE_LEVEL_POINTS * currentLevel;

            // Apply pentalty if hint used and attempts
            int penalties = (attempts - 1) * PENALTY_POINTS;
            int hintPenalties = hintsUsed * HINT_PENALTY;

            // Combine points and penalties to get the final score
            int levelScore = Math.Max(0, levelPoints - penalties - hintPenalties);
            currentScore += levelScore;

            return levelScore;
        }

        public void ApplyPenalty(int penalty)
        {
            currentScore = Math.Max(0, currentScore - penalty);
        }

        public int GetCurrentScore() => currentScore;

        public void ResetScore()
        {
            currentScore = 0;
            currentLevel = 1;
        }
    }
}