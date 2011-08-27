using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlayerEvaluator.Core
{
    public class Player : IPlayer
    {
        private readonly string name;
        private readonly string team;
        private readonly double[] weeklyScores;

        public Player(string name, string team, double[] weeklyScores)
        {
            this.name = name;
            this.weeklyScores = weeklyScores;
            this.team = team;
        }

        public string Name
        {
            get { return name; }
        }

        public string Team
        {
            get { return team; }
        }

        public double[] WeeklyScores
        {
            get { return weeklyScores; }
        }
    }
}
