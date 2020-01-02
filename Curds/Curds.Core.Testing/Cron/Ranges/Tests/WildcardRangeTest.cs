using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Curds.Cron.Ranges.Tests
{
    using Implementation;

    [TestClass]
    public class WildcardRangeTest
    {
        private WildcardRange TestObject = new WildcardRange();

        [TestMethod]
        public void IsAlwaysTrue()
        {
            Assert.IsTrue(TestObject.IsActive(DateTime.MinValue));
            Assert.IsTrue(TestObject.IsActive(DateTime.MaxValue));
        }
    }
}
