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
    public class RangeValueLinkTest : BaseLinkTemplate
    {
        private RangeValueLink<ICronFieldDefinition> TestObject = null;
        protected override IRangeFactoryLink InterfaceTestObject => TestObject;

        private string TestRange(int low, int high) => $"{low}-{high}";

        [TestInitialize]
        public void Init()
        {
            MockFieldDefinition
                .Setup(field => field.Parse(TestAbsoluteMin.ToString()))
                .Returns(TestAbsoluteMin);
            MockFieldDefinition
                .Setup(field => field.Parse(TestAbsoluteMax.ToString()))
                .Returns(TestAbsoluteMax);

            TestObject = new RangeValueLink<ICronFieldDefinition>(MockFieldDefinition.Object, MockRangeLink.Object);
        }

        [TestMethod]
        public void ParsesBothValues()
        {
            TestObject.HandleParse(TestRange(TestAbsoluteMin, TestAbsoluteMax));

            MockFieldDefinition.Verify(field => field.Parse(TestAbsoluteMin.ToString()), Times.Once);
            MockFieldDefinition.Verify(field => field.Parse(TestAbsoluteMax.ToString()), Times.Once);
        }

        [TestMethod]
        public void ReturnsExpectedRange()
        {
            ICronRange actual = TestObject.HandleParse(TestRange(TestAbsoluteMin, TestAbsoluteMax));

            Assert.IsInstanceOfType(actual, typeof(RangeValueRange<ICronFieldDefinition>));
        }

        [TestMethod]
        public void ReturnedRangeHasLowPopulatedFromParse()
        {
            ICronRange actual = TestObject.HandleParse(TestRange(TestAbsoluteMin, TestAbsoluteMax));

            RangeValueRange<ICronFieldDefinition> range = (RangeValueRange<ICronFieldDefinition>)actual;
            Assert.AreEqual(TestAbsoluteMin, range.Low);
        }

        [TestMethod]
        public void ReturnedRangeHasHighPopulated()
        {
            ICronRange actual = TestObject.HandleParse(TestRange(TestAbsoluteMin, TestAbsoluteMax));

            RangeValueRange<ICronFieldDefinition> range = (RangeValueRange<ICronFieldDefinition>)actual;
            Assert.AreEqual(TestAbsoluteMax, range.High);
        }

        [DataTestMethod]
        [DataRow("")]
        [DataRow("  ")]
        [DataRow("5")]
        [DataRow("-5")]
        [DataRow("5-")]
        [DataRow("5--5")]
        [DataRow("Test-Test")]
        [DataRow("Test")]
        public void ReturnsNullOnNonRange(string testRange)
        {
            Assert.IsNull(TestObject.HandleParse(testRange));
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ThrowsIfRangeIsInverted()
        {
            TestObject.HandleParse(TestRange(TestAbsoluteMax, TestAbsoluteMin));
        }
    }
}
