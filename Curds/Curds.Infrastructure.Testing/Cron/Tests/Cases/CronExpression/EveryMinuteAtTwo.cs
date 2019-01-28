using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Infrastructure.Cron.Tests.Cases.CronExpression
{
    public class EveryMinuteAtTwo : CronCase<Cron.CronExpression>
    {
        public override IEnumerable<Cron.CronExpression> Samples
        {
            get
            {
                yield return new Cron.CronExpression(" * 14 * * *");
            }
        }

        public override IEnumerable<DateTime> TrueTimes
        {
            get
            {
                yield return DateTime.Parse("2017-01-01 14:00:00");
                yield return DateTime.Parse("2017-01-01 14:25:00");
                yield return DateTime.Parse("2017-01-01 14:45:00");
                yield return DateTime.Parse("2017-01-01 14:59:00");
            }
        }

        public override IEnumerable<DateTime> FalseTimes
        {
            get
            {
                yield return DateTime.Parse("2017-01-01 13:59:00");
                yield return DateTime.Parse("2017-01-01 15:00:00");
            }
        }
    }
}
