using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Curds.Cron.FieldDefinitions.Template
{
    using Cron.Abstraction;

    public abstract class BaseFieldDefinitionTemplate
    {
        protected DateTime TestTime = new DateTime(100, 1, 5);

        protected abstract ICronFieldDefinition InterfaceTestObject { get; }

        [TestMethod]
        public void MinIsExpected()
        {
            Assert.AreEqual(ExpectedMin, InterfaceTestObject.AbsoluteMin);
        }
        protected abstract int ExpectedMin { get; }

        [TestMethod]
        public void MaxIsExpected()
        {
            Assert.AreEqual(ExpectedMax, InterfaceTestObject.AbsoluteMax);
        }
        protected abstract int ExpectedMax { get; }

        [TestMethod]
        public void SelectsExpectedDatePart()
        {
            int actual = InterfaceTestObject.SelectDatePart(TestTime);

            Assert.AreEqual(ExpectedDatePart(TestTime), actual);
        }
        protected abstract Func<DateTime, int> ExpectedDatePart { get; }
    }
}
