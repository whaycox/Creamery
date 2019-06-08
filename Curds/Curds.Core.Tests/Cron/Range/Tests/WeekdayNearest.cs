using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Curds.Cron.Range.Tests
{
    using Enumeration;

    [TestClass]
    public class WeekdayNearest : Template.Basic<Implementation.WeekdayNearest>
    {
        private const int TestingDayOfMonth = 15;
        private static int DayBefore => TestingDayOfMonth - 1;
        private static int DayAfter => TestingDayOfMonth + 1;

        protected override ExpressionPart TestingPart => ExpressionPart.DayOfMonth;

        protected override Implementation.WeekdayNearest TestObject { get; } = new Implementation.WeekdayNearest(TestingDayOfMonth);

        private void TestAMonth(int year, int month)
        {
            DateTime testTime = new DateTime(year, month, 1);
            DateTime fireTime = new DateTime(year, month, TestingDayOfMonth);
            int daysInMonth = DateTime.DaysInMonth(year, month);
            for (int i = 1; i <= daysInMonth; i++)
            {
                if (i == DayBefore && testTime.DayOfWeek == DayOfWeek.Friday)
                    Assert.IsTrue(TestObject.Test(MockToken, testTime));
                else if (i == TestingDayOfMonth && IsAWeekday(testTime))
                    Assert.IsTrue(TestObject.Test(MockToken, testTime));
                else if (i == DayAfter && testTime.DayOfWeek == DayOfWeek.Monday)
                    Assert.IsTrue(TestObject.Test(MockToken, testTime));
                else
                    Assert.IsFalse(TestObject.Test(MockToken, testTime));

                testTime = testTime.AddDays(1);
            }
        }
        private bool IsAWeekday(DateTime testTime) => testTime.DayOfWeek != DayOfWeek.Saturday && testTime.DayOfWeek != DayOfWeek.Sunday;

        [TestMethod]
        public void IsTrueOnTheCorrectDay()
        {
            TestWhenOnMidweek();
            TestWhenOnFriday();
            TestWhenOnSaturday();
            TestWhenOnSunday();
            TestWhenOnMonday();
        }
        private void TestWhenOnMidweek() => TestAMonth(2019, 5);
        private void TestWhenOnFriday() => TestAMonth(2019, 3);
        private void TestWhenOnSaturday() => TestAMonth(2018, 12);
        private void TestWhenOnSunday() => TestAMonth(2018, 7);
        private void TestWhenOnMonday() => TestAMonth(2019, 4);
    }
}
