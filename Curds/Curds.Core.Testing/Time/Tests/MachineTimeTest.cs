using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Curds.Time.Tests
{
    using Implementation;

    [TestClass]
    public class MachineTimeTest
    {
        private MachineTime TestObject = new MachineTime();

        [TestMethod]
        public void RetrievesCurrentTime()
        {
            DateTimeOffset actual = TestObject.Current;

            DateTimeOffset expected = DateTimeOffset.Now;
            double delta = (expected - actual).TotalMilliseconds;
            Assert.IsTrue(delta < MaxDeltaMs);
        }
        private double MaxDeltaMs = 5;
    }
}
