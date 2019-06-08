using System;

namespace Curds.Cron.Range.Implementation
{
    using Domain;

    public class LastDayOfWeek : DayOfWeek
    {
        public LastDayOfWeek(int dayOfWeek)
            : base(dayOfWeek, dayOfWeek)
        { }

        public override bool Test(Token.Domain.Basic token, DateTime testTime) => IsLastDayOfWeek(testTime) && base.Test(token, testTime);
        private bool IsLastDayOfWeek(DateTime testTime) => testTime.AddDays(DaysInWeek).Month != testTime.Month;
    }
}
