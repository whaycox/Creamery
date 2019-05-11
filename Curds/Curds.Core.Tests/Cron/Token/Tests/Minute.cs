using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Infrastructure.Cron.Token.Tests
{
    using Cases.Minute;

    [TestClass]
    public class Minute : CronTemplate<Token.Minute>
    {
        protected override IEnumerable<AcceptanceCase> AcceptanceCases => new List<AcceptanceCase>
        {
            { new FailureCase<FormatException>(() => new Token.Minute("0-60")) },
            { new SuccessCase(() => new Token.Minute("*,*/5,20,55")) },
            { new FailureCase<InvalidOperationException>(() => new Token.Minute("30-29")) },
            { new FailureCase<FormatException>(() => new Token.Minute("60")) },
            { new FailureCase<FormatException>(() => new Token.Minute("50-100")) },
            { new FailureCase<FormatException>(() => new Token.Minute("-10-50")) },
        };

        protected override IEnumerable<CronCase<Token.Minute>> TestCases => new List<CronCase<Token.Minute>>
        {
            { new BackHalfHour() },
            { new EverySixMinutes() },
        };
    }
}
