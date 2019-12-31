using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Cron.Fields.Implementation
{
    using Cron.Abstraction;

    internal class HourField : BaseField
    {
        public HourField(IEnumerable<ICronRange> ranges)
            : base(ranges)
        { }
    }
}
