using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Curds.Cron.Token.Template
{
    using Domain;

    public abstract class Basic<T> : Test where T : Basic
    {
        protected abstract int AbsoluteMin { get; }
        protected abstract int AbsoluteMax { get; }

        protected abstract T Build(IEnumerable<Range.Domain.Basic> ranges);

        protected IEnumerable<Range.Mock.Basic> MinMaxRange(int min, int max) => new Range.Mock.Basic[] { new Range.Mock.Basic(min, max, true) };

        [TestMethod]
        public void NullRangesThrows()
        {
            Assert.ThrowsException<ArgumentNullException>(() => Build(null));
        }

        [TestMethod]
        public void LessThanAbsoluteMinThrows()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Build(MinMaxRange(AbsoluteMin - 1, AbsoluteMax)));
        }

        [TestMethod]
        public void MoreThanAbsoluteMaxThrows()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Build(MinMaxRange(AbsoluteMin, AbsoluteMax + 1)));
        }

        [TestMethod]
        public void TestPassesCases()
        {
            foreach (TestCase test in TestCases)
                Assert.AreEqual(test.Expected, Build(test.RangeGenerator()).Test(test.Time));
        }
        protected abstract IEnumerable<TestCase> TestCases { get; }
    }
}
