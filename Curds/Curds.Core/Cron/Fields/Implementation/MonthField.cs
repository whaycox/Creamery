using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Cron.Fields.Implementation
{
    using Cron.Abstraction;

    internal class MonthField : BaseField
    {
        public MonthField(IEnumerable<ICronRange> ranges)
            : base(ranges)
        { }
    }
}
