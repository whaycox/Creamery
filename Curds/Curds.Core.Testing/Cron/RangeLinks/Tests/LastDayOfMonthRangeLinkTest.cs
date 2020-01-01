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
            ICronRange range = TestObject.HandleParse("L");

            Assert.IsInstanceOfType(range, typeof(LastDayOfMonthRange));
        }

        [TestMethod]
        public void ParsesCaseInsensitive()
        {
            ICronRange range = TestObject.HandleParse("l");

            Assert.IsInstanceOfType(range, typeof(LastDayOfMonthRange));
        }
    }
}
