using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Infrastructure.Cron.Tokens.Tests.Cases.Month
{
    public class Spring : CronCase<Tokens.Month>
    {
        public override IEnumerable<Tokens.Month> Samples
        {
            get
            {
                yield return new Tokens.Month("03-May");
            }
        }

        public override IEnumerable<DateTime> TrueTimes
        {
            get
            {
                yield return DateTime.Parse("2017-03-01");
                yield return DateTime.Parse("2017-03-31");
                yield return DateTime.Parse("2017-04-01");
                yield return DateTime.Parse("2017-04-30");
                yield return DateTime.Parse("2017-05-01");
                yield return DateTime.Parse("2017-05-30");
            }
        }
        public override IEnumerable<DateTime> FalseTimes
        {
            get
            {
                yield return DateTime.Parse("2017-02-01");
                yield return DateTime.Parse("2017-02-28");
                yield return DateTime.Parse("2017-06-01");
            }
        }
    }
}
