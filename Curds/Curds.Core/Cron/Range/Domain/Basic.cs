using System;

namespace Curds.Cron.Range.Domain
{
    public class Basic
    {
        private int Min { get; }
        private int Max { get; }

        public Basic(int min, int max)
        {
            if (min > max || max < min)
                throw new ArgumentOutOfRangeException("Cannot have inverted ranges");

            Min = min;
            Max = max;
        }

        public virtual bool Test(Token.Domain.Basic token, DateTime testTime, int testValue) => Min <= testValue && Max >= testValue;
    }
}
