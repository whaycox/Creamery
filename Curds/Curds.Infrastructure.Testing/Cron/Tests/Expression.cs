using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Infrastructure.Cron.Tests
{
    using Cases.CronExpression;

    [TestClass]
    public class Expression : CronTemplate<Cron.Expression>
    {
        protected override IEnumerable<AcceptanceCase> AcceptanceCases => new List<AcceptanceCase>
        { 
            { new AcceptanceCase<FormatException>() { Delegate = () => new Cron.Expression("oeu"), ShouldSucceed = false } },
            { new AcceptanceCase<FormatException>() { Delegate = () => new Cron.Expression("10 3 4 5 6 5"), ShouldSucceed = false } },
            { new AcceptanceCase<FormatException>() { Delegate = () => new Cron.Expression("* * * * *"), ShouldSucceed = true } },
        };

        protected override IEnumerable<CronCase<Cron.Expression>> TestCases => new List<CronCase<Cron.Expression>>
        {
            { new BigExpression() },
            { new EveryHourLastFriday() },
            { new EveryMinuteAtTwo() },
            { new MinuteStep() },
            { new TwelveNoon() },
        };
    }
}
