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
        protected override IEnumerable<AcceptanceCase> AcceptanceCases
        {
            get
            {
                yield return new AcceptanceCase() { Delegate = () => new Tokens.DayOfWeek("*,*/2,3-6"), ShouldSucceed = true };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.DayOfWeek("*,*/2,Tue-Fri"), ShouldSucceed = true };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.DayOfWeek("Tue-Fri"), ShouldSucceed = true };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.DayOfWeek("Tues-6"), ShouldSucceed = false };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.DayOfWeek("2-Thur"), ShouldSucceed = false };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.DayOfWeek("-1-5"), ShouldSucceed = false };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.DayOfWeek("2-7"), ShouldSucceed = false };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.DayOfWeek("Mon-Tue"), ShouldSucceed = true };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.DayOfWeek("Tue-Mon"), ShouldSucceed = false };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.DayOfWeek("Wed-SAT"), ShouldSucceed = true };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.DayOfWeek("THU-SUN"), ShouldSucceed = false };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.DayOfWeek("SUN-THU"), ShouldSucceed = true };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.DayOfWeek("0-6"), ShouldSucceed = true };
            }
        }

        protected override IEnumerable<CronCase<Tokens.DayOfWeek>> TestCases
        {
            get
            {
                yield return new LastWednesday();
                yield return new MonWedFri();
                yield return new ThirdThursday();
                yield return new Weekdays();
                yield return new Weekends();
            }
        }
    }
}
