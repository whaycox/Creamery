using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Infrastructure.Cron.Tests.Cases.CronExpression
{
    public class MinuteStep : CronCase<Cron.CronExpression>
    {
        public override IEnumerable<Cron.CronExpression> Samples
        {
            get
            {
                yield return new Cron.CronExpression("*/5 14 * * *");
            }
        }

        public override IEnumerable<DateTime> TrueTimes
        {
            get
            {
                yield return DateTime.Parse("2017-01-01 14:00:00");
                yield return DateTime.Parse("2017-01-01 14:05:00");
                yield return DateTime.Parse("2017-01-01 14:10:00");
                yield return DateTime.Parse("2017-01-01 14:15:00");
                yield return DateTime.Parse("2017-01-01 14:20:00");
                yield return DateTime.Parse("2017-01-01 14:25:00");
                yield return DateTime.Parse("2017-01-01 14:30:00");
                yield return DateTime.Parse("2017-01-01 14:35:00");
                yield return DateTime.Parse("2017-01-01 14:40:00");
                yield return DateTime.Parse("2017-01-01 14:45:00");
                yield return DateTime.Parse("2017-01-01 14:50:00");
                yield return DateTime.Parse("2017-01-01 14:55:00");
            }
        }

        public override IEnumerable<DateTime> FalseTimes
        {
            get
            {
                yield return DateTime.Parse("2017-01-01 13:55:00");
                yield return DateTime.Parse("2017-01-01 14:21:00");
                yield return DateTime.Parse("2017-01-01 14:44:00");
                yield return DateTime.Parse("2017-01-01 15:00:00");
            }
        }
    }
}
