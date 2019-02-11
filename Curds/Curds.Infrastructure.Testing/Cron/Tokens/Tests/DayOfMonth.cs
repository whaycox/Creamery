using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Infrastructure.Cron.Tokens.Tests
{
    using Cases.DayOfMonth;

    [TestClass]
    public class DayOfMonth : CronTemplate<Tokens.DayOfMonth>
    {
        protected override IEnumerable<AcceptanceCase> AcceptanceCases => new List<AcceptanceCase>
        {
            { new SuccessCase(() => new Tokens.DayOfMonth("*,*/2,5-7")) },
            { new SuccessCase(() => new Tokens.DayOfMonth("2,4,5,7,13")) },
            { new FailureCase<FormatException>(() => new Tokens.DayOfMonth("5 -7")) },
            { new FailureCase<FormatException>(() => new Tokens.DayOfMonth("5- 7")) },
            { new FailureCase<FormatException>(() => new Tokens.DayOfMonth("0-7")) },
            { new FailureCase<FormatException>(() => new Tokens.DayOfMonth("5-32")) },
            { new SuccessCase(() => new Tokens.DayOfMonth("*")) },
            { new SuccessCase(() => new Tokens.DayOfMonth("*/2")) },
            { new SuccessCase(() => new Tokens.DayOfMonth("*/5")) },
            { new SuccessCase(() => new Tokens.DayOfMonth("*/7")) },
            { new FailureCase<FormatException>(() => new Tokens.DayOfMonth("32")) },
            { new FailureCase<FormatException>(() => new Tokens.DayOfMonth("0")) },
            { new SuccessCase(() => new Tokens.DayOfMonth("5W")) },
            { new FailureCase<FormatException>(() => new Tokens.DayOfMonth("W5")) },
            { new SuccessCase(() => new Tokens.DayOfMonth("10W")) },
            { new FailureCase<FormatException>(() => new Tokens.DayOfMonth("W10")) },
            { new SuccessCase(() => new Tokens.DayOfMonth("15W")) },
            { new FailureCase<FormatException>(() => new Tokens.DayOfMonth("W15")) },
            { new SuccessCase(() => new Tokens.DayOfMonth("25W")) },
            { new FailureCase<FormatException>(() => new Tokens.DayOfMonth("W25")) },
            { new SuccessCase(() => new Tokens.DayOfMonth("L")) },
            { new FailureCase<FormatException>(() => new Tokens.DayOfMonth("20-L")) },
            { new SuccessCase(() => new Tokens.DayOfMonth("L-20")) },
            { new FailureCase<ArgumentOutOfRangeException>(() => new Tokens.DayOfMonth("L-21")) },
        };

        protected override IEnumerable<CronCase<Tokens.DayOfMonth>> TestCases => new List<CronCase<Tokens.DayOfMonth>>
        {
            { new FifthThroughThirteenth() },
            { new Last() },
            { new NearestWeekday() },
            { new OffsetLast() },
            { new TenthTwentiethLast() },
        };
    }
}
