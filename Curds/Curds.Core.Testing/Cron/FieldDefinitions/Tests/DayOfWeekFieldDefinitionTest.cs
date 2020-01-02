using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Curds.Cron.FieldDefinitions.Tests
{
    using Cron.Abstraction;
    using Implementation;
    using Template;

    [TestClass]
    public class DayOfWeekFieldDefinitionTest : BaseFieldDefinitionTemplate
    {
        protected override ICronFieldDefinition InterfaceTestObject { get; } = new DayOfWeekFieldDefinition();

        protected override int ExpectedMin => 0;
        protected override int ExpectedMax => 6;

        protected override Func<DateTime, int> ExpectedDatePart => (time) => (int)time.DayOfWeek;

        [DataTestMethod]
        [DataRow("SUN", 0)]
        [DataRow("MON", 1)]
        [DataRow("TUE", 2)]
        [DataRow("WED", 3)]
        [DataRow("THU", 4)]
        [DataRow("FRI", 5)]
        [DataRow("SAT", 6)]
        public void CanParseDayOfWeekAliases(string aliasedValue, int expectedValue)
        {
            int actual = InterfaceTestObject.Parse(aliasedValue);

            Assert.AreEqual(expectedValue, actual);
        }

        [TestMethod]
        public void ParseIsCaseInsensitive()
        {
            Assert.AreEqual(3, InterfaceTestObject.Parse("WeD"));
        }
    }
}
