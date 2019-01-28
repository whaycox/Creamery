using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Infrastructure.Cron.Tokens.Tests.Cases.Month
{
    public class Winter : CronCase<Tokens.Month>
    {
        public override IEnumerable<Tokens.Month> Samples
        {
            get
            {
                yield return new Tokens.Month("10-12");
            }
        }

        public override IEnumerable<DateTime> TrueTimes
        {
            get
            {
                yield return DateTime.Parse("2017-10-01");
                yield return DateTime.Parse("2017-10-31");
                yield return DateTime.Parse("2017-11-01");
                yield return DateTime.Parse("2017-11-30");
                yield return DateTime.Parse("2017-12-01");
                yield return DateTime.Parse("2017-12-31");
            }
        }

        public override IEnumerable<DateTime> FalseTimes
        {
            get
            {
                yield return DateTime.Parse("2017-09-30");
                yield return DateTime.Parse("2017-09-01");
                yield return DateTime.Parse("2018-01-01");
            }
        }
    }
}
