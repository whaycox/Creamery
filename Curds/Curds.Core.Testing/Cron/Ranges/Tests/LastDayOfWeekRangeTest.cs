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
    using Implementation;
    using FieldDefinitions.Implementation;

    [TestClass]
    public class LastDayOfWeekRangeTest
    {
        private DayOfWeekFieldDefinition TestFieldDefinition = new DayOfWeekFieldDefinition();
        private int TestDayOfWeek = 1;

        private LastDayOfWeekRange TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestObject = new LastDayOfWeekRange(TestFieldDefinition, TestDayOfWeek);
        }

        [TestMethod]
        public void ActiveOnlyOnLastDayOfWeekInMonth()
        {
            DateTime testTime = new DateTime(2019, 12, 2);

            Assert.IsFalse(TestObject.IsActive(testTime));
            testTime = testTime.AddDays(7);
            Assert.IsFalse(TestObject.IsActive(testTime));
            testTime = testTime.AddDays(7);
            Assert.IsFalse(TestObject.IsActive(testTime));
            testTime = testTime.AddDays(7);
            Assert.IsFalse(TestObject.IsActive(testTime));
            testTime = testTime.AddDays(7);
            Assert.IsTrue(TestObject.IsActive(testTime));
        }

    }
}
