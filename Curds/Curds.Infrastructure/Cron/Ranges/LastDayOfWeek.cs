using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Infrastructure.Cron.Ranges
{
    internal class LastDayOfWeek : Basic
    {
        public LastDayOfWeek(int dayOfWeek)
            : base(dayOfWeek, dayOfWeek)
        { }
    }
}
