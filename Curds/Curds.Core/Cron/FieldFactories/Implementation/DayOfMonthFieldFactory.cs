using System.Collections.Generic;

namespace Curds.Cron.FieldFactories.Implementation
{
    using Cron.Abstraction;
    using Fields.Implementation;
    using RangeFactories.Abstraction;

    internal class DayOfMonthFieldFactory : BaseFieldFactory
    {
        public DayOfMonthFieldFactory(IDayOfMonthRangeFactory rangeFactory)
            : base(rangeFactory)
        { }

        protected override ICronField BuildField(IEnumerable<ICronRange> ranges) => new DayOfMonthField(ranges);
    }
}
