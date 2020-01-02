using System.Collections.Generic;

namespace Curds.Cron.FieldFactories.Implementation
{
    using Abstraction;
    using Fields.Implementation;
    using FieldDefinitions.Implementation;

    internal class MonthFieldFactory : BaseFieldFactory<MonthFieldDefinition>
    {
        public MonthFieldFactory(ICronRangeFactory<MonthFieldDefinition> rangeFactory)
            : base(rangeFactory)
        { }

        protected override ICronField BuildField(IEnumerable<ICronRange> ranges) => new MonthField(ranges);
    }
}
