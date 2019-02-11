using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Infrastructure.Cron.Tokens.Tests
{
    using Cases.Minute;

    [TestClass]
    public class Minute : CronTemplate<Tokens.Minute>
    {
        protected override IEnumerable<AcceptanceCase> AcceptanceCases => new List<AcceptanceCase>
        {
            { new FailureCase<FormatException>(() => new Tokens.Minute("0-60")) },
            { new SuccessCase(() => new Tokens.Minute("*,*/5,20,55")) },
            { new FailureCase<InvalidOperationException>(() => new Tokens.Minute("30-29")) },
            { new FailureCase<FormatException>(() => new Tokens.Minute("60")) },
            { new FailureCase<FormatException>(() => new Tokens.Minute("50-100")) },
            { new FailureCase<FormatException>(() => new Tokens.Minute("-10-50")) },
        };

        protected override IEnumerable<CronCase<Tokens.Minute>> TestCases => new List<CronCase<Tokens.Minute>>
        {
            { new BackHalfHour() },
            { new EverySixMinutes() },
        };
    }
}
