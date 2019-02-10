using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Infrastructure.Cron.Tests
{
    using Cases.CronExpression;

    [TestClass]
    public class CronExpression : CronTemplate<Cron.Expression>
    {
        protected override IEnumerable<AcceptanceCase> AcceptanceCases
        {
            get
            {
                yield return new AcceptanceCase() { Delegate = () => new Cron.Expression("oeu"), ShouldSucceed = false };
                yield return new AcceptanceCase() { Delegate = () => new Cron.Expression("10 3 4 5 6 5"), ShouldSucceed = false };
                yield return new AcceptanceCase() { Delegate = () => new Cron.Expression("* * * * *"), ShouldSucceed = true };
            }
        }

        protected override IEnumerable<CronCase<Cron.Expression>> TestCases
        {
            get
            {
                yield return new BigExpression();
                yield return new EveryHourLastFriday();
                yield return new EveryMinuteAtTwo();
                yield return new MinuteStep();
                yield return new TwelveNoon();
            }
        }
    }
}
