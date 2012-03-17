using System.Collections;
using NUnit.Framework;
using PlayerEvaluator.Core;

namespace PlayerEvaluator.Tests
{
    [TestFixture]
    public class CoreTests
    {
        [Test]
        public void CanEvaluateNonPlayer()
        {
            var data = new Hashtable();
            data["Name"] = "Blah";
            data["Team"] = "Bears";
            data["WeeklyScores"] = new double[] { 0.0, 0.0, 0.0 };

            var scorer = new ThumbsUpScorer();
            var parser = new PlayerParser();

            var evaluator = new Evaluator(parser, scorer);
            var evaluation = evaluator.Evaluate(data);

            Assert.AreEqual("You should not acquire Blah.", evaluation);
        }

        [Test]
        public void CanEvaluatePlayer()
        {
            var data = new Hashtable();
            data["Name"] = "Blah";
            data["Team"] = "Bears";
            data["WeeklyScores"] = new double[] { 0.1, 0.0, 0.0 };

            var scorer = new ThumbsUpScorer();
            var parser = new PlayerParser();

            var evaluator = new Evaluator(parser, scorer);
            var evaluation = evaluator.Evaluate(data);

            Assert.AreEqual("You should acquire Blah.", evaluation);
        }

        private class ThumbsUpScorer : IScorer
        {
            public int Score(IPlayer player)
            {
                return 100;
            }
        }
    }
}
