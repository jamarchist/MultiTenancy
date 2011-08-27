using System.Collections;

namespace PlayerEvaluator.Core
{
    public class PlayerParser : IParser
    {
        public IPlayer Parse(Hashtable playerData)
        {
            return new Player(
                    (string)playerData["Name"],
                    (string)playerData["Team"],
                    (double[])playerData["WeeklyScores"]
                );
        }
    }
}