using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlayerEvaluator.Core
{
    public interface IScorer
    {
        int Score(IPlayer player);
    }
}
