using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Infrastructure.Cron.Token.Tests
{
    using Cases.DayOfWeek;

    [TestClass]
    public class DayOfWeek : CronTemplate<Token.DayOfWeek>
    {
        protected override IEnumerable<AcceptanceCase> AcceptanceCases => new List<AcceptanceCase>
        {
            { new SuccessCase(() => new Token.DayOfWeek("*,*/2,3-6")) },
            { new SuccessCase(() => new Token.DayOfWeek("*,*/2,Tue-Fri")) },
            { new SuccessCase(() => new Token.DayOfWeek("Tue-Fri")) },
            { new FailureCase<FormatException>(() => new Token.DayOfWeek("Tues-6")) },
            { new FailureCase<FormatException>(() => new Token.DayOfWeek("2-Thur")) },
            { new FailureCase<FormatException>(() => new Token.DayOfWeek("-1-5")) },
            { new FailureCase<FormatException>(() => new Token.DayOfWeek("2-7")) },
            { new SuccessCase(() => new Token.DayOfWeek("Mon-Tue")) },
            { new FailureCase<InvalidOperationException>(() => new Token.DayOfWeek("Tue-Mon")) },
            { new SuccessCase(() => new Token.DayOfWeek("Wed-SAT")) },
            { new FailureCase<InvalidOperationException>(() => new Token.DayOfWeek("THU-SUN")) },
            { new SuccessCase(() => new Token.DayOfWeek("SUN-THU")) },
            { new SuccessCase(() => new Token.DayOfWeek("0-6")) },
        };

        protected override IEnumerable<CronCase<Token.DayOfWeek>> TestCases => new List<CronCase<Token.DayOfWeek>>
        {
            { new LastWednesday() },
            { new MonWedFri() },
            { new ThirdThursday() },
            { new Weekdays() },
            { new Weekends() },
        };
    }
}
