using System;
using System.Linq;
using PlayerEvaluator.Core;

namespace PlayerEvaluator.Custom.Ryan
{
    public class Scorer : IScorer
    {
        public int Score(IPlayer player)
        {
            var customPlayer = player as IRyanPlayer;
            var score = 50;
            var averageWeek = customPlayer.WeeklyScores.Sum() / customPlayer.WeeklyScores.Count();

            var multiplier = averageWeek - 10;
            var adjustedScore = score + (customPlayer.WeeklyScores.Count() * multiplier);

            if (customPlayer.IsHighlyOverrated)
            {
                adjustedScore -= 10;
            }

            return Convert.ToInt32(adjustedScore);
        }
    }
}
