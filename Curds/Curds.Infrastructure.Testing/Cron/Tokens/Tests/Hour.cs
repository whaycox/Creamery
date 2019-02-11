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
            { new AcceptanceCase<FormatException>() { Delegate = () => new Tokens.Hour("*,*/2,5-20"), ShouldSucceed = true } },
            { new AcceptanceCase<FormatException>() { Delegate = () => new Tokens.Hour("*-2"), ShouldSucceed = false } },
            { new AcceptanceCase<FormatException>() { Delegate = () => new Tokens.Hour("5-24"), ShouldSucceed = false } },
            { new AcceptanceCase<FormatException>() { Delegate = () => new Tokens.Hour("0,1,2,3,4,23"), ShouldSucceed = true } },
            { new AcceptanceCase<FormatException>() { Delegate = () => new Tokens.Hour("0,1,2,3,4,25"), ShouldSucceed = false } },
            { new AcceptanceCase<FormatException>() { Delegate = () => new Tokens.Hour("24"), ShouldSucceed = false } },
            { new AcceptanceCase<FormatException>() { Delegate = () => new Tokens.Hour("-10-12"), ShouldSucceed = false } },
            { new AcceptanceCase<FormatException>() { Delegate = () => new Tokens.Hour("12-30"), ShouldSucceed = false } },
        };

        protected override IEnumerable<CronCase<Tokens.Hour>> TestCases => new List<CronCase<Tokens.Hour>>
        {
            { new BusinessHours() },
            { new EverySixHours() },
        };
    }
}
