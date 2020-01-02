using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Curds.Cron.FieldFactories.Implementation
{
    using Cron.Abstraction;

    internal abstract class BaseFieldFactory<TFieldDefinition> : ICronFieldFactory<TFieldDefinition>
        where TFieldDefinition : ICronFieldDefinition
    {
        private const char RangeSeparator = ',';

        private ICronRangeFactory<TFieldDefinition> RangeFactory { get; }

        public BaseFieldFactory(ICronRangeFactory<TFieldDefinition> rangeFactory)
        {
            RangeFactory = rangeFactory;
        }

        public ICronField Parse(string field)
        {
            string[] ranges = field.Split(RangeSeparator);
            return BuildField(ranges.Select(range => RangeFactory.Parse(range)));
        }
        protected abstract ICronField BuildField(IEnumerable<ICronRange> ranges);
    }
}
