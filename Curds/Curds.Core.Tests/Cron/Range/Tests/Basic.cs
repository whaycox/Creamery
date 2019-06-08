using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Curds.Cron.Range.Tests
{
    using Enumeration;

    [TestClass]
    public class Basic : Template.Basic<Domain.Basic>
    {
        private const int LowValue = 3;
        private const int HighValue = 8;

        protected override Domain.Basic TestObject { get; } = new Domain.Basic(LowValue, HighValue);

        protected override ExpressionPart TestingPart => ExpressionPart.Minute;

        private DateTime TestTime(int minute) => new DateTime(2019, 05, 13, 10, minute, 5);

        [TestMethod]
        public void ThrowsOnInvertedRanges()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Domain.Basic(HighValue, LowValue));
        }

        [TestMethod]
        public void IsInvalidWithMinInsideRange()
        {
            Assert.IsFalse(TestObject.IsValid(LowValue + 1, HighValue));
        }

        [TestMethod]
        public void IsInvalidWithMaxInsideRange()
        {
            Assert.IsFalse(TestObject.IsValid(LowValue, HighValue - 1));
        }

        [TestMethod]
        public void IsValidAtRangeOrOutside()
        {
            Assert.IsTrue(TestObject.IsValid(LowValue, HighValue));
            Assert.IsTrue(TestObject.IsValid(LowValue - 1, HighValue));
            Assert.IsTrue(TestObject.IsValid(LowValue, HighValue + 1));
        }

        [TestMethod]
        public void IsTrueOnInclusive()
        {
            Assert.IsTrue(TestObject.Test(MockToken, TestTime(LowValue)));
            Assert.IsTrue(TestObject.Test(MockToken, TestTime(HighValue)));
        }

        [TestMethod]
        public void IsFalseOutside()
        {
            Assert.IsFalse(TestObject.Test(MockToken, TestTime(LowValue - 1)));
            Assert.IsFalse(TestObject.Test(MockToken, TestTime(HighValue + 1)));
        }
    }
}
