using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Infrastructure.Cron.Tokens.Tests.Cases.Hour
{
    public class BusinessHours : CronCase<Tokens.Hour>
    {
        public override IEnumerable<Tokens.Hour> Samples
        {
            get
            {
                yield return new Tokens.Hour("9-17");
            }
        }

        public override IEnumerable<DateTime> TrueTimes
        {
            get
            {
                yield return DateTime.Parse("2017-09-01 09:00:00");
                yield return DateTime.Parse("2017-09-01 09:30:00");
                yield return DateTime.Parse("2017-09-01 10:00:00");
                yield return DateTime.Parse("2017-09-01 10:30:00");
                yield return DateTime.Parse("2017-09-01 11:00:00");
                yield return DateTime.Parse("2017-09-01 11:30:00");
                yield return DateTime.Parse("2017-09-01 12:00:00");
                yield return DateTime.Parse("2017-09-01 12:30:00");
                yield return DateTime.Parse("2017-09-01 13:00:00");
                yield return DateTime.Parse("2017-09-01 16:30:00");
                yield return DateTime.Parse("2017-09-01 17:00:00");
                yield return DateTime.Parse("2017-09-01 17:30:00");
            }
        }

        public override IEnumerable<DateTime> FalseTimes
        {
            get
            {
                yield return DateTime.Parse("2017-09-01 08:00:00");
                yield return DateTime.Parse("2017-09-01 08:30:00");
                yield return DateTime.Parse("2017-09-01 18:00:00");
            }
        }
    }
}
