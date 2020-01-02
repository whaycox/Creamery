using System;

namespace Curds.Cron.Ranges.Implementation
{
    using Cron.Abstraction;

    internal class RangeValueRange<TFieldDefinition> : BaseRange<TFieldDefinition>
        where TFieldDefinition : ICronFieldDefinition
    {
        public int Low { get; }
        public int High { get; }

        public RangeValueRange(TFieldDefinition fieldDefinition, int low, int high)
            : base(fieldDefinition)
        {
            Low = low;
            High = high;
        }

        public override bool IsActive(DateTime testTime)
        {
            int datePart = FieldDefinition.SelectDatePart(testTime);
            return Low <= datePart && datePart <= High;
        }
    }
}
