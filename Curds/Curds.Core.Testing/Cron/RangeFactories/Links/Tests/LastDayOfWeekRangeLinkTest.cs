using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Curds.Cron.RangeFactories.Links.Tests
{
    using Template;
    using Implementation;
    using Cron.Abstraction;
    using FieldDefinitions.Implementation;
    using Ranges.Implementation;
    using RangeFactories.Abstraction;

    [TestClass]
    public class LastDayOfWeekRangeLinkTest : BaseRangeLinkTemplate
    {
        private DayOfWeekFieldDefinition TestFieldDefinition = new DayOfWeekFieldDefinition();

        private LastDayOfWeekRangeLink TestObject = null;
        protected override IRangeFactoryLink InterfaceTestObject => TestObject;

        [TestInitialize]
        public void Init()
        {
            TestObject = new LastDayOfWeekRangeLink(TestFieldDefinition, MockRangeLink.Object);
        }

        [TestMethod]
        public void ReturnsExpectedRange()
        {
            ICronRange actual = TestObject.HandleParse("3L");

            Assert.IsInstanceOfType(actual, typeof(LastDayOfWeekRange));
            LastDayOfWeekRange range = (LastDayOfWeekRange)actual;
            Assert.AreEqual(3, range.DayOfWeek);
        }

        [TestMethod]
        public void LooksupAliasValue()
        {
            ICronRange actual = TestObject.HandleParse("FRIl");

            Assert.IsInstanceOfType(actual, typeof(LastDayOfWeekRange));
            LastDayOfWeekRange range = (LastDayOfWeekRange)actual;
            Assert.AreEqual(5, range.DayOfWeek);
        }

        [DataTestMethod]
        [DataRow("")]
        [DataRow("  ")]
        [DataRow("5")]
        [DataRow("5#2")]
        [DataRow("TESTL")]
        public void ReturnsNullOnWrongPattern(string testRange)
        {
            Assert.IsNull(TestObject.HandleParse(testRange));
        }

    }
}
