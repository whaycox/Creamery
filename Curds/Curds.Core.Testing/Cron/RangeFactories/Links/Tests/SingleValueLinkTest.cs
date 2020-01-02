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
                .Setup(field => field.LookupAlias(It.IsAny<string>()))
                .Returns<string>(supplied => supplied);

            TestObject = new SingleValueLink<ICronFieldDefinition>(MockFieldDefinition.Object, MockRangeLink.Object);
        }

        [TestMethod]
        public void LooksupAliasedValue()
        {
            TestObject.HandleParse(TestMinRange);

            MockFieldDefinition.Verify(field => field.LookupAlias(TestMinRange), Times.Once);
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

        [DataTestMethod]
        [DataRow("")]
        [DataRow("  ")]
        [DataRow("test")]
        [DataRow("1e5")]
        public void NullOnNonInt(string testRange)
        {
            Assert.IsNull(TestObject.HandleParse(testRange));
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void LessThanAbsoluteMinThrows()
        {
            string invalidValue = (TestAbsoluteMin - 1).ToString();

            TestObject.HandleParse(invalidValue);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void MoreThanAbsoluteMaxThrows()
        {
            string invalidValue = (TestAbsoluteMax + 1).ToString();

            TestObject.HandleParse(invalidValue);
        }
    }
}
