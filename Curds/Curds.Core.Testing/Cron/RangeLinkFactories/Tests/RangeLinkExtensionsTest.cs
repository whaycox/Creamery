using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Curds.Cron.RangeLinkFactories.Tests
{
    using Cron.Abstraction;
    using RangeLinks.Implementation;
    using FieldDefinitions.Implementation;

    [TestClass]
    public class RangeLinkExtensionsTest
    {
        private Mock<ICronFieldDefinition> MockFieldDefinition = new Mock<ICronFieldDefinition>();
        private Mock<ICronRangeLink> MockRangeLink = new Mock<ICronRangeLink>();

        private ICronRangeLink TestObject => MockRangeLink.Object;

        private void VerifyLinkIsExpected(ICronRangeLink rangeLink, Type expectedType)
        {
            Assert.IsInstanceOfType(rangeLink, expectedType);
            Assert.AreSame(TestObject, rangeLink.Successor);
        }

        [TestMethod]
        public void SingleValueAddsLink()
        {
            VerifyLinkIsExpected(TestObject.AddSingleValue(MockFieldDefinition.Object), typeof(SingleValueRangeLink<ICronFieldDefinition>));
        }

        [TestMethod]
        public void WildcardAddsLink()
        {
            VerifyLinkIsExpected(TestObject.AddWildcard(MockFieldDefinition.Object), typeof(WildcardRangeLink<ICronFieldDefinition>));
        }

        [TestMethod]
        public void RangeValueAddsLink()
        {
            VerifyLinkIsExpected(TestObject.AddRangeValue(MockFieldDefinition.Object), typeof(RangeValueRangeLink<ICronFieldDefinition>));
        }

        [TestMethod]
        public void NthDayOfWeekAddsLink()
        {
            DayOfWeekFieldDefinition fieldDefinition = new DayOfWeekFieldDefinition();

            VerifyLinkIsExpected(TestObject.AddNthDayOfWeek(fieldDefinition), typeof(NthDayOfWeekRangeLink));
        }

        [TestMethod]
        public void LastDayOfWeekAddsLink()
        {
            DayOfWeekFieldDefinition fieldDefinition = new DayOfWeekFieldDefinition();

            VerifyLinkIsExpected(TestObject.AddLastDayOfWeek(fieldDefinition), typeof(LastDayOfWeekRangeLink));
        }

        [TestMethod]
        public void NearestWeekdayAddsLink()
        {
            DayOfMonthFieldDefinition fieldDefinition = new DayOfMonthFieldDefinition();

            VerifyLinkIsExpected(TestObject.AddNearestWeekday(fieldDefinition), typeof(NearestWeekdayRangeLink));
        }

        [TestMethod]
        public void LastDayOfMonthAddsLink()
        {
            DayOfMonthFieldDefinition fieldDefinition = new DayOfMonthFieldDefinition();

            VerifyLinkIsExpected(TestObject.AddLastDayOfMonth(fieldDefinition), typeof(LastDayOfMonthRangeLink));
        }
    }
}
