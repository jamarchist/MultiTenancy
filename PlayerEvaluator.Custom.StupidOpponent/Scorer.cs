using PlayerEvaluator.Core;

namespace PlayerEvaluator.Custom.StupidOpponent
{
    public class Scorer : IScorer
    {
        public int Score(IPlayer player)
        {
            if (player.Team == "Bears")
            {
                return 100;
            }

            return 0;
        }
    }
}
