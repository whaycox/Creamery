using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Curds.Cron.RangeFactories.Links.Tests
{
    using Cron.Abstraction;
    using Links.Implementation;
    using RangeFactories.Abstraction;
    using FieldDefinitions.Implementation;

    [TestClass]
    public class LinkExtensionsTest
    {
        private Mock<ICronFieldDefinition> MockFieldDefinition = new Mock<ICronFieldDefinition>();
        private Mock<IRangeFactoryLink> MockRangeLink = new Mock<IRangeFactoryLink>();

        private IRangeFactoryLink TestObject => MockRangeLink.Object;

        private void VerifyLinkIsExpected(IRangeFactoryLink rangeLink, Type expectedType)
        {
            Assert.IsInstanceOfType(rangeLink, expectedType);
            Assert.AreSame(TestObject, rangeLink.Successor);
        }

        [TestMethod]
        public void SingleValueAddsLink()
        {
            VerifyLinkIsExpected(TestObject.AddSingleValue(MockFieldDefinition.Object), typeof(SingleValueLink<ICronFieldDefinition>));
        }

        [TestMethod]
        public void WildcardAddsLink()
        {
            VerifyLinkIsExpected(TestObject.AddWildcard(MockFieldDefinition.Object), typeof(WildcardLink<ICronFieldDefinition>));
        }

        [TestMethod]
        public void RangeValueAddsLink()
        {
            VerifyLinkIsExpected(TestObject.AddRangeValue(MockFieldDefinition.Object), typeof(RangeValueLink<ICronFieldDefinition>));
        }

        [TestMethod]
        public void NthDayOfWeekAddsLink()
        {
            DayOfWeekFieldDefinition fieldDefinition = new DayOfWeekFieldDefinition();

            VerifyLinkIsExpected(TestObject.AddNthDayOfWeek(fieldDefinition), typeof(NthDayOfWeekLink));
        }

        [TestMethod]
        public void LastDayOfWeekAddsLink()
        {
            DayOfWeekFieldDefinition fieldDefinition = new DayOfWeekFieldDefinition();

            VerifyLinkIsExpected(TestObject.AddLastDayOfWeek(fieldDefinition), typeof(LastDayOfWeekLink));
        }

        [TestMethod]
        public void NearestWeekdayAddsLink()
        {
            DayOfMonthFieldDefinition fieldDefinition = new DayOfMonthFieldDefinition();

            VerifyLinkIsExpected(TestObject.AddNearestWeekday(fieldDefinition), typeof(NearestWeekdayLink));
        }

        [TestMethod]
        public void LastDayOfMonthAddsLink()
        {
            DayOfMonthFieldDefinition fieldDefinition = new DayOfMonthFieldDefinition();

            VerifyLinkIsExpected(TestObject.AddLastDayOfMonth(fieldDefinition), typeof(LastDayOfMonthLink));
        }
    }
}
