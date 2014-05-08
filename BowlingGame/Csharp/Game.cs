using System;
using System.Linq;

namespace BowlingGame
{
    public class Game
    {
        private const int GAME_FRAMES = 10;

        public int GetScore(string scoreLine)
        {
            var scoreCard = new ScoreCard(scoreLine);

            var gameScore = 0;
            var roll = 1;

            for (var frame = 1; frame <= GAME_FRAMES; frame++)
            {
                if (scoreCard.IsStrike(roll))
                {
                    gameScore += GetFrameScore(scoreCard, roll, roll + 1, roll + 2);
                }
                else if (scoreCard.IsSpare(roll + 1))
                {
                    gameScore += GetFrameScore(scoreCard, roll, roll + 1, roll + 2);
                    roll++;
                }
                else
                {
                    gameScore += GetFrameScore(scoreCard, roll, roll + 1);
                    roll++;
                }

                roll++;
            }

            return gameScore;
        }

        private int GetFrameScore(ScoreCard scoreCard, params int[] rolls)
        {
            return rolls.Sum(roll => scoreCard.GetPins(roll));
        }

        private class ScoreCard
        {
            private const char SPARE = '/';
            private const char STRIKE = 'X';
            private const int TOTAL_PINS = 10;
            private const char ZERO = '-';

            private readonly string m_ScoreLine;

            public ScoreCard(string scoreLine)
            {
                m_ScoreLine = scoreLine;
            }

            private char this[int roll]
            {
                get { return m_ScoreLine[roll - 1]; }
            }

            public int GetPins(int roll)
            {
                switch (this[roll])
                {
                    case STRIKE:
                        return TOTAL_PINS;
                    case SPARE:
                        return TOTAL_PINS - GetPins(roll - 1);
                    case ZERO:
                        return 0;
                    default:
                        return Int32.Parse(this[roll].ToString());
                }
            }

            public bool IsSpare(int roll)
            {
                return this[roll] == SPARE;
            }

            public bool IsStrike(int roll)
            {
                return this[roll] == STRIKE;
            }
        }
    }
}
