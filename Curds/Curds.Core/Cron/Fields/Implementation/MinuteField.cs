using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Cron.Fields.Implementation
{
    using Cron.Abstraction;

    internal class MinuteField : BaseField
    {
        public MinuteField(IEnumerable<ICronRange> ranges)
            : base(ranges)
        { }
    }
}
