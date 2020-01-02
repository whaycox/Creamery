using System.Collections.Generic;

namespace Curds.Cron.FieldFactories.Implementation
{
    using Abstraction;
    using Fields.Implementation;
    using FieldDefinitions.Implementation;

    internal class DayOfMonthFieldFactory : BaseFieldFactory<DayOfMonthFieldDefinition>
    {
        public DayOfMonthFieldFactory(ICronRangeFactory<DayOfMonthFieldDefinition> rangeFactory)
            : base(rangeFactory)
        { }

        protected override ICronField BuildField(IEnumerable<ICronRange> ranges) => new DayOfMonthField(ranges);
    }
}
