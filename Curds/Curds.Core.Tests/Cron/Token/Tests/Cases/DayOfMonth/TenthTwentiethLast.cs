using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Infrastructure.Cron.Token.Tests.Cases.DayOfMonth
{
    public class TenthTwentiethLast : CronCase<Token.DayOfMonth>
    {
        public override IEnumerable<Token.DayOfMonth> Samples
        {
            get
            {
                yield return new Token.DayOfMonth("10,20,L");
            }
        }

        public override IEnumerable<DateTime> TrueTimes
        {
            get
            {
                yield return DateTime.Parse("2018-04-10");
                yield return DateTime.Parse("2018-04-20");
                yield return DateTime.Parse("2018-04-30");
            }
        }

        public override IEnumerable<DateTime> FalseTimes
        {
            get
            {
                yield return DateTime.Parse("2018-04-08");
                yield return DateTime.Parse("2018-04-09");
                yield return DateTime.Parse("2018-04-11");
                yield return DateTime.Parse("2018-04-12");
                yield return DateTime.Parse("2018-04-18");
                yield return DateTime.Parse("2018-04-19");
                yield return DateTime.Parse("2018-04-21");
                yield return DateTime.Parse("2018-04-22");
                yield return DateTime.Parse("2018-04-28");
                yield return DateTime.Parse("2018-04-29");
            }
        }
    }
}
