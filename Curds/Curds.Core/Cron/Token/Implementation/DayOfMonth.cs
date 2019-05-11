using System;
using System.Collections.Generic;

namespace Curds.Cron.Token.Implementation
{
    using Enumeration;
    using Range.Implementation;
    using Domain;

    public class DayOfMonth : Basic
    {
        public const int MinDayOfMonth = 1;
        public const int MaxDayOfMonth = 31;

        public override int AbsoluteMin => MinDayOfMonth;
        public override int AbsoluteMax => MaxDayOfMonth;

        public override Token TokenType => Token.DayOfMonth;

        public DayOfMonth(IEnumerable<Range.Domain.Basic> ranges)
            : base(ranges)
        { }

        protected override bool TestChild(DateTime testTime, Range.Domain.Basic childRange)
        {
            switch (childRange)
            {
                case LastDayOfMonth lastDayOfMonth:
                    return TestLastDayOfMonth(testTime, lastDayOfMonth);
                case WeekdayNearest nearestWeekday:
                    return TestNearestWeekday(testTime, nearestWeekday);
                default:
                    return base.TestChild(testTime, childRange);
            }
        }
        private bool TestLastDayOfMonth(DateTime testTime, LastDayOfMonth lastDayOfMonthRange)
        {
            DateTime offsetDate = LastDayOfMonth(testTime.Year, testTime.Month).AddDays(-lastDayOfMonthRange.Offset);
            return testTime.Day == offsetDate.Day;
        }
        private DateTime LastDayOfMonth(int year, int month) => new DateTime(year, month, DateTime.DaysInMonth(year, month));
        private bool TestNearestWeekday(DateTime testTime, WeekdayNearest nearestWeekdayRange)
        {
            bool toReturn = false;
            switch (testTime.DayOfWeek)
            {
                case System.DayOfWeek.Saturday:
                case System.DayOfWeek.Sunday:
                    return toReturn;
                case System.DayOfWeek.Monday:
                    toReturn = base.TestChild(testTime.AddDays(-1), nearestWeekdayRange);
                    if (!toReturn)
                        toReturn = base.TestChild(testTime, nearestWeekdayRange);
                    return toReturn;
                case System.DayOfWeek.Friday:
                    toReturn = base.TestChild(testTime.AddDays(1), nearestWeekdayRange);
                    if (!toReturn)
                        toReturn = base.TestChild(testTime, nearestWeekdayRange);
                    return toReturn;
                default:
                    return base.TestChild(testTime, nearestWeekdayRange);
            }
        }
    }
}
