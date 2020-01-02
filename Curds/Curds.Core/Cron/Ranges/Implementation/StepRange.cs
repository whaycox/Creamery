using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Cron.Ranges.Implementation
{
    using Cron.Abstraction;

    internal class StepRange<TFieldDefinition> : BaseRange<TFieldDefinition>
        where TFieldDefinition : ICronFieldDefinition
    {
        public int StepValue { get; }

        public StepRange(TFieldDefinition fieldDefinition, int stepValue)
            : base(fieldDefinition)
        {
            StepValue = stepValue;
        }

        public override bool IsActive(DateTime testTime)
        {
            int datePart = FieldDefinition.SelectDatePart(testTime);
            return ((datePart + FieldDefinition.AbsoluteMin) % StepValue) == 0;
        }
    }
}
