using System;

namespace Curds.Cron.Ranges.Implementation
{
    using Cron.Abstraction;

    internal class SingleValueRange<TFieldDefinition> : BaseRange<TFieldDefinition>
        where TFieldDefinition : ICronFieldDefinition
    {
        public int Value { get; }

        public SingleValueRange(
            TFieldDefinition fieldDefinition,
            int value)
            : base(fieldDefinition)
        {
            Value = value;
        }

        public override bool IsActive(DateTime testTime) => FieldDefinition.SelectDatePart(testTime) == Value;
    }
}
