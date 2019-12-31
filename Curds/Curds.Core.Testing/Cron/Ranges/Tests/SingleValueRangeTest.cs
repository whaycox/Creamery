using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Curds.Cron.Ranges.Tests
{
    using Cron.Abstraction;
    using Cron.Template;
    using Implementation;

    [TestClass]
    public class SingleValueRangeTest : FieldDefinitionTemplate
    {
        private int TestValue = 4;
        private DateTime TestTime = DateTime.MinValue;

        private SingleValueRange<ICronFieldDefinition> TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestObject = new SingleValueRange<ICronFieldDefinition>(MockFieldDefinition.Object, TestValue);
        }

        [TestMethod]
        public void IsActiveWhenDatePartIsValue()
        {
            MockFieldDefinition
                .Setup(field => field.SelectDatePart(TestTime))
                .Returns(TestValue);

            Assert.IsTrue(TestObject.IsActive(TestTime));
        }

        [TestMethod]
        public void IsNotActiveWhenDatePartIsntValue()
        {
            MockFieldDefinition
                .Setup(field => field.SelectDatePart(TestTime))
                .Returns(TestValue + 1);

            Assert.IsFalse(TestObject.IsActive(TestTime));
        }
    }
}
