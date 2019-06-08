using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Curds.Cron.Range.Tests
{
    using Enumeration;

    [TestClass]
    public class LastDayOfMonth : Template.Basic<Implementation.LastDayOfMonth>
    {
        private Implementation.LastDayOfMonth _obj = new Implementation.LastDayOfMonth();
        protected override Implementation.LastDayOfMonth TestObject => _obj;

        protected override ExpressionPart TestingPart => ExpressionPart.DayOfMonth;

        [TestMethod]
        public void NegativeOffsetThrows()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Implementation.LastDayOfMonth(-1));
        }

        [TestMethod]
        public void EnforcesAMaxOffset()
        {
            new Implementation.LastDayOfMonth(Implementation.LastDayOfMonth.MaxOffset);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Implementation.LastDayOfMonth(Implementation.LastDayOfMonth.MaxOffset + 1));
        }

        private void TestInMonth(DateTime startOfMonth, int offset = 0)
        {
            int daysInMonth = DateTime.DaysInMonth(startOfMonth.Year, startOfMonth.Month);

            DateTime testTime = startOfMonth;
            for (int i = 0; i < daysInMonth; i++)
            {
                if (i == daysInMonth - (1 + offset))
                    Assert.IsTrue(TestObject.Test(MockToken, testTime));
                else
                    Assert.IsFalse(TestObject.Test(MockToken, testTime));
                testTime = testTime.AddDays(1);
            }
        }

        [TestMethod]
        public void IsTrueOnlyOnLastDayOfMonth()
        {
            TestInMay();
            TestInFeb();
            TestInApril();
            TestInLeapFeb();
        }
        private bool TestTime(DateTime testTime) => TestObject.Test(MockToken, testTime);
        private int DatePart(DateTime testTime) => testTime.Day;
        private void TestInMay() => TestInMonth(new DateTime(2019, 5, 1));
        private void TestInFeb() => TestInMonth(new DateTime(2019, 2, 1));
        private void TestInApril() => TestInMonth(new DateTime(2019, 4, 1));
        private void TestInLeapFeb() => TestInMonth(new DateTime(2016, 2, 1));

        [TestMethod]
        public void ObeysOffset()
        {
            for (int i = 0; i <= Implementation.LastDayOfMonth.MaxOffset; i++)
            {
                _obj = new Implementation.LastDayOfMonth(i);
                TestInMonth(new DateTime(2019, 5, 1), i);
            }
        }
    }
}
