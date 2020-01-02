using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Curds.Cron.Fields.Implementation
{
    using Abstraction;

    internal abstract class BaseField : ICronField
    {
        public List<ICronRange> Ranges { get; }

        public BaseField(IEnumerable<ICronRange> ranges)
        {
            Ranges = ranges?.ToList() ?? throw new ArgumentNullException(nameof(ranges));
        }

        public bool IsActive(DateTime testTime) => Ranges.Any(range => range.IsActive(testTime));
    }
}
