using System.Collections.Generic;

namespace Curds.Cron.FieldFactories.Implementation
{
    using Abstraction;
    using Fields.Implementation;
    using FieldDefinitions.Implementation;

    internal class DayOfWeekFieldFactory : BaseFieldFactory<DayOfWeekFieldDefinition>
    {
        public DayOfWeekFieldFactory(ICronRangeFactory<DayOfWeekFieldDefinition> rangeFactory)
            : base(rangeFactory)
        { }

        protected override ICronField BuildField(IEnumerable<ICronRange> ranges) => new DayOfWeekField(ranges);
    }
}
