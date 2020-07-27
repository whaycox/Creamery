using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Curds.Persistence.Query.Tests
{
    using Abstraction;
    using Implementation;

    [TestClass]
    public class SqlQueryAliasBuilderTest
    {
        private string TestObjectName = nameof(TestObjectName);
        private string TestGeneratedAlias = nameof(TestGeneratedAlias);

        private Mock<IAliasStrategy> MockAliasStrategy = new Mock<IAliasStrategy>();

        private SqlQueryAliasBuilder TestObject = null;

        [TestInitialize]
        public void Init()
        {
            MockAliasStrategy
                .Setup(strategy => strategy.GenerateAlias(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(TestGeneratedAlias);

            TestObject = new SqlQueryAliasBuilder(MockAliasStrategy.Object);
        }

        [TestMethod]
        public void RequestsAliasFromStrategy()
        {
            TestObject.RegisterNewAlias(TestObjectName);

            MockAliasStrategy.Verify(strategy => strategy.GenerateAlias(TestObjectName, 1), Times.Once);
        }

        [TestMethod]
        public void ReturnsGeneratedAlias()
        {
            string actual = TestObject.RegisterNewAlias(TestObjectName);

            Assert.AreEqual(TestGeneratedAlias, actual);
        }

        [TestMethod]
        public void IncrementsDisambiguatorWhileReturningUsedResult()
        {
            MockAliasStrategy
                .SetupSequence(strategy => strategy.GenerateAlias(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(TestGeneratedAlias)
                .Returns(TestGeneratedAlias)
                .Returns(TestGeneratedAlias)
                .Returns(nameof(IncrementsDisambiguatorWhileReturningUsedResult));
            TestObject.RegisterNewAlias(TestObjectName);

            TestObject.RegisterNewAlias(TestObjectName);

            MockAliasStrategy.Verify(strategy => strategy.GenerateAlias(TestObjectName, 1), Times.Exactly(2));
            MockAliasStrategy.Verify(strategy => strategy.GenerateAlias(TestObjectName, 2), Times.Once);
            MockAliasStrategy.Verify(strategy => strategy.GenerateAlias(TestObjectName, 3), Times.Once);
        }

        [TestMethod]
        public void ReturnsFirstUniqueGeneratedAlias()
        {
            MockAliasStrategy
                .SetupSequence(strategy => strategy.GenerateAlias(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(TestGeneratedAlias)
                .Returns(TestGeneratedAlias)
                .Returns(TestGeneratedAlias)
                .Returns(TestGeneratedAlias)
                .Returns(TestGeneratedAlias)
                .Returns(nameof(ReturnsFirstUniqueGeneratedAlias));
            TestObject.RegisterNewAlias(TestObjectName);

            string actual = TestObject.RegisterNewAlias(TestObjectName);

            Assert.AreEqual(nameof(ReturnsFirstUniqueGeneratedAlias), actual);
        }
    }
}
