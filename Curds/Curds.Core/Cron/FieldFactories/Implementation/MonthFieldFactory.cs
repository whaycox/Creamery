using System.Collections.Generic;

namespace Curds.Cron.FieldFactories.Implementation
{
    using Abstraction;
    using Cron.Abstraction;
    using Fields.Implementation;
    using RangeFactories.Abstraction;

    internal class MonthFieldFactory : BaseFieldFactory, IMonthFieldFactory
    {
        public MonthFieldFactory(IMonthRangeFactory rangeFactory)
            : base(rangeFactory)
        { }

        protected override ICronField BuildField(IEnumerable<ICronRange> ranges) => new MonthField(ranges);
    }
}
