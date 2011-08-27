using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using PlayerEvaluator.Custom.Ryan;

namespace PlayerEvaluator.Tests
{
    [TestFixture]
    public class RyanTests
    {
        [Test]
        public void CanEvaluateBadPlayer()
        {
            var data = new Hashtable();
            data["Name"] = "Blah";
            data["Team"] = "Somebodies";
            data["WeeklyScores"] = new double[] { 7.8, 8.9, 12 };
            data["IsHighlyOverrated"] = false;

            var parser = new PlayerParser();
            var scorer = new Scorer();

            var evaluator = new PlayerEvaluator.Core.Evaluator(parser, scorer);
            var evaluation = evaluator.Evaluate(data);

            Assert.AreEqual("You should not acquire Blah.", evaluation);
        }
    }
}
