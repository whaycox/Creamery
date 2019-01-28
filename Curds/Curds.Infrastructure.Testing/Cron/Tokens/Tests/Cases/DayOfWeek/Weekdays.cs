using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Infrastructure.Cron.Tokens.Tests.Cases.DayOfWeek
{
    public class Weekdays : CronCase<Tokens.DayOfWeek>
    {
        public override IEnumerable<Tokens.DayOfWeek> Samples
        {
            get
            {
                yield return new Tokens.DayOfWeek("Mon-Fri");
            }
        }

        public override IEnumerable<DateTime> TrueTimes
        {
            get
            {
                yield return DateTime.Parse("2017-05-01");
                yield return DateTime.Parse("2017-05-02");
                yield return DateTime.Parse("2017-05-03");
                yield return DateTime.Parse("2017-05-04");
                yield return DateTime.Parse("2017-05-05");
            }
        }

        public override IEnumerable<DateTime> FalseTimes
        {
            get
            {
                yield return DateTime.Parse("2017-04-30");
                yield return DateTime.Parse("2017-05-06");
            }
        }
    }
}
