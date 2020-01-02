using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;

namespace Curds.Cron.RangeFactories.Links.Tests
{
    using Cron.Abstraction;
    using Implementation;
    using Ranges.Implementation;
    using Template;
    using RangeFactories.Abstraction;

    [TestClass]
    public class SingleValueLinkTest : BaseLinkTemplate
    {
        private string TestMinRange => TestAbsoluteMin.ToString();
        private string TestMaxRange => TestAbsoluteMax.ToString();

        private SingleValueLink<ICronFieldDefinition> TestObject = null;
        protected override IRangeFactoryLink InterfaceTestObject => TestObject;

        [TestInitialize]
        public void Init()
        {
            MockFieldDefinition
                .Setup(field => field.Parse(TestMinRange))
                .Returns(TestAbsoluteMin);
            MockFieldDefinition
                .Setup(field => field.Parse(TestMaxRange))
                .Returns(TestAbsoluteMax);

            TestObject = new SingleValueLink<ICronFieldDefinition>(MockFieldDefinition.Object, MockRangeLink.Object);
        }

        [TestMethod]
        public void ParsesValue()
        {
            TestObject.HandleParse(TestMinRange);

            MockFieldDefinition.Verify(field => field.Parse(TestMinRange), Times.Once);
        }

        [TestMethod]
        public void ReturnsExpectedRange()
        {
            ICronRange actual = TestObject.HandleParse(TestMinRange);

            Assert.IsInstanceOfType(actual, typeof(SingleValueRange<ICronFieldDefinition>));
        }

        [TestMethod]
        public void ReturnedRangeHasExpectedValue()
        {
            ICronRange actual = TestObject.HandleParse(TestMaxRange);

            SingleValueRange<ICronFieldDefinition> range = (SingleValueRange<ICronFieldDefinition>)actual;
            Assert.AreEqual(TestAbsoluteMax, range.Value);
        }
    }
}
