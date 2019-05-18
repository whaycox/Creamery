using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Curds.Cron.Token.Tests
{
    [TestClass]
    public class DayOfWeek : Template.Basic<Domain.DayOfWeek>
    {
        protected override int AbsoluteMin => Domain.DayOfWeek.MinDayOfWeek;
        protected override int AbsoluteMax => Domain.DayOfWeek.MaxDayOfWeek;

        protected override Domain.DayOfWeek Build(IEnumerable<Range.Domain.Basic> ranges) => new Domain.DayOfWeek(ranges);
        protected override int ExpectedDatePart(DateTime testTime) => (int)testTime.DayOfWeek;
    }
}
