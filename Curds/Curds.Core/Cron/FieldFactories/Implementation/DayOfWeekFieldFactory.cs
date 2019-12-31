using System.Collections.Generic;

namespace Curds.Cron.FieldFactories.Implementation
{
    using Cron.Abstraction;
    using Fields.Implementation;
    using RangeFactories.Abstraction;

    internal class DayOfWeekFieldFactory : BaseFieldFactory
    {
        public DayOfWeekFieldFactory(IDayOfWeekRangeFactory rangeFactory)
            : base(rangeFactory)
        { }

        protected override ICronField BuildField(IEnumerable<ICronRange> ranges) => new DayOfWeekField(ranges);
    }
}
