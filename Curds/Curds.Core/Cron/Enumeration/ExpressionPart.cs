using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Cron.Enumeration
{
    public enum ExpressionPart
    {
        Minute = 0,
        Hour = 1,
        DayOfMonth = 2,
        Month = 3,
        DayOfWeek = 4,
    }
}
