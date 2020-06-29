using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Curds.Persistence.Query.Queries.Tests
{
    using Implementation;
    using Persistence.Abstraction;
    using Persistence.Domain;
    using Query.Abstraction;
    using Query.Domain;
    using Template;

    [TestClass]
    public class EntityUpdateQueryTest : BaseSqlQueryTemplate
    {
        private string TestNewName = nameof(TestNewName);

        private Mock<ISqlQueryTokenFactory> MockTokenFactory = new Mock<ISqlQueryTokenFactory>();
        private Mock<ISqlTable> MockUpdatedTable = new Mock<ISqlTable>();

        private EntityUpdateQuery<ITestDataModel, TestEntity> TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestObject = new EntityUpdateQuery<ITestDataModel, TestEntity>(
                MockQueryContext.Object,
                MockTokenFactory.Object,
                MockPhraseBuilder.Object,
                MockUpdatedTable.Object,
                MockSource.Object);
        }

        [TestMethod]
        public void GenerateCommandBuildsUpdateTablePhrase()
        {
            TestObject.GenerateCommand();

            MockPhraseBuilder.Verify(builder => builder.UpdateTableToken(MockUpdatedTable.Object), Times.Once);
        }

        [TestMethod]
        public void GenerateCommandBuildsSetValuesPhrase()
        {
            TestObject.GenerateCommand();

            MockPhraseBuilder.Verify(builder => builder.SetValuesToken(It.IsAny<IEnumerable<ISqlQueryToken>>()), Times.Once);
        }

        [TestMethod]
        public void GenerateCommandBuildsFromUniversePhrase()
        {
            TestObject.GenerateCommand();

            MockPhraseBuilder.Verify(builder => builder.FromUniverseTokens(MockSource.Object), Times.Once);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(7)]
        [DataRow(10)]
        [DataRow(15)]
        public void GeneratedTokensAreExpected(int sourceTokensGenerated)
        {
            List<ISqlQueryToken> expectedTokens = new List<ISqlQueryToken>();
            expectedTokens.Add(SetupPhraseBuilder(builder => builder.UpdateTableToken(It.IsAny<ISqlTable>())));
            expectedTokens.Add(SetupPhraseBuilder(builder => builder.SetValuesToken(It.IsAny<IEnumerable<ISqlQueryToken>>())));
            expectedTokens.AddRange(SetupPhraseBuilder(builder => builder.FromUniverseTokens(It.IsAny<ISqlUniverse>()), sourceTokensGenerated));

            TestObject.GenerateCommand();

            CollectionAssert.AreEqual(expectedTokens, FormattedTokens);
        }

        private Expression<Func<TestEntity, string>> TestValueSelectionExpression => entity => entity.Name;

        [TestMethod]
        public void SetParsesValueSelection()
        {
            Expression<Func<TestEntity, string>> testExpression = TestValueSelectionExpression;

            TestObject.Set(testExpression, TestNewName);

            MockQueryContext.Verify(context => context.ParseQueryExpression(testExpression), Times.Once);
        }

        [TestMethod]
        public void SetBuildsParameterForNewValue()
        {
            TestObject.Set(TestValueSelectionExpression, TestNewName);

            MockTokenFactory.Verify(factory => factory.Parameter(MockParameterBuilder.Object, "newValue", TestNewName), Times.Once);
        }

        [TestMethod]
        public void SetBuildsAssignmentForParsedValueAndParameter()
        {
            ISqlQueryToken testIdentifierToken = Mock.Of<ISqlQueryToken>();
            MockQueryContext
                .Setup(context => context.ParseQueryExpression(It.IsAny<Expression>()))
                .Returns(testIdentifierToken);
            ISqlQueryToken testParameterToken = Mock.Of<ISqlQueryToken>();
            MockTokenFactory
                .Setup(factory => factory.Parameter(It.IsAny<ISqlQueryParameterBuilder>(), It.IsAny<string>(), It.IsAny<object>()))
                .Returns(testParameterToken);

            TestObject.Set(TestValueSelectionExpression, TestNewName);

            MockTokenFactory.Verify(factory => factory.ArithmeticOperation(ArithmeticOperation.Equals, testIdentifierToken, testParameterToken));
        }

        [TestMethod]
        public void SetAddsAssignmentToCollection()
        {
            ISqlQueryToken testAssignmentToken = Mock.Of<ISqlQueryToken>();
            MockTokenFactory
                .Setup(factory => factory.ArithmeticOperation(It.IsAny<ArithmeticOperation>(), It.IsAny<ISqlQueryToken>(), It.IsAny<ISqlQueryToken>()))
                .Returns(testAssignmentToken);

            TestObject.Set(TestValueSelectionExpression, TestNewName);

            CollectionAssert.AreEqual(new[] { testAssignmentToken }, TestObject.SetTokens);
        }

        [TestMethod]
        public void SetReturnsSameQuery()
        {
            IEntityUpdate<TestEntity> actual = TestObject.Set(TestValueSelectionExpression, TestNewName);

            Assert.AreSame(TestObject, actual);
        }
    }
}
