using System;

namespace Curds.Cron.Range.Implementation
{
    using Domain;

    public class NthDayOfWeek : DayOfWeek
    {
        private const int MaxN = 5;

        public int NthValue { get; }

        public NthDayOfWeek(int dayOfWeek, int nthValue)
            : base(dayOfWeek, dayOfWeek)
        {
            if (nthValue <= 0 || nthValue > MaxN)
                throw new ArgumentOutOfRangeException(nameof(nthValue));
            NthValue = nthValue;
        }

        public override bool Test(Token.Domain.Basic token, DateTime testTime) => IsNthDayOfWeek(testTime) && base.Test(token, testTime);
        private bool IsNthDayOfWeek(DateTime testTime) => DayOfWeekOccurrenceThisMonth(testTime) == NthValue;
        private int DayOfWeekOccurrenceThisMonth(DateTime testTime) => (testTime.Day / DaysInWeek) + 1;
    }
}
