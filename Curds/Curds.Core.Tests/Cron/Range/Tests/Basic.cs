using System;
using System.Collections.Generic;
using System.Text;
using Curds.Cron.Range.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Cron.Range.Tests
{
    [TestClass]
    public class Basic : Test<Domain.Basic>
    {
        private const int LowValue = 1;
        private const int HighValue = 2;

        private Mock.Basic MockRange = new Mock.Basic();
        private Token.Mock.Basic MockToken => new Token.Mock.Basic(MockRange);

        protected override Domain.Basic TestObject { get; } = new Domain.Basic(LowValue, HighValue);

        [TestMethod]
        public void ThrowsOnInvertedRanges()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Domain.Basic(HighValue, LowValue));
        }

        [TestMethod]
        public void IsTrueOnInclusive()
        {
            Assert.IsTrue(TestObject.Test(MockToken, DateTime.MinValue, LowValue));
            Assert.IsTrue(TestObject.Test(MockToken, DateTime.MinValue, HighValue));
        }

        [TestMethod]
        public void IsFalseOutside()
        {
            Assert.IsFalse(TestObject.Test(MockToken, DateTime.MinValue, LowValue - 1));
            Assert.IsFalse(TestObject.Test(MockToken, DateTime.MinValue, HighValue + 1));
        }


    }
}
