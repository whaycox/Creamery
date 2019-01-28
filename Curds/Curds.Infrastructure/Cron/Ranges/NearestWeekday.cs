using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Infrastructure.Cron.Ranges
{
    internal class NearestWeekday : Basic
    {
        public NearestWeekday(int dayOfMonth)
            : base(dayOfMonth, dayOfMonth)
        { }
    }
}
