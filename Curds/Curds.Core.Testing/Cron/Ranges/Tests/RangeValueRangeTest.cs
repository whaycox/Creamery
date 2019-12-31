using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Curds.Cron.Ranges.Tests
{
    using Cron.Abstraction;
    using Cron.Template;
    using Implementation;

    [TestClass]
    public class RangeValueRangeTest : FieldDefinitionTemplate
    {
        private DateTime TestTime = DateTime.MinValue;

        private RangeValueRange<ICronFieldDefinition> TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestObject = new RangeValueRange<ICronFieldDefinition>(MockFieldDefinition.Object, TestAbsoluteMin, TestAbsoluteMax);
        }

        [DataTestMethod]
        [DataRow(3)]
        [DataRow(4)]
        [DataRow(5)]
        public void IsActiveWhenValueIsInsideRange(int testValue)
        {
            MockFieldDefinition
                .Setup(field => field.SelectDatePart(TestTime))
                .Returns(testValue);

            Assert.IsTrue(TestObject.IsActive(TestTime));
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(6)]
        [DataRow(7)]
        public void IsNotActiveWhenValueIsOutsideRange(int testValue)
        {
            MockFieldDefinition
                .Setup(field => field.SelectDatePart(TestTime))
                .Returns(testValue);

            Assert.IsFalse(TestObject.IsActive(TestTime));
        }
    }
}
