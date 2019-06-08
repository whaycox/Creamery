using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Curds.Cron.Parser.Tests
{
    using Range.Implementation;

    [TestClass]
    public class DayOfMonth : Template.Basic<Implementation.DayOfMonth>
    {
        protected override Implementation.DayOfMonth TestObject { get; } = new Implementation.DayOfMonth();

        private void TestWeekdayNearestRange(Range.Domain.Basic parsed, int dayOfMonth) => TestRange<WeekdayNearest>(parsed, (p) => TestWeekdayNearestRangeHelper(p, dayOfMonth)); 
        private void TestWeekdayNearestRangeHelper(WeekdayNearest parsed, int dayOfMonth)
        {
            Assert.AreEqual(dayOfMonth, parsed.Min);
            Assert.AreEqual(dayOfMonth, parsed.Max);
        }

        private void TestLastDayOfMonthRange(Range.Domain.Basic parsed, int offset) => TestRange<LastDayOfMonth>(parsed, (p) => TestLastDayOfMonthRangeHelper(p, offset));
        private void TestLastDayOfMonthRangeHelper(LastDayOfMonth parsed, int offset)
        {
            Assert.AreEqual(offset, parsed.Offset);
        }

        [TestMethod]
        public void CanParseWeekdayNearest()
        {
            TestParse("15W", CanParseWeekdayNearestHelper);
        }
        private void CanParseWeekdayNearestHelper(List<Range.Domain.Basic> parsed)
        {
            Assert.AreEqual(1, parsed.Count);
            TestWeekdayNearestRange(parsed[0], 15);
        }

        [TestMethod]
        public void CanParseLastDayOfMonth()
        {
            TestParse("L", CanParseLastDayOfMonthHelper);
        }
        private void CanParseLastDayOfMonthHelper(List<Range.Domain.Basic> parsed)
        {
            Assert.AreEqual(1, parsed.Count);
            TestLastDayOfMonthRange(parsed[0], 0);
        }

        [TestMethod]
        public void CanParseOffsetLast()
        {
            TestParse("L-5", CanParseOffsetLastHelper);
        }
        private void CanParseOffsetLastHelper(List<Range.Domain.Basic> parsed)
        {
            Assert.AreEqual(1, parsed.Count);
            TestLastDayOfMonthRange(parsed[0], 5);
        }
    }
}
