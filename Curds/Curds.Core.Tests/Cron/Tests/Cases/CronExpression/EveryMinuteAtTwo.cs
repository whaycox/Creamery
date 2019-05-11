using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Infrastructure.Cron.Tests.Cases.CronExpression
{
    public class EveryMinuteAtTwo : CronCase<Cron.Expression>
    {
        public override IEnumerable<Cron.Expression> Samples
        {
            get
            {
                yield return new Cron.Expression(" * 14 * * *");
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
