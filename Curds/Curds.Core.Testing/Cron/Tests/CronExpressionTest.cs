using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace Curds.Cron.Tests
{
    using Abstraction;
    using Implementation;

    [TestClass]
    public class CronExpressionTest
    {
        private DateTime TestTime = new DateTime(1, 1, 1);

        private Mock<ICronField> MockFirstField = new Mock<ICronField>();
        private Mock<ICronField> MockSecondField = new Mock<ICronField>();

        private CronExpression TestObject = null;

        [TestInitialize]
        public void Init()
        {
            List<ICronField> mockFields = new List<ICronField> { MockFirstField.Object, MockSecondField.Object };
            TestObject = new CronExpression(mockFields);
        }

        [DataTestMethod]
        [DataRow(false, true)]
        [DataRow(true, false)]
        [DataRow(false, false)]
        public void IsActiveFalseIfAnyFieldFalse(bool firstActive, bool secondActive)
        {
            MockFirstField
                .Setup(field => field.IsActive(It.IsAny<DateTime>()))
                .Returns(firstActive);
            MockSecondField
                .Setup(field => field.IsActive(It.IsAny<DateTime>()))
                .Returns(secondActive);

            Assert.IsFalse(TestObject.IsActive(TestTime));
        }

        [TestMethod]
        public void IsActiveTrueIfAllFieldsTrue()
        {
            MockFirstField
                .Setup(field => field.IsActive(It.IsAny<DateTime>()))
                .Returns(true);
            MockSecondField
                .Setup(field => field.IsActive(It.IsAny<DateTime>()))
                .Returns(true);

            Assert.IsTrue(TestObject.IsActive(TestTime));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullFieldsThrows()
        {
            new CronExpression(null);
        }
    }
}
