using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Infrastructure.Cron.Ranges
{
    internal class NthDayOfWeek : Basic
    {
        public int NthValue { get; }

        public NthDayOfWeek(int dayOfWeek, int nthValue)
            : base(dayOfWeek, dayOfWeek)
        {
            NthValue = nthValue;
        }
    }
}
