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
            { new SuccessCase(() => new Tokens.DayOfWeek("*,*/2,3-6")) },
            { new SuccessCase(() => new Tokens.DayOfWeek("*,*/2,Tue-Fri")) },
            { new SuccessCase(() => new Tokens.DayOfWeek("Tue-Fri")) },
            { new FailureCase<FormatException>(() => new Tokens.DayOfWeek("Tues-6")) },
            { new FailureCase<FormatException>(() => new Tokens.DayOfWeek("2-Thur")) },
            { new FailureCase<FormatException>(() => new Tokens.DayOfWeek("-1-5")) },
            { new FailureCase<FormatException>(() => new Tokens.DayOfWeek("2-7")) },
            { new SuccessCase(() => new Tokens.DayOfWeek("Mon-Tue")) },
            { new FailureCase<InvalidOperationException>(() => new Tokens.DayOfWeek("Tue-Mon")) },
            { new SuccessCase(() => new Tokens.DayOfWeek("Wed-SAT")) },
            { new FailureCase<InvalidOperationException>(() => new Tokens.DayOfWeek("THU-SUN")) },
            { new SuccessCase(() => new Tokens.DayOfWeek("SUN-THU")) },
            { new SuccessCase(() => new Tokens.DayOfWeek("0-6")) },
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
