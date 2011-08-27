using System.Collections;
using NUnit.Framework;
using PlayerEvaluator.Core;
using PlayerEvaluator.Custom.StupidOpponent;

namespace PlayerEvaluator.Tests
{
    [TestFixture]
    public class OpponentTests
    {
        [Test]
        public void CanEvaluateBearsPlayer()
        {
            var data = new Hashtable();
            data["Name"] = "Blah";
            data["Team"] = "Bears";
            data["WeeklyScores"] = new double[] { 0.5, 0.3, 1.0 };

            var parser = new PlayerParser();
            var scorer = new Scorer();

            var evaluator = new Evaluator(parser, scorer);
            var evaluation = evaluator.Evaluate(data);

            Assert.AreEqual("You should acquire Blah.", evaluation);
        }
    }
}
