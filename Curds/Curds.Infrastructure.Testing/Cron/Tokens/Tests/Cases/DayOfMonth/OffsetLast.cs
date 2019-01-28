using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Infrastructure.Cron.Tokens.Tests.Cases.DayOfMonth
{
    public class OffsetLast : CronCase<Tokens.DayOfMonth>
    {
        public override IEnumerable<Tokens.DayOfMonth> Samples
        {
            get
            {
                yield return new Tokens.DayOfMonth("L-5");
            }
        }

        public override IEnumerable<DateTime> TrueTimes
        {
            get
            {
                yield return DateTime.Parse("2018-07-26");
                yield return DateTime.Parse("2018-06-25");
                yield return DateTime.Parse("2018-02-23");
                yield return DateTime.Parse("2016-02-24");
            }
        }

        public override IEnumerable<DateTime> FalseTimes
        {
            get
            {
                yield return DateTime.Parse("2018-07-27");
                yield return DateTime.Parse("2018-07-25");
                yield return DateTime.Parse("2018-07-31");
                yield return DateTime.Parse("2018-06-26");
                yield return DateTime.Parse("2018-06-24");
                yield return DateTime.Parse("2018-06-30");
                yield return DateTime.Parse("2018-02-22");
                yield return DateTime.Parse("2018-02-24");
                yield return DateTime.Parse("2018-02-28");
                yield return DateTime.Parse("2016-02-23");
                yield return DateTime.Parse("2016-02-25");
                yield return DateTime.Parse("2016-02-29");
            }
        }
    }
}
