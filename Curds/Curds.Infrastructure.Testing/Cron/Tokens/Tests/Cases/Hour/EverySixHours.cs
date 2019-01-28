using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Infrastructure.Cron.Tokens.Tests.Cases.Hour
{
    public class EverySixHours : CronCase<Tokens.Hour>
    {
        public override IEnumerable<Tokens.Hour> Samples
        {
            get
            {
                yield return new Tokens.Hour("*/6");
            }
        }

        public override IEnumerable<DateTime> TrueTimes
        {
            get
            {
                yield return DateTime.Parse("2017-09-01 06:00:00");
                yield return DateTime.Parse("2017-09-01 12:00:00");
                yield return DateTime.Parse("2017-09-01 00:00:00");
                yield return DateTime.Parse("2017-09-01 18:00:00");
            }
        }

        public override IEnumerable<DateTime> FalseTimes
        {
            get
            {
                yield return DateTime.Parse("2017-09-01 01:00:00");
                yield return DateTime.Parse("2017-09-01 02:00:00");
                yield return DateTime.Parse("2017-09-01 03:00:00");
                yield return DateTime.Parse("2017-09-01 04:00:00");
                yield return DateTime.Parse("2017-09-01 05:00:00");
                yield return DateTime.Parse("2017-09-01 07:00:00");
                yield return DateTime.Parse("2017-09-01 08:00:00");
                yield return DateTime.Parse("2017-09-01 09:00:00");
                yield return DateTime.Parse("2017-09-01 10:00:00");
                yield return DateTime.Parse("2017-09-01 11:00:00");
                yield return DateTime.Parse("2017-09-01 13:00:00");
                yield return DateTime.Parse("2017-09-01 14:00:00");
                yield return DateTime.Parse("2017-09-01 15:00:00");
                yield return DateTime.Parse("2017-09-01 16:00:00");
                yield return DateTime.Parse("2017-09-01 17:00:00");
                yield return DateTime.Parse("2017-09-01 19:00:00");
                yield return DateTime.Parse("2017-09-01 20:00:00");
                yield return DateTime.Parse("2017-09-01 21:00:00");
                yield return DateTime.Parse("2017-09-01 22:00:00");
                yield return DateTime.Parse("2017-09-01 23:00:00");
            }
        }
    }
}
