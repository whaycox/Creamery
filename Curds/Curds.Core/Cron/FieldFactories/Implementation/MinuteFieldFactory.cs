using System.Collections.Generic;

namespace Curds.Cron.FieldFactories.Implementation
{
    using Abstraction;
    using Cron.Abstraction;
    using Fields.Implementation;
    using RangeFactories.Abstraction;

    internal class MinuteFieldFactory : BaseFieldFactory, IMinuteFieldFactory
    {
        public MinuteFieldFactory(IMinuteRangeFactory rangeFactory)
            : base(rangeFactory)
        { }

        protected override ICronField BuildField(IEnumerable<ICronRange> ranges) => new MinuteField(ranges);
    }
}
