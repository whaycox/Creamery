using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Curds.Cron.Ranges.Tests
{
    using FieldDefinitions.Implementation;
    using Implementation;

    [TestClass]
    public class NthDayOfWeekRangeTest
    {
        private DayOfWeekFieldDefinition TestFieldDefinition = new DayOfWeekFieldDefinition();
        private int TestDayOfWeek = 2;
        private int TestStepValue = 3;

        private NthDayOfWeekRange TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestObject = new NthDayOfWeekRange(TestFieldDefinition, TestDayOfWeek, TestStepValue);
        }

        [TestMethod]
        public void IsActiveOnlyOnNthDayOfWeek()
        {
            DateTime testTime = new DateTime(2019, 12, 3);

            Assert.IsFalse(TestObject.IsActive(testTime));
            testTime = testTime.AddDays(7);
            Assert.IsFalse(TestObject.IsActive(testTime));
            testTime = testTime.AddDays(7);
            Assert.IsTrue(TestObject.IsActive(testTime));
            testTime = testTime.AddDays(7);
            Assert.IsFalse(TestObject.IsActive(testTime));
            testTime = testTime.AddDays(7);
            Assert.IsFalse(TestObject.IsActive(testTime));
        }


    }
}
