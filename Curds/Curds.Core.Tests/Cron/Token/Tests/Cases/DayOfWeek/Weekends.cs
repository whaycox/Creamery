using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Infrastructure.Cron.Token.Tests.Cases.DayOfWeek
{
    public class Weekends : CronCase<Token.DayOfWeek>
    {
        public override IEnumerable<Token.DayOfWeek> Samples
        {
            get
            {
                yield return new Token.DayOfWeek("0,6");
            }
        }

        public override IEnumerable<DateTime> TrueTimes
        {
            get
            {
                yield return DateTime.Parse("2017-09-02");
                yield return DateTime.Parse("2017-09-03");
            }
        }

        public override IEnumerable<DateTime> FalseTimes
        {
            get
            {
                yield return DateTime.Parse("2017-09-01");
                yield return DateTime.Parse("2017-09-04");
            }
        }
    }
}
