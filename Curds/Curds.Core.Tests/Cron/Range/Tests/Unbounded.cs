using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Curds.Cron.Range.Tests
{
    using Enumeration;

    [TestClass]
    public class Unbounded : Template.Unbounded<Implementation.Unbounded>
    {
        protected override Implementation.Unbounded TestObject { get; } = new Implementation.Unbounded();
        protected override ExpressionPart TestingPart => ExpressionPart.Hour;

        [TestMethod]
        public void IsAlwaysTrue()
        {
            DateTime testTime = new DateTime(2019, 05, 13);
            for (int i = Token.Domain.Hour.MinHour; i <= Token.Domain.Hour.MaxHour; i++)
            {
                Assert.IsTrue(TestObject.Test(MockToken, testTime));
                testTime = testTime.AddHours(1);
            }
        }
    }
}
