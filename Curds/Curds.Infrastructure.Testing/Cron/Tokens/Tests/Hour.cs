using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Infrastructure.Cron.Tokens.Tests
{
    using Cases.Hour;

    [TestClass]
    public class Hour : CronTemplate<Tokens.Hour>
    {
        protected override IEnumerable<AcceptanceCase> AcceptanceCases => new List<AcceptanceCase>
        {
            { new SuccessCase(() => new Tokens.Hour("*,*/2,5-20")) },
            { new FailureCase<FormatException>(() => new Tokens.Hour("*-2")) },
            { new FailureCase<FormatException>(() => new Tokens.Hour("5-24")) },
            { new SuccessCase(() => new Tokens.Hour("0,1,2,3,4,23")) },
            { new FailureCase<FormatException>(() => new Tokens.Hour("0,1,2,3,4,25")) },
            { new FailureCase<FormatException>(() => new Tokens.Hour("24")) },
            { new FailureCase<FormatException>(() => new Tokens.Hour("-10-12")) },
            { new FailureCase<FormatException>(() => new Tokens.Hour("12-30")) },
        };

        protected override IEnumerable<CronCase<Tokens.Hour>> TestCases => new List<CronCase<Tokens.Hour>>
        {
            { new BusinessHours() },
            { new EverySixHours() },
        };
    }
}
