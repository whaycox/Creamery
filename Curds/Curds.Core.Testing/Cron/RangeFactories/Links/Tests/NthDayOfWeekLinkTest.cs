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
    using FieldDefinitions.Implementation;
    using Cron.Abstraction;
    using Ranges.Implementation;
    using RangeFactories.Abstraction;

    [TestClass]
    public class NthDayOfWeekLinkTest : BaseLinkTemplate
    {
        private DayOfWeekFieldDefinition TestFieldDefinition = new DayOfWeekFieldDefinition();

        private NthDayOfWeekLink TestObject = null;
        protected override IRangeFactoryLink InterfaceTestObject => TestObject;

        [TestInitialize]
        public void Init()
        {
            TestObject = new NthDayOfWeekLink(TestFieldDefinition, MockRangeLink.Object);
        }

        [TestMethod]
        public void ReturnsExpectedRange()
        {
            ICronRange actual = TestObject.HandleParse("4#3");

            Assert.IsInstanceOfType(actual, typeof(NthDayOfWeekRange));
            NthDayOfWeekRange range = (NthDayOfWeekRange)actual;
            Assert.AreEqual(4, range.DayOfWeek);
            Assert.AreEqual(3, range.NthValue);
        }

        [TestMethod]
        public void LooksupAliasForRange()
        {
            ICronRange actual = TestObject.HandleParse("FRI#5");

            Assert.IsInstanceOfType(actual, typeof(NthDayOfWeekRange));
            NthDayOfWeekRange range = (NthDayOfWeekRange)actual;
            Assert.AreEqual(5, range.DayOfWeek);
            Assert.AreEqual(5, range.NthValue);
        }

        [DataTestMethod]
        [DataRow("")]
        [DataRow("  ")]
        [DataRow("5")]
        [DataRow("5/3")]
        [DataRow("*")]
        public void ReturnsNullOnWrongPattern(string testRange)
        {
            Assert.IsNull(TestObject.HandleParse(testRange));
        }
    }
}
