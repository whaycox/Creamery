using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Curds.Cron.RangeFactories.Links.Tests
{
    using Cron.Abstraction;
    using FieldDefinitions.Implementation;
    using Implementation;
    using Ranges.Implementation;
    using Template;
    using RangeFactories.Abstraction;

    [TestClass]
    public class NearestWeekdayRangeLinkTest : BaseRangeLinkTemplate
    {
        private DayOfMonthFieldDefinition TestFieldDefinition = new DayOfMonthFieldDefinition();

        private NearestWeekdayRangeLink TestObject = null;
        protected override IRangeFactoryLink InterfaceTestObject => TestObject;

        [TestInitialize]
        public void Init()
        {
            TestObject = new NearestWeekdayRangeLink(TestFieldDefinition, MockRangeLink.Object);
        }

        [TestMethod]
        public void ReturnsExpectedRange()
        {
            ICronRange actual = TestObject.HandleParse("5W");

            Assert.IsInstanceOfType(actual, typeof(NearestWeekdayRange));
            NearestWeekdayRange range = (NearestWeekdayRange)actual;
            Assert.AreEqual(5, range.DayOfMonth);
        }

        [TestMethod]
        public void ParsesCaseInsensitive()
        {
            ICronRange actual = TestObject.HandleParse("3w");

            Assert.IsInstanceOfType(actual, typeof(NearestWeekdayRange));
            NearestWeekdayRange range = (NearestWeekdayRange)actual;
            Assert.AreEqual(3, range.DayOfMonth);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void DayOfMonthAboveMaxThrows()
        {
            TestObject.HandleParse("40W");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void DayOfMonthBelowMinThrows()
        {
            TestObject.HandleParse("0W");
        }

        [DataTestMethod]
        [DataRow("")]
        [DataRow("  ")]
        [DataRow("W15")]
        [DataRow("1W5")]
        [DataRow("L")]
        [DataRow("5#3")]
        public void ReturnsNullOnWrongPattern(string testRange)
        {
            Assert.IsNull(TestObject.HandleParse(testRange));
        }
    }
}
