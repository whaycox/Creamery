using System;
using System.Collections.Generic;
using System.Text;
using Curds.Cron.Parser.Handler.Domain;
using Curds.Cron.Parser.Handler.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Cron.Parser.Handler.Tests
{
    using Domain;

    [TestClass]
    public class WeekdayNearest : Template.ParsingHandler<Implementation.WeekdayNearest>
    {
        protected override Implementation.WeekdayNearest TestObject { get; } = new Implementation.WeekdayNearest(null);

        protected override Implementation.WeekdayNearest Build(ParsingHandler successor) => new Implementation.WeekdayNearest(successor);

        private string WeekdayNearestString(int dayOfMonth) => $"{dayOfMonth}W";

        [TestMethod]
        public void ThrowsWithARange()
        {
            Assert.ThrowsException<FormatException>(() => TestObject.HandleParse("1-2"));
            Assert.ThrowsException<FormatException>(() => TestObject.HandleParse("1-2W"));
            Assert.ThrowsException<FormatException>(() => TestObject.HandleParse("1W-2"));
        }

        [TestMethod]
        public void ThrowsWithoutW()
        {
            Assert.ThrowsException<FormatException>(() => TestObject.HandleParse("1"));
        }

        [TestMethod]
        public void ParsesCorrectly()
        {
            for (int i = Token.Domain.DayOfMonth.MinDayOfMonth; i <= Token.Domain.DayOfMonth.MaxDayOfMonth; i++)
                TestParse<Range.Implementation.WeekdayNearest>(WeekdayNearestString(i), (p) => ParsesCorrectlyHelper(p, i));
        }
        private void ParsesCorrectlyHelper(Range.Implementation.WeekdayNearest parsed, int expected)
        {
            Assert.AreEqual(expected, parsed.Min);
            Assert.AreEqual(expected, parsed.Max);
        }
    }
}
