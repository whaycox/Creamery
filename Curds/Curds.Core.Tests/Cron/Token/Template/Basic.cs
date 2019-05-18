using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Curds.Cron.Token.Template
{
    using Domain;
    using System.Linq;

    public abstract class Basic<T> : Test where T : Basic
    {
        protected abstract int AbsoluteMin { get; }
        protected abstract int AbsoluteMax { get; }

        protected abstract T Build(IEnumerable<Range.Domain.Basic> ranges);

        protected abstract int ExpectedDatePart(DateTime testTime);

        protected IEnumerable<Range.Mock.Basic> MinMaxRange(int min, int max) => new Range.Mock.Basic[] { new Range.Mock.Basic(min, max) };
        protected IEnumerable<Range.Mock.Basic> BoolRange(params bool[] children) => children.Select(c => new Range.Mock.Basic(c));

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
        public void TrueIfAnyChildrenAre()
        {
            T token = Build(BoolRange(false, true, false));
            Assert.IsTrue(token.Test(DateTime.MinValue));
        }

        [TestMethod]
        public void FalseWhenAllChildrenAre()
        {
            T token = Build(BoolRange(false, false, false));
            Assert.IsFalse(token.Test(DateTime.MinValue));
        }
    }
}
