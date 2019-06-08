using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Curds.Cron.Parser.Tests
{
    [TestClass]
    public class DayOfWeek : Template.Basic<Implementation.DayOfWeek>
    {
        protected override Implementation.DayOfWeek TestObject { get; } = new Implementation.DayOfWeek();

        [TestMethod]
        public void ParsesSingleString()
        {
            TestParse("sun", ParsesSingleStringHelper);
        }
        private void ParsesSingleStringHelper(List<Range.Domain.Basic> parsed)
        {
            Assert.AreEqual(1, parsed.Count);
            TestBasicRange(parsed[0], 0, 0);
        }

        [TestMethod]
        public void ParsesStringRange()
        {
            TestParse("tUe-Thu", ParsesStringRangeHelper);
        }
        private void ParsesStringRangeHelper(List<Range.Domain.Basic> parsed)
        {
            Assert.AreEqual(1, parsed.Count);
            TestBasicRange(parsed[0], 2, 4);
        }

        [TestMethod]
        public void ParsesMultipleStrings()
        {
            TestParse("mON,TUE,WeD", ParsesMultipleStringsHelper);
        }
        private void ParsesMultipleStringsHelper(List<Range.Domain.Basic> parsed)
        {
            Assert.AreEqual(3, parsed.Count);
            TestBasicRange(parsed[0], 1, 1);
            TestBasicRange(parsed[1], 2, 2);
            TestBasicRange(parsed[2], 3, 3);
        }

        [TestMethod]
        public void ParsesMultipleStringRanges()
        {
            TestParse("Thu-FRI,TUE-WED,SUN-MON", ParsesMultipleStringRangesHelper);
        }
        private void ParsesMultipleStringRangesHelper(List<Range.Domain.Basic> parsed)
        {
            Assert.AreEqual(3, parsed.Count);
            TestBasicRange(parsed[0], 4, 5);
            TestBasicRange(parsed[1], 2, 3);
            TestBasicRange(parsed[2], 0, 1);
        }
    }
}
