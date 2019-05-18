using System;

namespace Curds.Cron.Range.Implementation
{
    using Domain;

    public class LastDayOfMonth : Basic
    {
        public const int MaxOffset = 20;

        public int Offset { get; set; }

        public LastDayOfMonth()
            : this(0)
        { }

        public LastDayOfMonth(int offset)
            : base(28, 31)
        {
            if (offset < 0 || offset > MaxOffset)
                throw new ArgumentOutOfRangeException(nameof(offset));
            Offset = offset;
        }

        public override bool Test(Token.Domain.Basic token, DateTime testTime)
        {
            DateTime offsetDate = LastDay(testTime.Year, testTime.Month).AddDays(-Offset);
            return token.DatePart(testTime) == token.DatePart(offsetDate);
        }
        private DateTime LastDay(int year, int month) => new DateTime(year, month, DateTime.DaysInMonth(year, month));
    }
}
