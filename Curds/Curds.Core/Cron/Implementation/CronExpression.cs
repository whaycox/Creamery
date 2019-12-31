using System;
using System.Collections.Generic;
using System.Linq;

namespace Curds.Cron.Implementation
{
    using Abstraction;

    internal class CronExpression : ICronExpression
    {
        public List<ICronField> Fields { get; }

        public CronExpression(IEnumerable<ICronField> fields)
        {
            Fields = fields?.ToList() ?? throw new ArgumentNullException(nameof(fields));
        }

        public bool IsActive(DateTime testTime) => Fields.All(field => field.IsActive(testTime));
    }
}
