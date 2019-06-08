using System;

namespace Curds.Cron.Range.Implementation
{
    using Domain;

    public class WeekdayNearest : Basic
    {
        public WeekdayNearest(int dayOfMonth)
            : base(dayOfMonth, dayOfMonth)
        { }

        public override bool Test(Token.Domain.Basic token, DateTime testTime)
        {
            bool toReturn = false;
            switch (testTime.DayOfWeek)
            {
                case System.DayOfWeek.Saturday:
                case System.DayOfWeek.Sunday:
                    return toReturn;
                case System.DayOfWeek.Monday:
                    toReturn = base.Test(token, testTime.AddDays(-1));
                    if (!toReturn)
                        toReturn = base.Test(token, testTime);
                    return toReturn;
                case System.DayOfWeek.Friday:
                    toReturn = base.Test(token, testTime.AddDays(1));
                    if (!toReturn)
                        toReturn = base.Test(token, testTime);
                    return toReturn;
                default:
                    return base.Test(token, testTime);
            }
        }
    }
}
