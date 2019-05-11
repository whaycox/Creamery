using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Infrastructure.Cron.Token.Tests.Cases.DayOfWeek
{
    public class MonWedFri : CronCase<Token.DayOfWeek>
    {
        public override IEnumerable<Token.DayOfWeek> Samples
        {
            get
            {
                yield return new Token.DayOfWeek("Mon,Wed,5");
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
