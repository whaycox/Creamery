using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Curds.Cron.FieldDefinitions.Tests
{
    using Cron.Abstraction;
    using Implementation;
    using Template;

    [TestClass]
    public class MonthFieldDefinitionTest : BaseFieldDefinitionTemplate
    {
        protected override ICronFieldDefinition InterfaceTestObject { get; } = new MonthFieldDefinition();

        protected override int ExpectedMin => 1;
        protected override int ExpectedMax => 12;

        protected override Func<DateTime, int> ExpectedDatePart => (time) => time.Month;

        [DataTestMethod]
        [DataRow("JAN", 1)]
        [DataRow("FEB", 2)]
        [DataRow("MAR", 3)]
        [DataRow("APR", 4)]
        [DataRow("MAY", 5)]
        [DataRow("JUN", 6)]
        [DataRow("JUL", 7)]
        [DataRow("AUG", 8)]
        [DataRow("SEP", 9)]
        [DataRow("OCT", 10)]
        [DataRow("NOV", 11)]
        [DataRow("DEC", 12)]
        public void CanParseMonthAliases(string aliasedValue, int expectedValue)
        {
            int actual = InterfaceTestObject.Parse(aliasedValue);

            Assert.AreEqual(expectedValue, actual);
        }

        [TestMethod]
        public void ParseIsCaseInsensitive()
        {
            Assert.AreEqual(1, InterfaceTestObject.Parse("jAn"));
        }
    }
}
