using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Curds.Cron.Ranges.Tests
{
    using FieldDefinitions.Implementation;
    using Implementation;

    [TestClass]
    public class LastDayOfMonthRangeTest
    {
        private DayOfMonthFieldDefinition TestFieldDefinition = new DayOfMonthFieldDefinition();

        private LastDayOfMonthRange TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestObject = new LastDayOfMonthRange(TestFieldDefinition, default);
        }

        private void TestThreeConsecutiveDays(DateTime testTime)
        {
            Assert.IsFalse(TestObject.IsActive(testTime));
            testTime = testTime.AddDays(1);
            Assert.IsTrue(TestObject.IsActive(testTime));
            testTime = testTime.AddDays(1);
            Assert.IsFalse(TestObject.IsActive(testTime));
        }

        [TestMethod]
        public void IsActiveOnlyOnLastDayOfMonth()
        {
            TestThreeConsecutiveDays(new DateTime(2020, 2, 28));
            TestThreeConsecutiveDays(new DateTime(2019, 2, 27));
            TestThreeConsecutiveDays(new DateTime(2019, 3, 30));
        }

        [DataTestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(10)]
        [DataRow(15)]
        public void ObeysOffsetLastDayOfMonth(int testOffset)
        {
            TestObject = new LastDayOfMonthRange(TestFieldDefinition, testOffset);

            TestThreeConsecutiveDays(new DateTime(2020, 2, 28).AddDays(-testOffset));
            TestThreeConsecutiveDays(new DateTime(2019, 2, 27).AddDays(-testOffset));
            TestThreeConsecutiveDays(new DateTime(2019, 3, 30).AddDays(-testOffset));
        }

        [TestMethod]
        public void OffsetLargerThanMonthIsActiveOnFirst()
        {
            TestObject = new LastDayOfMonthRange(TestFieldDefinition, 30);

            TestThreeConsecutiveDays(new DateTime(2020, 1, 31));
        }
    }
}
