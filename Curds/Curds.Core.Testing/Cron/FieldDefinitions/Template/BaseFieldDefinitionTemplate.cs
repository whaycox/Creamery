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
        public void ParsesInput()
        {
            int actual = InterfaceTestObject.Parse("5");

            Assert.AreEqual(5, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ParseThrowsOnNonInt()
        {
            InterfaceTestObject.Parse(nameof(ParseThrowsOnNonInt));
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ParseThrowsBelowMin()
        {
            InterfaceTestObject.Parse((ExpectedMin - 1).ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ParseThrowsAboveMax()
        {
            InterfaceTestObject.Parse((ExpectedMax + 1).ToString());
        }

        [TestMethod]
        public void SelectsExpectedDatePart()
        {
            int actual = InterfaceTestObject.SelectDatePart(TestTime);

            Assert.AreEqual(ExpectedDatePart(TestTime), actual);
        }
        protected abstract Func<DateTime, int> ExpectedDatePart { get; }
    }
}
