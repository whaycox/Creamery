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
    using Values.Domain;

    [TestClass]
    public class SqlQueryTokenFactoryTest
    {
        private List<ISqlColumn> TestColumns = new List<ISqlColumn>();
        private string TestSchema = nameof(TestSchema);
        private string TestTableName = nameof(TestTableName);
        private string TestColumnName = nameof(TestColumnName);

        private Mock<ISqlTable> MockTable = new Mock<ISqlTable>();
        private Mock<ISqlColumn> MockColumn = new Mock<ISqlColumn>();
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
        public void KeywordBuildsExpectedToken()
        {
            ISqlQueryToken actual = TestObject.Keyword(SqlQueryKeyword.INSERT);

            Assert.IsInstanceOfType(actual, typeof(KeywordSqlQueryToken));
            KeywordSqlQueryToken keyword = (KeywordSqlQueryToken)actual;
            Assert.AreEqual(SqlQueryKeyword.INSERT, keyword.Keyword);
        }

        [DataTestMethod]
        [DataRow(false)]
        [DataRow(true)]
        public void ColumnListBuildsExpectedToken(bool testIncludeDefinitions)
        {
            throw new System.NotImplementedException();
            //ISqlQueryToken actual = TestObject.ColumnList(TestColumns, testIncludeDefinitions);

            //Assert.IsInstanceOfType(actual, typeof(ColumnListSqlQueryToken));
            //ColumnListSqlQueryToken columnList = (ColumnListSqlQueryToken)actual;
            //CollectionAssert.AreEqual(TestColumns, columnList.Columns);
            //Assert.AreEqual(testIncludeDefinitions, columnList.IncludeDefinition);
            //Assert.IsTrue(columnList.IncludeGrouping);
        }

        [TestMethod]
        public void SelectListBuildsExpectedToken()
        {
            throw new System.NotImplementedException();
            //ISqlQueryToken actual = TestObject.SelectList(TestColumns);

            //Assert.IsInstanceOfType(actual, typeof(ColumnListSqlQueryToken));
            //ColumnListSqlQueryToken columnList = (ColumnListSqlQueryToken)actual;
            //CollectionAssert.AreEqual(TestColumns, columnList.Columns);
            //Assert.IsFalse(columnList.IncludeDefinition);
            //Assert.IsFalse(columnList.IncludeGrouping);
        }

        [TestMethod]
        public void TemporaryIdentityNameBuildsExpectedToken()
        {
            throw new System.NotImplementedException();
            //ISqlQueryToken actual = TestObject.TemporaryIdentityName(MockTable.Object);

            //Assert.IsInstanceOfType(actual, typeof(TemporaryIdentityTableNameSqlQueryToken));
            //TemporaryIdentityTableNameSqlQueryToken table = (TemporaryIdentityTableNameSqlQueryToken)actual;
            //Assert.AreEqual(TestTableName, table.BaseTableName);
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
        public void QualifiedObjectNameWithTableBuildsExpectedToken()
        {
            throw new System.NotImplementedException();
            //ISqlQueryToken actual = TestObject.TableName(MockTable.Object);

            //Assert.IsInstanceOfType(actual, typeof(QualifiedObjectSqlQueryToken));
            //QualifiedObjectSqlQueryToken qualifiedName = (QualifiedObjectSqlQueryToken)actual;
            //Assert.AreEqual(2, qualifiedName.Names.Count);
            //Assert.AreEqual(TestSchema, qualifiedName.Names[0].Name);
            //Assert.AreEqual(TestTableName, qualifiedName.Names[1].Name);
        }

        [TestMethod]
        public void QualifiedObjectNameWithColumnBuildsExpectedToken()
        {
            throw new System.NotImplementedException();
            //ISqlQueryToken actual = TestObject.ColumnName(MockColumn.Object);

            //Assert.IsInstanceOfType(actual, typeof(QualifiedObjectSqlQueryToken));
            //QualifiedObjectSqlQueryToken qualifiedName = (QualifiedObjectSqlQueryToken)actual;
            //Assert.AreEqual(3, qualifiedName.Names.Count);
            //Assert.AreEqual(TestSchema, qualifiedName.Names[0].Name);
            //Assert.AreEqual(TestTableName, qualifiedName.Names[1].Name);
            //Assert.AreEqual(TestColumnName, qualifiedName.Names[2].Name);
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
        public void InsertedIdentityNameBuildsExpectedToken()
        {
            MockTable
                .Setup(model => model.Identity)
                .Returns(MockColumn.Object);

            ISqlQueryToken actual = TestObject.InsertedIdentityName(MockTable.Object);

            Assert.IsInstanceOfType(actual, typeof(InsertedIdentityColumnSqlQueryToken));
            InsertedIdentityColumnSqlQueryToken inserted = (InsertedIdentityColumnSqlQueryToken)actual;
            Assert.AreEqual(2, inserted.Names.Count);
            Assert.AreEqual(nameof(SqlQueryKeyword.inserted), inserted.Names[0].Name);
            Assert.AreEqual(TestColumnName, inserted.Names[1].Name);
        }

        [TestMethod]
        public void ValueEntitiesBuildsExpectedToken()
        {
            int testInt = 20;
            Value testValue = new IntValue { Name = nameof(testValue), Int = testInt };
            ValueEntity testEntity = new ValueEntity();
            testEntity.Values.Add(testValue);
            var testEntities = new ValueEntity[] { testEntity };
            MockParameterBuilder
                .Setup(builder => builder.RegisterNewParamater(nameof(testValue), testInt))
                .Returns(nameof(ValueEntitiesBuildsExpectedToken));

            ISqlQueryToken actual = TestObject.ValueEntities(MockParameterBuilder.Object, testEntities);

            Assert.IsInstanceOfType(actual, typeof(ValueEntitiesSqlQueryToken));
            ValueEntitiesSqlQueryToken valueEntities = (ValueEntitiesSqlQueryToken)actual;
            Assert.AreEqual(1, valueEntities.Entities.Count);
            ValueEntitySqlQueryToken valueEntity = valueEntities.Entities[0];
            Assert.AreEqual(1, valueEntity.Values.Count);
            ParameterSqlQueryToken value = valueEntity.Values[0];
            Assert.AreEqual(nameof(ValueEntitiesBuildsExpectedToken), value.Name);
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
            BooleanComparison testComparison = BooleanComparison.GreaterThanOrEqual;
            ISqlQueryToken testLeftToken = Mock.Of<ISqlQueryToken>();
            ISqlQueryToken testRightToken = Mock.Of<ISqlQueryToken>();

            ISqlQueryToken actual = TestObject.BooleanComparison(testComparison, testLeftToken, testRightToken);

            BooleanComparisonSqlQueryToken actualToken = actual.VerifyIsActually<BooleanComparisonSqlQueryToken>();
            ConstantSqlQueryToken operationToken = actualToken.Operation.VerifyIsActually<ConstantSqlQueryToken>();
            Assert.AreEqual(" >= ", operationToken.Literal);
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
            ConstantSqlQueryToken operationToken = actualToken.Operation.VerifyIsActually<ConstantSqlQueryToken>();
            Assert.AreEqual(" % ", operationToken.Literal);
            Assert.AreSame(testLeftToken, actualToken.Left);
            Assert.AreSame(testRightToken, actualToken.Right);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(10)]
        [DataRow(15)]
        public void SetValuesBuildsExpectedToken(int valuesToSet)
        {
            List<ISqlQueryToken> testValueTokens = new List<ISqlQueryToken>();
            for (int i = 0; i < valuesToSet; i++)
                testValueTokens.Add(Mock.Of<ISqlQueryToken>());

            ISqlQueryToken actual = TestObject.SetValues(testValueTokens);

            SetValuesSqlQueryToken actualToken = actual.VerifyIsActually<SetValuesSqlQueryToken>();
            CollectionAssert.AreEqual(testValueTokens, actualToken.SetValueTokens);
        }
    }
}
