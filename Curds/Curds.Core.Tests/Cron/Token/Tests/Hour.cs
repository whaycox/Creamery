using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Infrastructure.Cron.Token.Tests
{
    using Cases.Hour;

    [TestClass]
    public class Hour : CronTemplate<Token.Hour>
    {
        protected override IEnumerable<AcceptanceCase> AcceptanceCases => new List<AcceptanceCase>
        {
            { new SuccessCase(() => new Token.Hour("*,*/2,5-20")) },
            { new FailureCase<FormatException>(() => new Token.Hour("*-2")) },
            { new FailureCase<FormatException>(() => new Token.Hour("5-24")) },
            { new SuccessCase(() => new Token.Hour("0,1,2,3,4,23")) },
            { new FailureCase<FormatException>(() => new Token.Hour("0,1,2,3,4,25")) },
            { new FailureCase<FormatException>(() => new Token.Hour("24")) },
            { new FailureCase<FormatException>(() => new Token.Hour("-10-12")) },
            { new FailureCase<FormatException>(() => new Token.Hour("12-30")) },
        };

        protected override IEnumerable<CronCase<Token.Hour>> TestCases => new List<CronCase<Token.Hour>>
        {
            { new BusinessHours() },
            { new EverySixHours() },
        };
    }
}
