using System;

namespace Curds.Cron.Ranges.Implementation
{
    using FieldDefinitions.Implementation;

    internal class LastDayOfMonthRange : BaseRange<DayOfMonthFieldDefinition>
    {
        public int Offset { get; }

        public LastDayOfMonthRange(DayOfMonthFieldDefinition fieldDefinition, int offset)
            : base(fieldDefinition)
        {
            Offset = offset;
        }

        public override bool IsActive(DateTime testTime)
        {
            int datePart = FieldDefinition.SelectDatePart(testTime);
            int expectedDatePart = CalculateExpectedDatePart(testTime);
            return datePart == expectedDatePart;
        }
        private int CalculateExpectedDatePart(DateTime testTime)
        {
            DateTime offsetTime = LastDay(testTime).AddDays(-Offset);
            if (offsetTime.Month != testTime.Month)
                return 1;
            else
                return FieldDefinition.SelectDatePart(offsetTime);
        }
        private DateTime LastDay(DateTime testTime) => new DateTime(testTime.Year, testTime.Month, DaysInMonth(testTime));
        private int DaysInMonth(DateTime testTime) => DateTime.DaysInMonth(testTime.Year, testTime.Month);
    }
}
