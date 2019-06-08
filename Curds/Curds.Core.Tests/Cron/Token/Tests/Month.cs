using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Curds.Cron.Token.Tests
{
    [TestClass]
    public class Month : Template.Basic<Domain.Month>
    {
        protected override int AbsoluteMin => Domain.Month.MinMonth;
        protected override int AbsoluteMax => Domain.Month.MaxMonth;

        protected override Domain.Month Build(IEnumerable<Range.Domain.Basic> ranges) => new Domain.Month(ranges);
        protected override int ExpectedDatePart(DateTime testTime) => testTime.Month;
    }
}
