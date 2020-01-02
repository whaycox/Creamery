using System.Collections.Generic;

namespace Curds.Cron.FieldFactories.Implementation
{
    using Abstraction;
    using Fields.Implementation;
    using FieldDefinitions.Implementation;

    internal class MinuteFieldFactory : BaseFieldFactory<MinuteFieldDefinition>
    {
        public MinuteFieldFactory(ICronRangeFactory<MinuteFieldDefinition> rangeFactory)
            : base(rangeFactory)
        { }

        protected override ICronField BuildField(IEnumerable<ICronRange> ranges) => new MinuteField(ranges);
    }
}
