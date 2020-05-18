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

        private SqlQueryTokenFactory TestObject = null;

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
            ISqlQueryToken actual = TestObject.ColumnList(TestColumns, testIncludeDefinitions);

            Assert.IsInstanceOfType(actual, typeof(ColumnListSqlQueryToken));
            ColumnListSqlQueryToken columnList = (ColumnListSqlQueryToken)actual;
            CollectionAssert.AreEqual(TestColumns, columnList.Columns);
            Assert.AreEqual(testIncludeDefinitions, columnList.IncludeDefinition);
            Assert.IsTrue(columnList.IncludeGrouping);
        }

        [TestMethod]
        public void SelectListBuildsExpectedToken()
        {
            ISqlQueryToken actual = TestObject.SelectList(TestColumns);

            Assert.IsInstanceOfType(actual, typeof(ColumnListSqlQueryToken));
            ColumnListSqlQueryToken columnList = (ColumnListSqlQueryToken)actual;
            CollectionAssert.AreEqual(TestColumns, columnList.Columns);
            Assert.IsFalse(columnList.IncludeDefinition);
            Assert.IsFalse(columnList.IncludeGrouping);
        }

        [TestMethod]
        public void TemporaryIdentityNameBuildsExpectedToken()
        {
            ISqlQueryToken actual = TestObject.TemporaryIdentityName(MockTable.Object);

            Assert.IsInstanceOfType(actual, typeof(TemporaryIdentityTableNameSqlQueryToken));
            TemporaryIdentityTableNameSqlQueryToken table = (TemporaryIdentityTableNameSqlQueryToken)actual;
            Assert.AreEqual(TestTableName, table.BaseTableName);
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
            ISqlQueryToken actual = TestObject.QualifiedObjectName(MockTable.Object);

            Assert.IsInstanceOfType(actual, typeof(QualifiedObjectSqlQueryToken));
            QualifiedObjectSqlQueryToken qualifiedName = (QualifiedObjectSqlQueryToken)actual;
            Assert.AreEqual(2, qualifiedName.Names.Count);
            Assert.AreEqual(TestSchema, qualifiedName.Names[0].Name);
            Assert.AreEqual(TestTableName, qualifiedName.Names[1].Name);
        }

        [TestMethod]
        public void QualifiedObjectNameWithColumnBuildsExpectedToken()
        {
            ISqlQueryToken actual = TestObject.QualifiedObjectName(MockColumn.Object);

            Assert.IsInstanceOfType(actual, typeof(QualifiedObjectSqlQueryToken));
            QualifiedObjectSqlQueryToken qualifiedName = (QualifiedObjectSqlQueryToken)actual;
            Assert.AreEqual(3, qualifiedName.Names.Count);
            Assert.AreEqual(TestSchema, qualifiedName.Names[0].Name);
            Assert.AreEqual(TestTableName, qualifiedName.Names[1].Name);
            Assert.AreEqual(TestColumnName, qualifiedName.Names[2].Name);
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
    }
}
