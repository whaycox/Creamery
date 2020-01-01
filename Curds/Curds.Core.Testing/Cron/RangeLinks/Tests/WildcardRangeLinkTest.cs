using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Cron.RangeLinks.Tests
{
    using Cron.Abstraction;
    using Implementation;
    using Ranges.Implementation;
    using Template;

    [TestClass]
    public class WildcardRangeLinkTest : BaseRangeLinkTemplate
    {
        private WildcardRangeLink<ICronFieldDefinition> TestObject = null;
        protected override ICronRangeLink InterfaceTestObject => TestObject;

        [TestInitialize]
        public void Init()
        {
            TestObject = new WildcardRangeLink<ICronFieldDefinition>(MockFieldDefinition.Object, MockRangeLink.Object);
        }

        [TestMethod]
        public void ReturnsExpectedRange()
        {
            ICronRange actual = TestObject.HandleParse("*");

            Assert.IsInstanceOfType(actual, typeof(WildcardRange));
        }

        [TestMethod]
        public void ReturnsStepRange()
        {
            int expectedStep = 5;

            ICronRange actual = TestObject.HandleParse($"*/{expectedStep}");

            Assert.IsInstanceOfType(actual, typeof(StepRange<ICronFieldDefinition>));
            StepRange<ICronFieldDefinition> range = (StepRange<ICronFieldDefinition>)actual;
            Assert.AreEqual(expectedStep, range.StepValue);
        }

        [DataTestMethod]
        [DataRow("")]
        [DataRow("  ")]
        [DataRow("5")]
        [DataRow("Test")]
        public void NullOnNonWildcard(string testRange)
        {
            Assert.IsNull(TestObject.HandleParse(testRange));
        }
    }
}
