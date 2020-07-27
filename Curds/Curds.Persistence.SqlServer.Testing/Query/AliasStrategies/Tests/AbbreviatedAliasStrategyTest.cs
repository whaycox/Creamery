using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Persistence.Query.AliasStrategies.Tests
{
    using Implementation;

    [TestClass]
    public class AbbreviatedAliasStrategyTest
    {
        private AbbreviatedAliasStrategy TestObject = new AbbreviatedAliasStrategy();

        [DataTestMethod]
        [DataRow("test", "t{0}")]
        [DataRow("testEntity", "tE{0}")]
        [DataRow("TestEntity", "TE{0}")]
        [DataRow("Test Entity Other", "TEO{0}")]
        public void GeneratedAliasIsExpected(string testObjectName, string expectedTemplate)
        {
            for (int i = 0; i < 10; i++)
            {
                string actual = TestObject.GenerateAlias(testObjectName, i);

                Assert.AreEqual(string.Format(expectedTemplate, i), actual);
            }
        }
    }
}
