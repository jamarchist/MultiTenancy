using PlayerEvaluator.Core;

namespace PlayerEvaluator.Custom.Ryan
{
    public interface IRyanPlayer : IPlayer
    {
        bool IsHighlyOverrated { get; }
    }
}