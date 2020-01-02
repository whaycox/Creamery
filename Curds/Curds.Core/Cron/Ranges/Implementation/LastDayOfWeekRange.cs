using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Cron.Ranges.Implementation
{
    using FieldDefinitions.Implementation;

    internal class LastDayOfWeekRange : BaseRange<DayOfWeekFieldDefinition>
    {
        private const int DaysInWeek = 7;

        public int DayOfWeek { get; }

        public LastDayOfWeekRange(DayOfWeekFieldDefinition fieldDefinition, int dayOfWeek)
            : base(fieldDefinition)
        {
            DayOfWeek = dayOfWeek;        
        }

        public override bool IsActive(DateTime testTime)
        {
            int datePart = FieldDefinition.SelectDatePart(testTime);
            if (datePart != DayOfWeek)
                return false;

            return IsLastDayOfWeek(testTime);
        }
        private bool IsLastDayOfWeek(DateTime testTime) => testTime.AddDays(DaysInWeek).Month != testTime.Month;
    }
}
