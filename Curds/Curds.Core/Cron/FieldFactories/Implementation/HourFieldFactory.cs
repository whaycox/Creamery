using System.Collections.Generic;

namespace Curds.Cron.FieldFactories.Implementation
{
    using Abstraction;
    using Fields.Implementation;
    using FieldDefinitions.Implementation;

    internal class HourFieldFactory : BaseFieldFactory<HourFieldDefinition>
    {
        public HourFieldFactory(ICronRangeFactory<HourFieldDefinition> rangeFactory)
            : base(rangeFactory)
        { }

        protected override ICronField BuildField(IEnumerable<ICronRange> ranges) => new HourField(ranges);
    }
}
