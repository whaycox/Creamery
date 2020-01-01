using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Curds.Cron.RangeLinks.Tests
{
    using Cron.Abstraction;
    using Implementation;
    using Template;
    using FieldDefinitions.Implementation;
    using Ranges.Implementation;

    [TestClass]
    public class LastDayOfMonthRangeLinkTest : BaseRangeLinkTemplate
    {
        private DayOfMonthFieldDefinition TestFieldDefinition = new DayOfMonthFieldDefinition();

        private LastDayOfMonthRangeLink TestObject = null;
        protected override ICronRangeLink InterfaceTestObject => TestObject;

        [TestInitialize]
        public void Init()
        {
            TestObject = new LastDayOfMonthRangeLink(TestFieldDefinition, MockRangeLink.Object);
        }

        [TestMethod]
        public void ReturnsExpectedRange()
        {
            ICronRange actual = TestObject.HandleParse("L");

            Assert.IsInstanceOfType(actual, typeof(LastDayOfMonthRange));
        }

        [TestMethod]
        public void ParsesCaseInsensitive()
        {
            ICronRange actual = TestObject.HandleParse("l");

            Assert.IsInstanceOfType(actual, typeof(LastDayOfMonthRange));
        }

        [DataTestMethod]
        [DataRow(0)]
        [DataRow(5)]
        [DataRow(10)]
        [DataRow(20)]
        [DataRow(30)]
        public void CanParseWithOffset(int expectedOffset)
        {
            ICronRange actual = TestObject.HandleParse($"L-{expectedOffset}");

            Assert.IsInstanceOfType(actual, typeof(LastDayOfMonthRange));
            LastDayOfMonthRange range = (LastDayOfMonthRange)actual;
            Assert.AreEqual(expectedOffset, range.Offset);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void InvalidOffsetThrows()
        {
            TestObject.HandleParse("L-31");
        }
    }
}
