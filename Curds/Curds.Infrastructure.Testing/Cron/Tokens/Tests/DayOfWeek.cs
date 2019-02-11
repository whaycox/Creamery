using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Infrastructure.Cron.Tokens.Tests
{
    using Cases.DayOfWeek;

    [TestClass]
    public class DayOfWeek : CronTemplate<Tokens.DayOfWeek>
    {
        protected override IEnumerable<AcceptanceCase> AcceptanceCases => new List<AcceptanceCase>
        {
            { new AcceptanceCase<FormatException>() { Delegate = () => new Tokens.DayOfWeek("*,*/2,3-6"), ShouldSucceed = true } },
            { new AcceptanceCase<FormatException>() { Delegate = () => new Tokens.DayOfWeek("*,*/2,Tue-Fri"), ShouldSucceed = true } },
            { new AcceptanceCase<FormatException>() { Delegate = () => new Tokens.DayOfWeek("Tue-Fri"), ShouldSucceed = true } },
            { new AcceptanceCase<FormatException>() { Delegate = () => new Tokens.DayOfWeek("Tues-6"), ShouldSucceed = false } },
            { new AcceptanceCase<FormatException>() { Delegate = () => new Tokens.DayOfWeek("2-Thur"), ShouldSucceed = false } },
            { new AcceptanceCase<FormatException>() { Delegate = () => new Tokens.DayOfWeek("-1-5"), ShouldSucceed = false } },
            { new AcceptanceCase<FormatException>() { Delegate = () => new Tokens.DayOfWeek("2-7"), ShouldSucceed = false } },
            { new AcceptanceCase<FormatException>() { Delegate = () => new Tokens.DayOfWeek("Mon-Tue"), ShouldSucceed = true } },
            { new AcceptanceCase<InvalidOperationException>() { Delegate = () => new Tokens.DayOfWeek("Tue-Mon"), ShouldSucceed = false } },
            { new AcceptanceCase<FormatException>() { Delegate = () => new Tokens.DayOfWeek("Wed-SAT"), ShouldSucceed = true } },
            { new AcceptanceCase<InvalidOperationException>() { Delegate = () => new Tokens.DayOfWeek("THU-SUN"), ShouldSucceed = false } },
            { new AcceptanceCase<FormatException>() { Delegate = () => new Tokens.DayOfWeek("SUN-THU"), ShouldSucceed = true } },
            { new AcceptanceCase<FormatException>() { Delegate = () => new Tokens.DayOfWeek("0-6"), ShouldSucceed = true } },
        };

        protected override IEnumerable<CronCase<Tokens.DayOfWeek>> TestCases => new List<CronCase<Tokens.DayOfWeek>>
        {
            { new LastWednesday() },
            { new MonWedFri() },
            { new ThirdThursday() },
            { new Weekdays() },
            { new Weekends() },
        };
    }
}
