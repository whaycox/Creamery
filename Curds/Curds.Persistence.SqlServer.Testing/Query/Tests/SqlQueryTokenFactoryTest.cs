using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using Whey;

namespace Curds.Persistence.Query.Tests
{
    using Abstraction;
    using Domain;
    using Implementation;
    using Tokens.Implementation;

    [TestClass]
    public class SqlQueryTokenFactoryTest
    {
        private List<ISqlColumn> TestColumns = new List<ISqlColumn>();
        private string TestSchema = nameof(TestSchema);
        private string TestTableName = nameof(TestTableName);
        private string TestColumnName = nameof(TestColumnName);

        private Mock<ISqlTable> MockTable = new Mock<ISqlTable>();
        private Mock<ISqlColumn> MockColumn = new Mock<ISqlColumn>();
        private Mock<ISqlJoinClause> MockJoinClause = new Mock<ISqlJoinClause>();
        private Mock<ISqlQueryParameterBuilder> MockParameterBuilder = new Mock<ISqlQueryParameterBuilder>();

        private SqlQueryTokenFactory TestObject = new SqlQueryTokenFactory();

        [TestInitialize]
        public void Init()
        {
            TestColumns.Add(MockColumn.Object);

            MockTable
                .Setup(table => table.Schema)
                .Returns(TestSchema);
            MockTable
                .Setup(table => table.Name)
                .Returns(TestTableName);
            MockColumn
                .Setup(column => column.Table)
                .Returns(MockTable.Object);
            MockColumn
                .Setup(column => column.Name)
                .Returns(TestColumnName);
        }

        [TestMethod]
        public void PhraseBuildsExpectedToken()
        {
            ISqlQueryToken tokenOne = Mock.Of<ISqlQueryToken>();
            ISqlQueryToken tokenTwo = Mock.Of<ISqlQueryToken>();
            ISqlQueryToken tokenThree = Mock.Of<ISqlQueryToken>();
            ISqlQueryToken[] testTokens = new[] { tokenOne, tokenTwo, tokenThree };

            ISqlQueryToken actual = TestObject.Phrase(testTokens);

            Assert.IsInstanceOfType(actual, typeof(PhraseSqlQueryToken));
            PhraseSqlQueryToken phrase = (PhraseSqlQueryToken)actual;
            CollectionAssert.AreEqual(testTokens, phrase.Tokens);
        }

        [TestMethod]
        public void KeywordBuildsExpectedToken()
        {
            ISqlQueryToken actual = TestObject.Keyword(SqlQueryKeyword.INSERT);

            KeywordSqlQueryToken keyword = actual.VerifyIsActually<KeywordSqlQueryToken>();
            Assert.AreEqual(SqlQueryKeyword.INSERT, keyword.Keyword);
        }

        [TestMethod]
        public void ObjectNameBuildsExpectedToken()
        {
            ISqlQueryToken actual = TestObject.ObjectName(TestTableName);

            ObjectNameSqlQueryToken objectName = actual.VerifyIsActually<ObjectNameSqlQueryToken>();
            Assert.AreEqual(TestTableName, objectName.Name);
        }

        [TestMethod]
        public void QualifiedObjectBuildsExpectedToken()
        {
            ISqlQueryToken actual = TestObject.QualifiedObject(
                TestTableName,
                TestColumnName);

            QualifiedObjectSqlQueryToken qualifiedObject = actual.VerifyIsActually<QualifiedObjectSqlQueryToken>();
            CollectionAssert.AreEqual(new[] { TestTableName, TestColumnName }, qualifiedObject.Names);
        }

        [TestMethod]
        public void ParameterBuildsExpectedToken()
        {
            object testObject = new object();
            MockParameterBuilder
                .Setup(builder => builder.RegisterNewParamater(It.IsAny<string>(), It.IsAny<object>()))
                .Returns(nameof(testObject));

            ISqlQueryToken actual = TestObject.Parameter(MockParameterBuilder.Object, nameof(testObject), testObject);

            ParameterSqlQueryToken actualToken = actual.VerifyIsActually<ParameterSqlQueryToken>();
            Assert.AreEqual(nameof(testObject), actualToken.Name);
        }

        [TestMethod]
        public void ParameterRegistersWithBuilder()
        {
            object testObject = new object();

            ISqlQueryToken actual = TestObject.Parameter(MockParameterBuilder.Object, nameof(testObject), testObject);

            MockParameterBuilder.Verify(builder => builder.RegisterNewParamater(nameof(testObject), testObject), Times.Once);
        }

        [TestMethod]
        public void ParameterWithNullValueHasNullType()
        {
            ISqlQueryToken actual = TestObject.Parameter(MockParameterBuilder.Object, nameof(ParameterWithNullValueHasNullType), null);

            ParameterSqlQueryToken actualToken = actual.VerifyIsActually<ParameterSqlQueryToken>();
            Assert.IsNull(actualToken.Type);
        }

        [TestMethod]
        public void ValueEntityBuildsExpectedToken()
        {
            ValueEntity testValueEntity = new ValueEntity();

            ISqlQueryToken actual = TestObject.ValueEntity(MockParameterBuilder.Object, testValueEntity);

            Assert.IsInstanceOfType(actual, typeof(ValueEntitySqlQueryToken));
        }

        [DataTestMethod]
        [DataRow(false)]
        [DataRow(true)]
        public void GroupedListBuildsExpectedToken(bool testSeparators)
        {
            ISqlQueryToken tokenOne = Mock.Of<ISqlQueryToken>();
            ISqlQueryToken tokenTwo = Mock.Of<ISqlQueryToken>();
            ISqlQueryToken tokenThree = Mock.Of<ISqlQueryToken>();
            ISqlQueryToken[] testTokens = new[] { tokenOne, tokenTwo, tokenThree };

            ISqlQueryToken actual = TestObject.GroupedList(testTokens, testSeparators);

            TokenListSqlQueryToken tokenListToken = actual.VerifyIsActually<TokenListSqlQueryToken>();
            Assert.IsTrue(tokenListToken.IncludeGrouping);
            Assert.AreEqual(testSeparators, tokenListToken.IncludeSeparators);
        }

        [DataTestMethod]
        [DataRow(false)]
        [DataRow(true)]
        public void UngroupedListBuildsExpectedToken(bool testSeparators)
        {
            ISqlQueryToken tokenOne = Mock.Of<ISqlQueryToken>();
            ISqlQueryToken tokenTwo = Mock.Of<ISqlQueryToken>();
            ISqlQueryToken tokenThree = Mock.Of<ISqlQueryToken>();
            ISqlQueryToken[] testTokens = new[] { tokenOne, tokenTwo, tokenThree };

            ISqlQueryToken actual = TestObject.UngroupedList(testTokens, testSeparators);

            TokenListSqlQueryToken tokenListToken = actual.VerifyIsActually<TokenListSqlQueryToken>();
            Assert.IsFalse(tokenListToken.IncludeGrouping);
            Assert.AreEqual(testSeparators, tokenListToken.IncludeSeparators);
        }

        [TestMethod]
        public void TableDefinitionBuildsExpectedToken()
        {
            ISqlQueryToken actual = TestObject.TableDefinition(MockTable.Object);

            Assert.IsInstanceOfType(actual, typeof(TableDefinitionSqlQueryToken));
        }

        [DataTestMethod]
        [DataRow(false, false)]
        [DataRow(false, true)]
        [DataRow(true, false)]
        [DataRow(true, true)]
        public void TableNameBuildsExpectedToken(bool testAlias, bool testSqlName)
        {
            ISqlQueryToken actual = TestObject.TableName(MockTable.Object, testAlias, testSqlName);

            TableNameSqlQueryToken tableName = actual.VerifyIsActually<TableNameSqlQueryToken>();
            Assert.AreEqual(testAlias, tableName.UseAlias);
            Assert.AreEqual(testSqlName, tableName.UseSqlName);
        }

        [TestMethod]
        public void ColumnDefinitionBuildsExpectedToken()
        {
            ISqlQueryToken actual = TestObject.ColumnDefinition(MockColumn.Object);

            Assert.IsInstanceOfType(actual, typeof(ColumnDefinitionSqlQueryToken));
        }

        [DataTestMethod]
        [DataRow(false)]
        [DataRow(true)]
        public void ColumnNameBuildsExpectedToken(bool testAlias)
        {
            ISqlQueryToken actual = TestObject.ColumnName(MockColumn.Object, testAlias);

            ColumnNameSqlQueryToken columnName = actual.VerifyIsActually<ColumnNameSqlQueryToken>();
            Assert.AreEqual(testAlias, columnName.UseAlias);
        }

        [TestMethod]
        public void DbTypeBuildsExpectedToken()
        {
            ISqlQueryToken actual = TestObject.DbType(MockColumn.Object);

            Assert.IsInstanceOfType(actual, typeof(SqlDbTypeSqlQueryToken));
        }

        [TestMethod]
        public void InsertedIdentityNameBuildsExpectedToken()
        {
            MockTable
                .Setup(model => model.Identity)
                .Returns(MockColumn.Object);

            ISqlQueryToken actual = TestObject.InsertedIdentityName(MockTable.Object);

            InsertedIdentityColumnSqlQueryToken inserted = actual.VerifyIsActually<InsertedIdentityColumnSqlQueryToken>();
            Assert.AreSame(MockColumn.Object, inserted.Identity);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(7)]
        [DataRow(10)]
        public void JoinClauseBuildsExpectedToken(int joinClauseTokens)
        {
            List<ISqlQueryToken> testJoinClauseTokens = MockJoinClause.SetupMock(joinClause => joinClause.Tokens, joinClauseTokens);

            ISqlQueryToken actual = TestObject.JoinClause(MockJoinClause.Object);

            TokenListSqlQueryToken tokenListToken = actual.VerifyIsActually<TokenListSqlQueryToken>();
            Assert.IsFalse(tokenListToken.IncludeSeparators);
            CollectionAssert.AreEqual(testJoinClauseTokens, tokenListToken.Tokens);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(7)]
        [DataRow(14)]
        public void BooleanCombinationBuildsExpectedToken(int elementsToAdd)
        {
            BooleanCombination testCombination = BooleanCombination.Or;
            List<ISqlQueryToken> testElements = new List<ISqlQueryToken>();
            for (int i = 0; i < elementsToAdd; i++)
                testElements.Add(Mock.Of<ISqlQueryToken>());

            ISqlQueryToken actual = TestObject.BooleanCombination(testCombination, testElements);

            BooleanCombinationSqlQueryToken actualToken = actual.VerifyIsActually<BooleanCombinationSqlQueryToken>();
            KeywordSqlQueryToken operationToken = actualToken.Operation.VerifyIsActually<KeywordSqlQueryToken>();
            Assert.AreEqual(SqlQueryKeyword.OR, operationToken.Keyword);
            CollectionAssert.AreEqual(testElements, actualToken.Elements);
        }

        [TestMethod]
        public void BooleanComparisonBuildsExpectedToken()
        {
            BooleanComparison testComparison = BooleanComparison.GreaterThanOrEquals;
            ISqlQueryToken testLeftToken = Mock.Of<ISqlQueryToken>();
            ISqlQueryToken testRightToken = Mock.Of<ISqlQueryToken>();

            ISqlQueryToken actual = TestObject.BooleanComparison(testComparison, testLeftToken, testRightToken);

            BooleanComparisonSqlQueryToken actualToken = actual.VerifyIsActually<BooleanComparisonSqlQueryToken>();
            Assert.AreEqual(testComparison, actualToken.Operation);
            Assert.AreSame(testLeftToken, actualToken.Left);
            Assert.AreSame(testRightToken, actualToken.Right);
        }

        [TestMethod]
        public void ArithmeticOperationBuildsExpectedToken()
        {
            ArithmeticOperation testOperation = ArithmeticOperation.Modulo;
            ISqlQueryToken testLeftToken = Mock.Of<ISqlQueryToken>();
            ISqlQueryToken testRightToken = Mock.Of<ISqlQueryToken>();

            ISqlQueryToken actual = TestObject.ArithmeticOperation(testOperation, testLeftToken, testRightToken);

            ArithmeticOperationSqlQueryToken actualToken = actual.VerifyIsActually<ArithmeticOperationSqlQueryToken>();
            Assert.AreEqual(testOperation, actualToken.Operation);
            Assert.AreSame(testLeftToken, actualToken.Left);
            Assert.AreSame(testRightToken, actualToken.Right);
        }
    }
}
