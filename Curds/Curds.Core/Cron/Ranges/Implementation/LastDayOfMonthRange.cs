using System;

namespace Curds.Cron.Ranges.Implementation
{
    using FieldDefinitions.Implementation;

    internal class LastDayOfMonthRange : BaseRange<DayOfMonthFieldDefinition>
    {
        public LastDayOfMonthRange(DayOfMonthFieldDefinition fieldDefinition)
            : base(fieldDefinition)
        { }

        public override bool IsActive(DateTime testTime) => testTime.AddDays(1).Month != testTime.Month;
    }
}
