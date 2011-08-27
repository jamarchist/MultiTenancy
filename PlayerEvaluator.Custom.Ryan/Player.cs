namespace PlayerEvaluator.Custom.Ryan
{
    public class Player : IRyanPlayer
    {
        private readonly string name;
        private readonly string team;
        private readonly double[] weeklyScores;
        private readonly bool isHighlyOverrated;

        public Player(string name, string team, double[] weeklyScores, bool isHighlyOverrated)
        {
            this.name = name;
            this.isHighlyOverrated = isHighlyOverrated;
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

        public bool IsHighlyOverrated
        {
            get { return isHighlyOverrated; }
        }
    }
}