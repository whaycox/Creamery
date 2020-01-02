using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Cron.Ranges.Implementation
{
    using FieldDefinitions.Implementation;

    internal class NearestWeekdayRange : BaseRange<DayOfMonthFieldDefinition>
    {
        public int DayOfMonth { get; }

        public NearestWeekdayRange(DayOfMonthFieldDefinition fieldDefinition, int dayOfMonth)
            : base(fieldDefinition)
        {
            DayOfMonth = dayOfMonth;
        }

        public override bool IsActive(DateTime testTime)
        {
            int currentDay = FieldDefinition.SelectDatePart(testTime);

            switch (testTime.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                case DayOfWeek.Saturday:
                    return false;
                case DayOfWeek.Monday:
                    return currentDay == DayOfMonth || currentDay == DayOfMonth + 1;
                case DayOfWeek.Friday:
                    return currentDay == DayOfMonth || currentDay == DayOfMonth - 1;
                default:
                    return currentDay == DayOfMonth;
            }
        }
    }
}
