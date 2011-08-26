namespace PlayerEvaluator.Core
{
    public interface IPlayer
    {
        string Name { get; }
        string Team { get; }
        double[] WeeklyScores { get; }
    }
}
