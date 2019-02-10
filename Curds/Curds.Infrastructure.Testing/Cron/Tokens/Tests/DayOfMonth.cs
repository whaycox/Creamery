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
            { new AcceptanceCase<FormatException>() { Delegate = () => new Tokens.DayOfMonth("*,*/2,5-7"), ShouldSucceed = true } },
            { new AcceptanceCase<FormatException>() { Delegate = () => new Tokens.DayOfMonth("2,4,5,7,13"), ShouldSucceed = true } },
            { new AcceptanceCase<FormatException>() { Delegate = () => new Tokens.DayOfMonth("5 -7"), ShouldSucceed = false } },
            { new AcceptanceCase<FormatException>() { Delegate = () => new Tokens.DayOfMonth("5 -7"), ShouldSucceed = false } },
            { new AcceptanceCase<FormatException>() { Delegate = () => new Tokens.DayOfMonth("5 -7"), ShouldSucceed = false } },
            { new AcceptanceCase<FormatException>() { Delegate = () => new Tokens.DayOfMonth("5- 7"), ShouldSucceed = false } },
            { new AcceptanceCase<FormatException>() { Delegate = () => new Tokens.DayOfMonth("0-7"), ShouldSucceed = false } },
            { new AcceptanceCase<FormatException>() { Delegate = () => new Tokens.DayOfMonth("5-32"), ShouldSucceed = false } },
            { new AcceptanceCase<FormatException>() { Delegate = () => new Tokens.DayOfMonth("*"), ShouldSucceed = true } },
            { new AcceptanceCase<FormatException>() { Delegate = () => new Tokens.DayOfMonth("*/2"), ShouldSucceed = true } },
            { new AcceptanceCase<FormatException>() { Delegate = () => new Tokens.DayOfMonth("*/5"), ShouldSucceed = true } },
            { new AcceptanceCase<FormatException>() { Delegate = () => new Tokens.DayOfMonth("*/7"), ShouldSucceed = true } },
            { new AcceptanceCase<FormatException>() { Delegate = () => new Tokens.DayOfMonth("32"), ShouldSucceed = false } },
            { new AcceptanceCase<FormatException>() { Delegate = () => new Tokens.DayOfMonth("0"), ShouldSucceed = false } },
            { new AcceptanceCase<FormatException>() { Delegate = () => new Tokens.DayOfMonth("5W"), ShouldSucceed = true } },
            { new AcceptanceCase<FormatException>() { Delegate = () => new Tokens.DayOfMonth("W5"), ShouldSucceed = false } },
            { new AcceptanceCase<FormatException>() { Delegate = () => new Tokens.DayOfMonth("10W"), ShouldSucceed = true } },
            { new AcceptanceCase<FormatException>() { Delegate = () => new Tokens.DayOfMonth("W10"), ShouldSucceed = false } },
            { new AcceptanceCase<FormatException>() { Delegate = () => new Tokens.DayOfMonth("15W"), ShouldSucceed = true } },
            { new AcceptanceCase<FormatException>() { Delegate = () => new Tokens.DayOfMonth("W15"), ShouldSucceed = false } },
            { new AcceptanceCase<FormatException>() { Delegate = () => new Tokens.DayOfMonth("25W"), ShouldSucceed = true } },
            { new AcceptanceCase<FormatException>() { Delegate = () => new Tokens.DayOfMonth("W25"), ShouldSucceed = false } },
            { new AcceptanceCase<FormatException>() { Delegate = () => new Tokens.DayOfMonth("L"), ShouldSucceed = true } },
            { new AcceptanceCase<FormatException>() { Delegate = () => new Tokens.DayOfMonth("20-L"), ShouldSucceed = false } },
            { new AcceptanceCase<FormatException>() { Delegate = () => new Tokens.DayOfMonth("L-20"), ShouldSucceed = true } },
            { new AcceptanceCase<ArgumentOutOfRangeException>() { Delegate = () => new Tokens.DayOfMonth("L-21"), ShouldSucceed = false } },
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
