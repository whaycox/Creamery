using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Curds.Cron.Parser.Handler.Tests
{
    using Domain;

    [TestClass]
    public class NthDayOfWeek : Template.ParsingHandler<Implementation.NthDayOfWeek>
    {
        protected override Implementation.NthDayOfWeek TestObject { get; } = new Implementation.NthDayOfWeek(null);

        protected override Implementation.NthDayOfWeek Build(ParsingHandler successor) => new Implementation.NthDayOfWeek(successor);

        private string NthDayOfWeekString(string day, int n) => $"{day}#{n}";
        private string NthDayOfWeekString(System.DayOfWeek day, int n) => NthDayOfWeekString(Mock.DayOfWeek.Map[day], n);

        [TestMethod]
        public void InvalidNThrows()
        {
            Assert.ThrowsException<FormatException>(() => TestObject.HandleParse(NthDayOfWeekString("SUN", -1)));
            Assert.ThrowsException<FormatException>(() => TestObject.HandleParse(NthDayOfWeekString("SUN", 0)));
            Assert.ThrowsException<FormatException>(() => TestObject.HandleParse(NthDayOfWeekString("SUN", 6)));
        }

        [TestMethod]
        public void ParsesWithNumber()
        {
            TestParse<Range.Implementation.NthDayOfWeek>(NthDayOfWeekString(ParsesWithNumberValue.ToString(), ParsesWithNumberValue), ParsesWithNumberHelper);
        }
        private const int ParsesWithNumberValue = 1;
        private void ParsesWithNumberHelper(Range.Implementation.NthDayOfWeek parsed)
        {
            Assert.AreEqual(ParsesWithNumberValue, parsed.Min);
            Assert.AreEqual(ParsesWithNumberValue, parsed.Max);
            Assert.AreEqual(ParsesWithNumberValue, parsed.NthValue);
        }

        [TestMethod]
        public void ParsesWithString()
        {
            TestParse<Range.Implementation.NthDayOfWeek>(NthDayOfWeekString((System.DayOfWeek)ParsesWithStringValue, ParsesWithStringValue), ParsesWithStringHelper);
        }
        private const int ParsesWithStringValue = 3;
        private void ParsesWithStringHelper(Range.Implementation.NthDayOfWeek parsed)
        {
            Assert.AreEqual(ParsesWithStringValue, parsed.Min);
            Assert.AreEqual(ParsesWithStringValue, parsed.Max);
            Assert.AreEqual(ParsesWithStringValue, parsed.NthValue);
        }

        [TestMethod]
        public void CanParseN()
        {
            for (int i = 1; i <= 5; i++)
                TestParse<Range.Implementation.NthDayOfWeek>(NthDayOfWeekString(CanParseNValue, i), (p) => CanParseNHelper(p, i));
        }
        private const System.DayOfWeek CanParseNValue = System.DayOfWeek.Thursday;
        private void CanParseNHelper(Range.Implementation.NthDayOfWeek parsed, int expected)
        {
            Assert.AreEqual((int)CanParseNValue, parsed.Min);
            Assert.AreEqual((int)CanParseNValue, parsed.Max);
            Assert.AreEqual(expected, parsed.NthValue);
        }
    }
}
