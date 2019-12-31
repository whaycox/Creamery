using System.Collections.Generic;

namespace Curds.Cron.FieldFactories.Implementation
{
    using Abstraction;
    using Cron.Abstraction;
    using Fields.Implementation;
    using RangeFactories.Abstraction;

    internal class DayOfWeekFieldFactory : BaseFieldFactory, IDayOfWeekFieldFactory
    {
        public DayOfWeekFieldFactory(IDayOfWeekRangeFactory rangeFactory)
            : base(rangeFactory)
        { }

        protected override ICronField BuildField(IEnumerable<ICronRange> ranges) => new DayOfWeekField(ranges);
    }
}
