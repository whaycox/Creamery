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
            { new FailureCase<FormatException>(() => new Cron.Expression("oeu")) },
            { new FailureCase<FormatException>(() => new Cron.Expression("10 3 4 5 6 5")) },
            { new SuccessCase(() => new Cron.Expression("* * * * *")) },
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
