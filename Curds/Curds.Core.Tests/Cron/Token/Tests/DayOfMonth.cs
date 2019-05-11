using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Infrastructure.Cron.Token.Tests
{
    using Cases.DayOfMonth;

    [TestClass]
    public class DayOfMonth : CronTemplate<Token.DayOfMonth>
    {
        protected override IEnumerable<AcceptanceCase> AcceptanceCases => new List<AcceptanceCase>
        {
            { new SuccessCase(() => new Token.DayOfMonth("*,*/2,5-7")) },
            { new SuccessCase(() => new Token.DayOfMonth("2,4,5,7,13")) },
            { new FailureCase<FormatException>(() => new Token.DayOfMonth("5 -7")) },
            { new FailureCase<FormatException>(() => new Token.DayOfMonth("5- 7")) },
            { new FailureCase<FormatException>(() => new Token.DayOfMonth("0-7")) },
            { new FailureCase<FormatException>(() => new Token.DayOfMonth("5-32")) },
            { new SuccessCase(() => new Token.DayOfMonth("*")) },
            { new SuccessCase(() => new Token.DayOfMonth("*/2")) },
            { new SuccessCase(() => new Token.DayOfMonth("*/5")) },
            { new SuccessCase(() => new Token.DayOfMonth("*/7")) },
            { new FailureCase<FormatException>(() => new Token.DayOfMonth("32")) },
            { new FailureCase<FormatException>(() => new Token.DayOfMonth("0")) },
            { new SuccessCase(() => new Token.DayOfMonth("5W")) },
            { new FailureCase<FormatException>(() => new Token.DayOfMonth("W5")) },
            { new SuccessCase(() => new Token.DayOfMonth("10W")) },
            { new FailureCase<FormatException>(() => new Token.DayOfMonth("W10")) },
            { new SuccessCase(() => new Token.DayOfMonth("15W")) },
            { new FailureCase<FormatException>(() => new Token.DayOfMonth("W15")) },
            { new SuccessCase(() => new Token.DayOfMonth("25W")) },
            { new FailureCase<FormatException>(() => new Token.DayOfMonth("W25")) },
            { new SuccessCase(() => new Token.DayOfMonth("L")) },
            { new FailureCase<FormatException>(() => new Token.DayOfMonth("20-L")) },
            { new SuccessCase(() => new Token.DayOfMonth("L-20")) },
            { new FailureCase<ArgumentOutOfRangeException>(() => new Token.DayOfMonth("L-21")) },
        };

        protected override IEnumerable<CronCase<Token.DayOfMonth>> TestCases => new List<CronCase<Token.DayOfMonth>>
        {
            { new FifthThroughThirteenth() },
            { new Last() },
            { new NearestWeekday() },
            { new OffsetLast() },
            { new TenthTwentiethLast() },
        };
    }
}
