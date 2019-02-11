using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Infrastructure.Cron.Tests.Cases.CronExpression
{
    public class BigExpression : CronCase<Cron.Expression>
    {
        public override IEnumerable<Cron.Expression> Samples
        {
            get
            {
                yield return new Cron.Expression("0,15,30,45 0,6,12,18 * 3,MAY-7,SEP 1-WED,FRI-SAT");
            }
        }

        public override IEnumerable<DateTime> TrueTimes
        {
            get
            {
                yield return DateTime.Parse("2017-09-01 00:45:00");
            }
        }

        public override IEnumerable<DateTime> FalseTimes
        {
            get
            {
                yield return DateTime.Parse("2017-04-01 00:00:00");
                yield return DateTime.Parse("2017-03-30 00:00:00");
                yield return DateTime.Parse("2017-08-01 00:00:00");
                yield return DateTime.Parse("2017-03-27 01:45:00");
                yield return DateTime.Parse("2017-03-27 00:46:00");
            }
        }
    }
}
