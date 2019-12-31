using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Curds.Cron.RangeLinkFactories.Tests
{
    using Cron.Abstraction;
    using RangeLinks.Implementation;

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
        public void SingleValueAddsSingleValueLink()
        {
            VerifyLinkIsExpected(TestObject.AddSingleValue(MockFieldDefinition.Object), typeof(SingleValueRangeLink<ICronFieldDefinition>));
        }

        [TestMethod]
        public void WildcardAddsWildcardLink()
        {
            VerifyLinkIsExpected(TestObject.AddWildcard(), typeof(WildcardRangeLink));
        }

        [TestMethod]
        public void RangeValueAddsRangeValueLink()
        {
            VerifyLinkIsExpected(TestObject.AddRangeValue(MockFieldDefinition.Object), typeof(RangeValueRangeLink<ICronFieldDefinition>));
        }
    }
}
