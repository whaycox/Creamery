using System;

namespace Curds.Cron.Ranges.Implementation
{
    using FieldDefinitions.Implementation;

    internal class NthDayOfWeekRange : BaseRange<DayOfWeekFieldDefinition>
    {
        private const int DaysInWeek = 7;

        public int DayOfWeek { get; }
        public int NthValue { get; }

        public NthDayOfWeekRange(
            DayOfWeekFieldDefinition fieldDefinition,
            int dayOfWeek,
            int nthValue)
            : base(fieldDefinition)
        {
            DayOfWeek = dayOfWeek;
            NthValue = nthValue;
        }

        public override bool IsActive(DateTime testTime)
        {
            if (FieldDefinition.SelectDatePart(testTime) != DayOfWeek)
                return false;
            return IsNthDayOfWeek(testTime);
        }
        private bool IsNthDayOfWeek(DateTime testTime) => DayOfWeekOccurrenceThisMonth(testTime) == NthValue;
        private int DayOfWeekOccurrenceThisMonth(DateTime testTime) => (testTime.Day / DaysInWeek) + 1;
    }
}
