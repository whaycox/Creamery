using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Infrastructure.Cron.Token.Tests.Cases.DayOfMonth
{
    public class NearestWeekday : CronCase<Token.DayOfMonth>
    {
        public override IEnumerable<Token.DayOfMonth> Samples
        {
            get
            {
                yield return new Token.DayOfMonth("15W");
            }
        }

        public override IEnumerable<DateTime> TrueTimes
        {
            get
            {
                yield return DateTime.Parse("2018-11-15");
                yield return DateTime.Parse("2018-09-14");
                yield return DateTime.Parse("2018-07-16");
            }
        }

        public override IEnumerable<DateTime> FalseTimes
        {
            get
            {
                yield return DateTime.Parse("2018-11-14");
                yield return DateTime.Parse("2018-11-16");
                yield return DateTime.Parse("2018-09-17");
                yield return DateTime.Parse("2018-07-13");
            }
        }
    }
}
