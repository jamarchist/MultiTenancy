using System.Collections;
using PlayerEvaluator.Core;

namespace PlayerEvaluator.Custom.Ryan
{
    public class PlayerParser : IParser
    {
        public IPlayer Parse(Hashtable playerData)
        {
            return new Player(
                    (string)playerData["Name"],
                    (string)playerData["Team"],
                    (double[])playerData["WeeklyScores"],
                    (bool)playerData["IsHighlyOverrated"]
                );
        }
    }
}