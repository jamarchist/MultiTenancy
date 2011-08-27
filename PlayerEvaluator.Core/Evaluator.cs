using System;
using System.Collections;
using System.Linq;

namespace PlayerEvaluator.Core
{
    public class Evaluator
    {
        private readonly IScorer scorer;
        private readonly IParser parser;

        public Evaluator(IParser parser, IScorer scorer)
        {
            this.parser = parser;
            this.scorer = scorer;
        }

        public string Evaluate(Hashtable playerData)
        {
            var player = parser.Parse(playerData);
            var dontAcquire = String.Format("You should not acquire {0}.", player.Name);
            var acquire = String.Format("You should acquire {0}.", player.Name);

            if (player.WeeklyScores.Sum() == 0)
            {
                return dontAcquire;
            }

            var clientScore = scorer.Score(player);
            if (clientScore > 50)
            {
                return acquire;
            }

            return dontAcquire;
        }
    }
}
