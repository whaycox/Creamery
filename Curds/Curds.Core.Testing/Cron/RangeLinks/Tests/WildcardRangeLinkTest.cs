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
        private WildcardRangeLink TestObject = null;
        protected override ICronRangeLink InterfaceTestObject => TestObject;

        [TestInitialize]
        public void Init()
        {
            TestObject = new WildcardRangeLink(MockRangeLink.Object);
        }

        [TestMethod]
        public void ReturnsExpectedRange()
        {
            ICronRange actual = TestObject.HandleParse("*");

            Assert.IsInstanceOfType(actual, typeof(WildcardRange));
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
