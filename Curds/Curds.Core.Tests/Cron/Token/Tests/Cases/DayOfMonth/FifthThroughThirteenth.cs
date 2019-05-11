using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Infrastructure.Cron.Token.Tests.Cases.DayOfMonth
{
    public class FifthThroughThirteenth : CronCase<Token.DayOfMonth>
    {
        public override IEnumerable<Token.DayOfMonth> Samples
        {
            get
            {
                yield return new Token.DayOfMonth("5-13");
            }
        }

        public override IEnumerable<DateTime> TrueTimes
        {
            get
            {
                yield return DateTime.Parse("2018-05-05");
                yield return DateTime.Parse("2018-05-06");
                yield return DateTime.Parse("2018-05-07");
                yield return DateTime.Parse("2018-05-08");
                yield return DateTime.Parse("2018-05-09");
                yield return DateTime.Parse("2018-05-10");
                yield return DateTime.Parse("2018-05-11");
                yield return DateTime.Parse("2018-05-12");
                yield return DateTime.Parse("2018-05-13");
            }
        }

        public override IEnumerable<DateTime> FalseTimes
        {
            get
            {
                yield return DateTime.Parse("2018-05-01");
                yield return DateTime.Parse("2018-05-02");
                yield return DateTime.Parse("2018-05-03");
                yield return DateTime.Parse("2018-05-04");
                yield return DateTime.Parse("2018-05-14");
                yield return DateTime.Parse("2018-05-15");
                yield return DateTime.Parse("2018-05-16");
                yield return DateTime.Parse("2018-05-17");
                yield return DateTime.Parse("2018-05-18");

            }
        }
    }
}
