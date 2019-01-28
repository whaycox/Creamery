using Curds.Infrastructure.Cron.Ranges;
using System;
using System.Collections.Generic;

namespace Curds.Infrastructure.Cron.Tokens
{
    public class DayOfWeek : Basic
    {
        public override int AbsoluteMin => 0;
        public override int AbsoluteMax => 6;

        public DayOfWeek(string expressionPart)
            : base(expressionPart)
        { }

        protected override IEnumerable<Ranges.Basic> ParseExpressionPart(string expressionPart) => new Parsers.DayOfWeek().ParseRanges(expressionPart, this);

        protected override bool TestChild(DateTime testTime, Ranges.Basic childRange)
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
        private bool IsLastDayOfWeek(DateTime testTime) => testTime.AddDays(7).Month != testTime.Month;
        private int DayOfWeekOccurrenceThisMonth(DateTime testTime) => (testTime.Day / 7) + 1;

        protected override int RetrieveDatePart(DateTime testTime) => (int)testTime.DayOfWeek;
    }
}
