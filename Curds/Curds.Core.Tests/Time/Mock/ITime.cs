using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Time.Mock
{
    public class ITime : Curds.Time.Abstraction.ITime
    {
        public DateTimeOffset Fetch { get; private set; }

        public void SetPointInTime(DateTimeOffset newFixedPoint)
        {
            Fetch = newFixedPoint;
        }

        public void Reset()
        {
            Fetch = default(DateTimeOffset);
        }
    }
}
