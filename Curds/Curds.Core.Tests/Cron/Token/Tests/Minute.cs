using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Curds.Cron.Token.Tests
{
    [TestClass]
    public class Minute : Template.Basic<Domain.Minute>
    {
        protected override int AbsoluteMin => Domain.Minute.MinMinute;
        protected override int AbsoluteMax => Domain.Minute.MaxMinute;

        protected override Domain.Minute Build(IEnumerable<Range.Domain.Basic> ranges) => new Domain.Minute(ranges);
        protected override int ExpectedDatePart(DateTime testTime) => testTime.Minute;
    }
}
