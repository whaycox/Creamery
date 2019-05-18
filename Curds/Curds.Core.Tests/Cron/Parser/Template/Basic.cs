using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Curds.Cron.Parser.Template
{
    using Range.Implementation;

    public abstract class Basic<T> : Test<T> where T : Domain.Basic
    {
        protected void TestParse(string ranges, Action<List<Range.Domain.Basic>> testDelegate) =>
            testDelegate(TestObject.ParseRanges(ranges).ToList());

        protected void TestBasicRange(Range.Domain.Basic range, int min, int max) => TestRange<Range.Domain.Basic>(range, (r) => TestBasicRangeHelper(r, min, max));
        protected void TestBasicRangeHelper(Range.Domain.Basic range, int min, int max)
        {
            Assert.AreEqual(min, range.Min);
            Assert.AreEqual(max, range.Max);
        }

        protected void TestStepRange(Range.Domain.Basic range, int step) => TestRange<Step>(range, (r) => TestStepRangeHelper(r, step));
        protected void TestStepRangeHelper(Step stepRange, int step)
        {
            Assert.AreEqual(step, stepRange.StepValueFromMin);
        }

        protected void TestRange<U>(Range.Domain.Basic range) where U : Range.Domain.Basic => TestRange<U>(range, DoNothing);
        protected void TestRange<U>(Range.Domain.Basic range, Action<U> testDelegate) where U : Range.Domain.Basic
        {
            Assert.IsInstanceOfType(range, typeof(U));
            U parsed = range as U;
            testDelegate(parsed);
        }
        protected void DoNothing<U>(U parsed) where U : Range.Domain.Basic { }

        [TestMethod]
        public void ParsesSingleValue()
        {
            TestParse("1", ParsesSingleValueHelper);
        }
        private void ParsesSingleValueHelper(List<Range.Domain.Basic> parsed)
        {
            Assert.AreEqual(1, parsed.Count);
            TestBasicRange(parsed[0], 1, 1);
        }

        [TestMethod]
        public void ParsesARange()
        {
            TestParse("1-2", ParsesARangeHelper);
        }
        private void ParsesARangeHelper(List<Range.Domain.Basic> parsed)
        {
            Assert.AreEqual(1, parsed.Count);
            TestBasicRange(parsed[0], 1, 2);
        }

        [TestMethod]
        public void ParsesMultipleValues()
        {
            TestParse("1,2,3", ParsesMultipleValuesHelper);
        }
        private void ParsesMultipleValuesHelper(List<Range.Domain.Basic> parsed)
        {
            Assert.AreEqual(3, parsed.Count);
            TestBasicRange(parsed[0], 1, 1);
            TestBasicRange(parsed[1], 2, 2);
            TestBasicRange(parsed[2], 3, 3);
        }

        [TestMethod]
        public void ParsesMultipleRanges()
        {
            TestParse("1-2,3-4", ParsesMultipleRangesHelper);
        }
        private void ParsesMultipleRangesHelper(List<Range.Domain.Basic> parsed)
        {
            Assert.AreEqual(2, parsed.Count);
            TestBasicRange(parsed[0], 1, 2);
            TestBasicRange(parsed[1], 3, 4);
        }

        [TestMethod]
        public void ParsesWildcard()
        {
            TestParse("*", ParsesWildcardHelper);
        }
        private void ParsesWildcardHelper(List<Range.Domain.Basic> parsed)
        {
            Assert.AreEqual(1, parsed.Count);
            TestRange<Unbounded>(parsed[0]);
        }

        [TestMethod]
        public void ParsesStep()
        {
            TestParse("*/4", ParsesStepHelper);
        }
        private void ParsesStepHelper(List<Range.Domain.Basic> parsed)
        {
            Assert.AreEqual(1, parsed.Count);
            TestStepRange(parsed[0], 4);
        }

        [TestMethod]
        public void ParsesSomeOfAll()
        {
            var parsed = TestObject.ParseRanges("1,2-3,*/4,*").ToList();
        }
        private void ParsesSomeOfAllHelper(List<Range.Domain.Basic> parsed)
        {
            Assert.AreEqual(4, parsed.Count);
            TestBasicRange(parsed[0], 1, 1);
            TestBasicRange(parsed[1], 2, 3);
            TestStepRange(parsed[2], 4);
            TestRange<Unbounded>(parsed[3]);
        }
    }
}
