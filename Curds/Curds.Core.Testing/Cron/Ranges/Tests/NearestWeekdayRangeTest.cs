using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Curds.Cron.Ranges.Tests
{
    using FieldDefinitions.Implementation;
    using Cron.Abstraction;
    using Implementation;

    [TestClass]
    public class NearestWeekdayRangeTest
    {
        private DayOfMonthFieldDefinition TestFieldDefinition = new DayOfMonthFieldDefinition();
        private int TestDayOfMonth = 15;

        private NearestWeekdayRange TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestObject = new NearestWeekdayRange(TestFieldDefinition, TestDayOfMonth);
        }

        [TestMethod]
        public void IsActiveWhenDayIsWeekday()
        {
            DateTime testTime = new DateTime(2019, 1, 15);

            Assert.IsTrue(TestObject.IsActive(testTime));
            testTime = testTime.AddMonths(1);
            Assert.IsTrue(TestObject.IsActive(testTime));
            testTime = testTime.AddMonths(1);
            Assert.IsTrue(TestObject.IsActive(testTime));
            testTime = testTime.AddMonths(1);
            Assert.IsTrue(TestObject.IsActive(testTime));
            testTime = testTime.AddMonths(1);
            Assert.IsTrue(TestObject.IsActive(testTime));
        }

        [TestMethod]
        public void IsNotActiveWhenDayIsWeekend()
        {
            DateTime testTime = new DateTime(2019, 6, 15);

            Assert.IsFalse(TestObject.IsActive(testTime));
        }

        [TestMethod]
        public void IsActiveOnFridayWhenDayIsSaturday()
        {
            DateTime testTime = new DateTime(2019, 6, 14);

            Assert.IsTrue(TestObject.IsActive(testTime));
        }

        [TestMethod]
        public void IsActiveOnMondayWhenDayIsSunday()
        {
            DateTime testTime = new DateTime(2019, 9, 16);

            Assert.IsTrue(TestObject.IsActive(testTime));
        }
    }
}
