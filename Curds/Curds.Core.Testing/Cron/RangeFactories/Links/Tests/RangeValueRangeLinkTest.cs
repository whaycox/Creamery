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
    public class RangeValueRangeLinkTest : BaseRangeLinkTemplate
    {
        private RangeValueRangeLink<ICronFieldDefinition> TestObject = null;
        protected override IRangeFactoryLink InterfaceTestObject => TestObject;

        private string TestRange(int low, int high) => $"{low}-{high}";

        [TestInitialize]
        public void Init()
        {
            MockFieldDefinition
                .Setup(field => field.LookupAlias(It.IsAny<string>()))
                .Returns<string>(supplied => supplied);

            TestObject = new RangeValueRangeLink<ICronFieldDefinition>(MockFieldDefinition.Object, MockRangeLink.Object);
        }

        [TestMethod]
        public void LooksupAliasedValues()
        {
            TestObject.HandleParse(TestRange(TestAbsoluteMin, TestAbsoluteMax));

            MockFieldDefinition.Verify(field => field.LookupAlias(TestAbsoluteMin.ToString()), Times.Once);
            MockFieldDefinition.Verify(field => field.LookupAlias(TestAbsoluteMax.ToString()), Times.Once);
        }

        [TestMethod]
        public void ReturnsExpectedRange()
        {
            ICronRange actual = TestObject.HandleParse(TestRange(TestAbsoluteMin, TestAbsoluteMax));

            Assert.IsInstanceOfType(actual, typeof(RangeValueRange<ICronFieldDefinition>));
        }

        [TestMethod]
        public void ReturnedRangeHasLowPopulated()
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

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ThrowsIfLowEndIsLessThanAbsoluteMin()
        {
            TestObject.HandleParse(TestRange(TestAbsoluteMin - 1, TestAbsoluteMax));
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ThrowsIfHighEndIsLessThanAbsoluteMin()
        {
            TestObject.HandleParse(TestRange(TestAbsoluteMin, TestAbsoluteMin - 1));
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ThrowsIfLowEndIsMoreThanAbsoluteMax()
        {
            TestObject.HandleParse(TestRange(TestAbsoluteMax + 1, TestAbsoluteMax));
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ThrowsIfHighEndIsMoreThanAbsoluteMax()
        {
            TestObject.HandleParse(TestRange(TestAbsoluteMin, TestAbsoluteMax + 1));
        }
    }
}
