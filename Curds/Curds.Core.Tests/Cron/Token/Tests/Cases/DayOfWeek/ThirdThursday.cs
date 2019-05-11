using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Infrastructure.Cron.Token.Tests.Cases.DayOfWeek
{
    public class ThirdThursday : CronCase<Token.DayOfWeek>
    {
        public override IEnumerable<Token.DayOfWeek> Samples
        {
            get
            {
                yield return new Token.DayOfWeek("Thu#3");
                yield return new Token.DayOfWeek("4#3");
            }
        }

        public override IEnumerable<DateTime> TrueTimes
        {
            get
            {
                yield return DateTime.Parse("2018-11-15");
                yield return DateTime.Parse("2018-10-18");
                yield return DateTime.Parse("2018-9-20");
                yield return DateTime.Parse("2018-08-16");
                yield return DateTime.Parse("2018-07-19");
            }
        }

        public override IEnumerable<DateTime> FalseTimes
        {
            get
            {
                yield return DateTime.Parse("2018-11-08");
                yield return DateTime.Parse("2018-11-22");
                yield return DateTime.Parse("2018-11-16");
                yield return DateTime.Parse("2018-11-14");
                yield return DateTime.Parse("2018-10-11");
                yield return DateTime.Parse("2018-10-25");
                yield return DateTime.Parse("2018-10-17");
                yield return DateTime.Parse("2018-10-19");
                yield return DateTime.Parse("2018-09-13");
                yield return DateTime.Parse("2018-09-27");
                yield return DateTime.Parse("2018-09-19");
                yield return DateTime.Parse("2018-09-21");
                yield return DateTime.Parse("2018-08-09");
                yield return DateTime.Parse("2018-08-23");
                yield return DateTime.Parse("2018-08-15");
                yield return DateTime.Parse("2018-08-17");
            }
        }
    }
}
