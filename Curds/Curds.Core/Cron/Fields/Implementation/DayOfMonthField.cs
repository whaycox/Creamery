using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Cron.Fields.Implementation
{
    using Cron.Abstraction;

    internal class DayOfMonthField : BaseField
    {
        public DayOfMonthField(IEnumerable<ICronRange> ranges)
            : base(ranges)
        { }
    }
}
