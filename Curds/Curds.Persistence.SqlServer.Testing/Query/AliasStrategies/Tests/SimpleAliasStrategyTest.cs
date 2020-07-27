using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Persistence.Query.AliasStrategies.Tests
{
    using Implementation;

    [TestClass]
    public class SimpleAliasStrategyTest
    {
        private string TestObjectName = nameof(TestObjectName);
        private int TestDisambiguator = 6;

        private SimpleAliasStrategy TestObject = new SimpleAliasStrategy();

        [TestMethod]
        public void ReturnsExpertedCombination()
        {
            string actual = TestObject.GenerateAlias(TestObjectName, TestDisambiguator);

            Assert.AreEqual($"{TestObjectName}_{TestDisambiguator}", actual);
        }
    }
}
