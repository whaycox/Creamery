using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Cron.Token.Tests
{
    [TestClass]
    public class DayOfMonth : Template.Basic<Domain.DayOfMonth>
    {
        protected override int AbsoluteMin => Domain.DayOfMonth.MinDayOfMonth;
        protected override int AbsoluteMax => Domain.DayOfMonth.MaxDayOfMonth;

        protected override Domain.DayOfMonth Build(IEnumerable<Range.Domain.Basic> ranges) => new Domain.DayOfMonth(ranges);
        protected override int ExpectedDatePart(DateTime testTime) => testTime.Day;
    }
}
