using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Infrastructure.Cron.Token.Tests.Cases.DayOfMonth
{
    public class Last : CronCase<Token.DayOfMonth>
    {
        public override IEnumerable<Token.DayOfMonth> Samples
        {
            get
            {
                yield return new Token.DayOfMonth("L");
            }
        }

        public override IEnumerable<DateTime> TrueTimes
        {
            get
            {
                yield return DateTime.Parse("2018-02-28");
                yield return DateTime.Parse("2016-02-29");
                yield return DateTime.Parse("2018-03-31");
                yield return DateTime.Parse("2018-04-30");
            }
        }

        public override IEnumerable<DateTime> FalseTimes
        {
            get
            {
                yield return DateTime.Parse("2018-02-27");
                yield return DateTime.Parse("2016-02-28");
                yield return DateTime.Parse("2018-03-30");
                yield return DateTime.Parse("2018-04-29");
            }
        }
    }
}
