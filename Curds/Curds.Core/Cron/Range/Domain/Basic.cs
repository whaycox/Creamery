using System;

namespace Curds.Cron.Range.Domain
{
    public class Basic
    {
        public int Min { get; }
        public int Max { get; }

        public Basic(int min, int max)
        {
            if (min > max || max < min)
                throw new ArgumentOutOfRangeException("Cannot have inverted ranges");

            Min = min;
            Max = max;
        }

        public virtual bool IsValid(int absoluteMin, int absoluteMax)
        {
            if (Min < absoluteMin)
                return false;
            if (Max > absoluteMax)
                return false;
            return true;
        }
        public virtual bool Test(Token.Domain.Basic token, DateTime testTime)
        {
            int datePart = token.DatePart(testTime);
            return Min <= datePart && datePart <= Max;
        }

        public override string ToString()
        {
            if (Min == Max)
                return Min.ToString();
            else
                return $"{Min}-{Max}";
        }
    }
}
