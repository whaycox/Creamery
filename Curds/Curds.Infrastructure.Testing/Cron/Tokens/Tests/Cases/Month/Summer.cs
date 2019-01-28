using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Infrastructure.Cron.Tokens.Tests.Cases.Month
{
    public class Summer : CronCase<Tokens.Month>
    {
        public override IEnumerable<Tokens.Month> Samples
        {
            get
            {
                yield return new Tokens.Month("Jun-Aug");
            }
        }

        public override IEnumerable<DateTime> TrueTimes
        {
            get
            {
                yield return DateTime.Parse("2017-06-01");
                yield return DateTime.Parse("2017-06-30");
                yield return DateTime.Parse("2017-07-01");
                yield return DateTime.Parse("2017-07-31");
                yield return DateTime.Parse("2017-08-01");
                yield return DateTime.Parse("2017-08-31");
            }
        }

        public override IEnumerable<DateTime> FalseTimes
        {
            get
            {
                yield return DateTime.Parse("2017-05-01");
                yield return DateTime.Parse("2017-05-31");
                yield return DateTime.Parse("2017-09-01");
            }
        }
    }
}
