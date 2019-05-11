using System;
using System.Collections.Generic;

namespace Curds.Cron.Token.Implementation
{
    using Enumeration;
    using Range.Implementation;
    using Domain;

    public class DayOfWeek : Basic
    {
        public const int MinDayOfWeek = 0;
        public const int MaxDayOfWeek = 6;
        public const int DaysInWeek = 7;

        public override int AbsoluteMin => MinDayOfWeek;
        public override int AbsoluteMax => MaxDayOfWeek;

        public override Token TokenType => Token.DayOfWeek;

        public DayOfWeek(IEnumerable<Range.Domain.Basic> ranges)
            : base(ranges)
        { }

        protected override bool TestChild(DateTime testTime, Range.Domain.Basic childRange)
        {
            switch (childRange)
            {
                case LastDayOfWeek lastDayOfWeek:
                    if (!IsLastDayOfWeek(testTime))
                        return false;
                    break;
                case NthDayOfWeek nthDayOfWeek:
                    if (DayOfWeekOccurrenceThisMonth(testTime) != nthDayOfWeek.NthValue)
                        return false;
                    break;
            }
            return base.TestChild(testTime, childRange);
        }
        private bool IsLastDayOfWeek(DateTime testTime) => testTime.AddDays(DaysInWeek).Month != testTime.Month;
        private int DayOfWeekOccurrenceThisMonth(DateTime testTime) => (testTime.Day / DaysInWeek) + 1;
    }
}
