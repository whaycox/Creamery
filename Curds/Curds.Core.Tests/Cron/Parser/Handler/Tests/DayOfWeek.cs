using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Curds.Cron.Parser.Handler.Tests
{
    using Domain;

    [TestClass]
    public class DayOfWeek : Template.Definite<Implementation.DayOfWeek>
    {
        protected override Implementation.DayOfWeek TestObject { get; } = new Implementation.DayOfWeek(null);

        protected override Implementation.DayOfWeek Build(ParsingHandler successor) => new Implementation.DayOfWeek(successor);

        [TestMethod]
        public void ThrowsWithBadNames()
        {
            Assert.ThrowsException<FormatException>(() => TestObject.HandleParse("THO"));
            Assert.ThrowsException<FormatException>(() => TestObject.HandleParse("MAN"));
            Assert.ThrowsException<FormatException>(() => TestObject.HandleParse("TOE"));
            Assert.ThrowsException<FormatException>(() => TestObject.HandleParse("WAD"));
            Assert.ThrowsException<FormatException>(() => TestObject.HandleParse("FIR"));
            Assert.ThrowsException<FormatException>(() => TestObject.HandleParse("SUT"));
            Assert.ThrowsException<FormatException>(() => TestObject.HandleParse("SAN"));
            Assert.ThrowsException<FormatException>(() => TestObject.HandleParse("THURS"));
            Assert.ThrowsException<FormatException>(() => TestObject.HandleParse("WEDS"));
        }

        [TestMethod]
        public void CanLookupFromNames()
        {
            for (int i = Token.Domain.DayOfWeek.MinDayOfWeek; i <= Token.Domain.DayOfWeek.MaxDayOfWeek; i++)
            {
                System.DayOfWeek day = (System.DayOfWeek)i;
                TestName(Mock.DayOfWeek.Map[day], i);
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
    }
}
