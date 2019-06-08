using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Curds.Cron.Token.Tests
{
    [TestClass]
    public class Hour : Template.Basic<Domain.Hour>
    {
        protected override int AbsoluteMin => Domain.Hour.MinHour;
        protected override int AbsoluteMax => Domain.Hour.MaxHour;

        protected override Domain.Hour Build(IEnumerable<Range.Domain.Basic> ranges) => new Domain.Hour(ranges);
        protected override int ExpectedDatePart(DateTime testTime) => testTime.Hour;
    }
}
