using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Infrastructure.Cron.Tokens.Tests.Cases.DayOfWeek
{
    public class MonWedFri : CronCase<Tokens.DayOfWeek>
    {
        public override IEnumerable<Tokens.DayOfWeek> Samples
        {
            get
            {
                yield return new Tokens.DayOfWeek("Mon,Wed,5");
            }
        }

        public override IEnumerable<DateTime> TrueTimes
        {
            get
            {
                yield return DateTime.Parse("2017-02-01");
                yield return DateTime.Parse("2017-02-03");
                yield return DateTime.Parse("2017-02-06");
            }
        }

        public override IEnumerable<DateTime> FalseTimes
        {
            get
            {
                yield return DateTime.Parse("2017-02-02");
                yield return DateTime.Parse("2017-02-04");
                yield return DateTime.Parse("2017-02-05");
                yield return DateTime.Parse("2017-02-07");
            }
        }
    }
}
