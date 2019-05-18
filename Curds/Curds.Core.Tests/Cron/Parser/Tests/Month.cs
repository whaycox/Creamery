using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Curds.Cron.Parser.Tests
{
    [TestClass]
    public class Month : Template.Basic<Implementation.Month>
    {
        protected override Implementation.Month TestObject { get; } = new Implementation.Month();

        [TestMethod]
        public void ParsesSingleString()
        {
            TestParse("apr", ParsesSingleStringHelper);
        }
        private void ParsesSingleStringHelper(List<Range.Domain.Basic> parsed)
        {
            Assert.AreEqual(1, parsed.Count);
            TestBasicRange(parsed[0], 4, 4);
        }

        [TestMethod]
        public void ParsesStringRange()
        {
            TestParse("Feb-SEp", ParsesStringRangeHelper);
        }
        private void ParsesStringRangeHelper(List<Range.Domain.Basic> parsed)
        {
            Assert.AreEqual(1, parsed.Count);
            TestBasicRange(parsed[0], 2, 9);
        }

        [TestMethod]
        public void ParsesMultipleStrings()
        {
            TestParse("Jul,FEB,DEC", ParsesMultipleStringsHelper);
        }
        private void ParsesMultipleStringsHelper(List<Range.Domain.Basic> parsed)
        {
            Assert.AreEqual(3, parsed.Count);
            TestBasicRange(parsed[0], 7, 7);
            TestBasicRange(parsed[1], 2, 2);
            TestBasicRange(parsed[2], 12, 12);
        }

        [TestMethod]
        public void ParsesMultipleStringRanges()
        {
            TestParse("Jan-Mar,APR-JUN,OCT-NOV", ParsesMultipleStringRangesHelper);
        }
        private void ParsesMultipleStringRangesHelper(List<Range.Domain.Basic> parsed)
        {
            Assert.AreEqual(3, parsed.Count);
            TestBasicRange(parsed[0], 1, 3);
            TestBasicRange(parsed[1], 4, 6);
            TestBasicRange(parsed[2], 10, 11);
        }
    }
}
