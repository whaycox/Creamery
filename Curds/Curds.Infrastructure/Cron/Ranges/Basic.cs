using System;
using System.Collections.Generic;

namespace Curds.Infrastructure.Cron.Ranges
{
    public class Basic
    {
        public int Min { get; }
        public int Max { get; }

        public Basic(int min, int max)
        {
            if (min > max || max < min)
                throw new InvalidOperationException("Cannot have inverted ranges");

            Min = min;
            Max = max;
        }

        public virtual bool Probe(int testValue) => Min <= testValue && Max >= testValue;
    }
}
