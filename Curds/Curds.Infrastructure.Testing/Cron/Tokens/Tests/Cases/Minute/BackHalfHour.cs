﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Infrastructure.Cron.Tokens.Tests.Cases.Minute
{
    public class BackHalfHour : CronCase<Tokens.Minute>
    {
        public override IEnumerable<Tokens.Minute> Samples
        {
            get
            {
                yield return new Tokens.Minute("30-55");
            }
        }

        public override IEnumerable<DateTime> TrueTimes
        {
            get
            {
                yield return DateTime.Parse("2017-09-01 12:30:00");
                yield return DateTime.Parse("2017-09-01 12:31:00");
                yield return DateTime.Parse("2017-09-01 12:54:00");
                yield return DateTime.Parse("2017-09-01 12:55:00");
            }
        }

        public override IEnumerable<DateTime> FalseTimes
        {
            get
            {
                yield return DateTime.Parse("2017-09-01 12:29:00");
                yield return DateTime.Parse("2017-09-01 12:29:59");
                yield return DateTime.Parse("2017-09-01 12:56:00");
            }
        }
    }
}
