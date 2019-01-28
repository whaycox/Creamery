using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Infrastructure.Cron.Tests.Cases.CronExpression
{
    public class TwelveNoon : CronCase<Cron.CronExpression>
    {
        public override IEnumerable<Cron.CronExpression> Samples
        {
            get
            {
                yield return new Cron.CronExpression("0 12 * * * ");
            }
        }

        public override IEnumerable<DateTime> TrueTimes
        {
            get
            {
                yield return DateTime.Parse("2017-01-01 12:00:00");
                yield return DateTime.Parse("2017-01-01 12:00:59");
            }
        }

        public override IEnumerable<DateTime> FalseTimes
        {
            get
            {
                yield return DateTime.Parse("2017-01-01 11:59:00");
                yield return DateTime.Parse("2017-01-01 11:59:59");
                yield return DateTime.Parse("2017-01-01 12:01:00");
                yield return DateTime.Parse("2017-01-01 12:01:59");
            }
        }
    }
}
