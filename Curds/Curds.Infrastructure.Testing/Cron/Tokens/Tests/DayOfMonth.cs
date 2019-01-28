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
        protected override IEnumerable<AcceptanceCase> AcceptanceCases
        {
            get
            {
                yield return new AcceptanceCase() { Delegate = () => new Tokens.DayOfMonth("*,*/2,5-7"), ShouldSucceed = true };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.DayOfMonth("2,4,5,7,13"), ShouldSucceed = true };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.DayOfMonth("5 -7"), ShouldSucceed = false };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.DayOfMonth("5 -7"), ShouldSucceed = false };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.DayOfMonth("5 -7"), ShouldSucceed = false };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.DayOfMonth("5- 7"), ShouldSucceed = false };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.DayOfMonth("0-7"), ShouldSucceed = false };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.DayOfMonth("5-32"), ShouldSucceed = false };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.DayOfMonth("*"), ShouldSucceed = true };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.DayOfMonth("*/2"), ShouldSucceed = true };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.DayOfMonth("*/5"), ShouldSucceed = true };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.DayOfMonth("*/7"), ShouldSucceed = true };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.DayOfMonth("32"), ShouldSucceed = false };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.DayOfMonth("0"), ShouldSucceed = false };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.DayOfMonth("5W"), ShouldSucceed = true };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.DayOfMonth("W5"), ShouldSucceed = false };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.DayOfMonth("10W"), ShouldSucceed = true };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.DayOfMonth("W10"), ShouldSucceed = false };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.DayOfMonth("15W"), ShouldSucceed = true };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.DayOfMonth("W15"), ShouldSucceed = false };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.DayOfMonth("25W"), ShouldSucceed = true };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.DayOfMonth("W25"), ShouldSucceed = false };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.DayOfMonth("L"), ShouldSucceed = true };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.DayOfMonth("20-L"), ShouldSucceed = false };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.DayOfMonth("L-20"), ShouldSucceed = true };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.DayOfMonth("L-21"), ShouldSucceed = false };
            }
        }

        protected override IEnumerable<CronCase<Tokens.DayOfMonth>> TestCases
        {
            get
            {
                yield return new FifthThroughThirteenth();
                yield return new Last();
                yield return new NearestWeekday();
                yield return new OffsetLast();
                yield return new TenthTwentiethLast();
            }
        }
    }
}
