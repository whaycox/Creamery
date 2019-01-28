﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Infrastructure.Cron.Tokens.Tests.Cases.DayOfWeek
{
    public class LastWednesday : CronCase<Tokens.DayOfWeek>
    {
        public override IEnumerable<Tokens.DayOfWeek> Samples
        {
            get
            {
                yield return new Tokens.DayOfWeek("WedL");
                yield return new Tokens.DayOfWeek("3L");
            }
        }

        public override IEnumerable<DateTime> TrueTimes
        {
            get
            {
                yield return DateTime.Parse("2018-11-28");
                yield return DateTime.Parse("2018-10-31");
                yield return DateTime.Parse("2018-12-26");
            }
        }

        public override IEnumerable<DateTime> FalseTimes
        {
            get
            {
                yield return DateTime.Parse("2018-11-07");
                yield return DateTime.Parse("2018-11-14");
                yield return DateTime.Parse("2018-11-21");
                yield return DateTime.Parse("2018-10-03");
                yield return DateTime.Parse("2018-10-10");
                yield return DateTime.Parse("2018-10-17");
                yield return DateTime.Parse("2018-10-24");
                yield return DateTime.Parse("2018-12-05");
                yield return DateTime.Parse("2018-12-12");
                yield return DateTime.Parse("2018-12-19");
            }
        }
    }
}
