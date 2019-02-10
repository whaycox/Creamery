using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Infrastructure.Cron.Tests.Cases.CronExpression
{
    public class EveryHourLastFriday : CronCase<Cron.Expression>
    {
        public override IEnumerable<Cron.Expression> Samples
        {
            get
            {
                yield return new Cron.Expression("0 * * * 5L");
            }
        }

        public override IEnumerable<DateTime> TrueTimes
        {
            get
            {
                yield return DateTime.Parse("2017-03-31 00:00:00");
                yield return DateTime.Parse("2017-03-31 12:00:00");
            }
        }

        public override IEnumerable<DateTime> FalseTimes
        {
            get
            {
                yield return DateTime.Parse("2017-03-31 00:30:00");
                yield return DateTime.Parse("2017-03-31 12:30:00");
                yield return DateTime.Parse("2017-03-24 00:00:00");
                yield return DateTime.Parse("2017-03-24 00:30:00");
                yield return DateTime.Parse("2017-03-24 12:00:00");
                yield return DateTime.Parse("2017-03-24 12:30:00");
            }
        }
    }
}
