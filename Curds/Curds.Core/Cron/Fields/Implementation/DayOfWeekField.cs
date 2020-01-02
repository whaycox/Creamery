using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Cron.Fields.Implementation
{
    using Cron.Abstraction;

    internal class DayOfWeekField : BaseField
    {
        public DayOfWeekField(IEnumerable<ICronRange> ranges)
            : base(ranges)
        { }
    }
}
