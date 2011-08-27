using System.Collections;

namespace PlayerEvaluator.Core
{
    public interface IParser
    {
        IPlayer Parse(Hashtable playerData);
    }
}