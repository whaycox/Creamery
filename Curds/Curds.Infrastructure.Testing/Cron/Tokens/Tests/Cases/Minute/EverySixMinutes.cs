using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Infrastructure.Cron.Tokens.Tests.Cases.Minute
{
    public class EverySixMinutes : CronCase<Tokens.Minute>
    {
        public override IEnumerable<Tokens.Minute> Samples
        {
            get
            {
                yield return new Tokens.Minute("*/6");
            }
        }

        public override IEnumerable<DateTime> TrueTimes
        {
            get
            {
                yield return DateTime.Parse("2017-09-01 00:00:00");
                yield return DateTime.Parse("2017-09-01 00:30:00");
                yield return DateTime.Parse("2017-09-01 00:36:00");
                yield return DateTime.Parse("2017-09-01 00:42:00");
                yield return DateTime.Parse("2017-09-01 00:48:00");
                yield return DateTime.Parse("2017-09-01 00:54:00");
                yield return DateTime.Parse("2017-09-01 01:00:00");
            }
        }

        public override IEnumerable<DateTime> FalseTimes
        {
            get
            {
                yield return DateTime.Parse("2017-09-01 00:05:00");
                yield return DateTime.Parse("2017-09-01 00:10:00");
                yield return DateTime.Parse("2017-09-01 00:15:00");
                yield return DateTime.Parse("2017-09-01 00:20:00");
                yield return DateTime.Parse("2017-09-01 00:25:00");
            }
        }
    }
}
