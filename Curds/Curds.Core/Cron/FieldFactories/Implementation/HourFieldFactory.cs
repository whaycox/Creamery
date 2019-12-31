﻿using System.Collections.Generic;

namespace Curds.Cron.FieldFactories.Implementation
{
    using Abstraction;
    using Cron.Abstraction;
    using Fields.Implementation;
    using RangeFactories.Abstraction;

    internal class HourFieldFactory : BaseFieldFactory, IHourFieldFactory
    {
        public HourFieldFactory(IHourRangeFactory rangeFactory)
            : base(rangeFactory)
        { }

        protected override ICronField BuildField(IEnumerable<ICronRange> ranges) => new HourField(ranges);
    }
}
