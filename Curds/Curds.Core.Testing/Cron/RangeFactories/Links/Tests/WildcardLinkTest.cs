using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Cron.RangeFactories.Links.Tests
{
    using Cron.Abstraction;
    using Implementation;
    using Ranges.Implementation;
    using Template;
    using RangeFactories.Abstraction;

    [TestClass]
    public class WildcardLinkTest : BaseLinkTemplate
    {
        private WildcardLink<ICronFieldDefinition> TestObject = null;
        protected override IRangeFactoryLink InterfaceTestObject => TestObject;

        [TestInitialize]
        public void Init()
        {
            TestObject = new WildcardLink<ICronFieldDefinition>(MockFieldDefinition.Object, MockRangeLink.Object);
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
