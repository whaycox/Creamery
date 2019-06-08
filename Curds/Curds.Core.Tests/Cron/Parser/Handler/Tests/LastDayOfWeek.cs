using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Curds.Cron.Parser.Handler.Tests
{
    using Domain;

    [TestClass]
    public class LastDayOfWeek : Template.ParsingHandler<Implementation.LastDayOfWeek>
    {
        protected override Implementation.LastDayOfWeek TestObject { get; } = new Implementation.LastDayOfWeek(null);

        protected override Implementation.LastDayOfWeek Build(ParsingHandler successor) => new Implementation.LastDayOfWeek(successor);

        [TestMethod]
        public void ThrowsWithBadNames()
        {
            Assert.ThrowsException<FormatException>(() => TestObject.HandleParse("THOL"));
            Assert.ThrowsException<FormatException>(() => TestObject.HandleParse("MANL"));
            Assert.ThrowsException<FormatException>(() => TestObject.HandleParse("TOEL"));
            Assert.ThrowsException<FormatException>(() => TestObject.HandleParse("WADL"));
            Assert.ThrowsException<FormatException>(() => TestObject.HandleParse("FIRL"));
            Assert.ThrowsException<FormatException>(() => TestObject.HandleParse("SUTL"));
            Assert.ThrowsException<FormatException>(() => TestObject.HandleParse("SANL"));
            Assert.ThrowsException<FormatException>(() => TestObject.HandleParse("THURSL"));
            Assert.ThrowsException<FormatException>(() => TestObject.HandleParse("WEDSL"));
        }

        [TestMethod]
        public void ThrowsWithRanges()
        {
            Assert.ThrowsException<FormatException>(() => TestObject.HandleParse("1-2L"));
            Assert.ThrowsException<FormatException>(() => TestObject.HandleParse("1L-2L"));
            Assert.ThrowsException<FormatException>(() => TestObject.HandleParse("1L-2"));
            Assert.ThrowsException<FormatException>(() => TestObject.HandleParse("SUN-MONL"));
            Assert.ThrowsException<FormatException>(() => TestObject.HandleParse("SUNL-MONL"));
            Assert.ThrowsException<FormatException>(() => TestObject.HandleParse("SUNL-MON"));
        }

        [TestMethod]
        public void CanLookupFromNames()
        {
            for (int i = Token.Domain.DayOfWeek.MinDayOfWeek; i <= Token.Domain.DayOfWeek.MaxDayOfWeek; i++)
            {
                System.DayOfWeek day = (System.DayOfWeek)i;
                TestName($"{Mock.DayOfWeek.Map[day]}L", i);
            }
        }
        private void TestName(string name, int expected)
        {
            TestParse<Range.Domain.Basic>(name, (p) => VerifyParsedName(p, expected));
        }
        private void VerifyParsedName(Range.Domain.Basic parsed, int expected)
        {
            Assert.AreEqual(expected, parsed.Min);
            Assert.AreEqual(expected, parsed.Max);
        }

        [TestMethod]
        public void CanParseWithNumbers()
        {
            for (int i = Token.Domain.DayOfWeek.MinDayOfWeek; i <= Token.Domain.DayOfWeek.MaxDayOfWeek; i++)
                TestParse<Range.Implementation.LastDayOfWeek>($"{i}L", (p) => CanParseWithNumbersHelper(p, i));
        }
        private void CanParseWithNumbersHelper(Range.Implementation.LastDayOfWeek parsed, int expected)
        {
            Assert.AreEqual(expected, parsed.Min);
            Assert.AreEqual(expected, parsed.Max);
        }
    }
}
