using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Curds.Cron.Range.Tests
{
    [TestClass]
    public class NthDayOfWeek : Template.DayOfWeek<Implementation.NthDayOfWeek>
    {
        protected override DayOfWeek TestDayOfWeek => DayOfWeek.Friday;

        private Implementation.NthDayOfWeek _obj = null;
        protected override Implementation.NthDayOfWeek TestObject => _obj;

        [TestMethod]
        public void ThrowsOnInvalidN()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Implementation.NthDayOfWeek(TestArg, 0));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Implementation.NthDayOfWeek(TestArg, -1));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Implementation.NthDayOfWeek(TestArg, 6));
        }

        [TestMethod]
        public void IsTrueOnNthOccurrenceOnly()
        {
            for (int i = 1; i <= 5; i++)
            {
                BuildObjForN(i);
                TestMayForNthFriday(i);
            }
        }
        private void BuildObjForN(int n) => _obj = new Implementation.NthDayOfWeek(TestArg, n);
        private void TestMayForNthFriday(int n)
        {
            DateTime testTime = new DateTime(2019, 05, 03);
            for (int i = 0; i < 5; i++)
            {
                if (i + 1 == n)
                    Assert.IsTrue(TestObject.Test(MockToken, testTime));
                else
                    Assert.IsFalse(TestObject.Test(MockToken, testTime));

                testTime = testTime.AddDays(7);
            }
        }
    }
}
