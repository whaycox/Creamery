using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using Whey;

namespace Curds.Persistence.Query.Tests
{
    using Abstraction;
    using Domain;
    using Implementation;

    [TestClass]
    public class SqlQueryPhraseBuilderTest
    {
        private ValueEntity TestValueEntity = new ValueEntity();

        private Mock<ISqlQueryTokenFactory> MockTokenFactory = new Mock<ISqlQueryTokenFactory>();
        private Mock<ISqlTable> MockTable = new Mock<ISqlTable>();
        private Mock<ISqlColumn> MockColumn = new Mock<ISqlColumn>();
        private Mock<ISqlJoinClause> MockJoinClause = new Mock<ISqlJoinClause>();
        private Mock<ISqlQueryParameterBuilder> MockParameterBuilder = new Mock<ISqlQueryParameterBuilder>();
        private Mock<ISqlQueryToken> MockSetValueToken = new Mock<ISqlQueryToken>();

        private SqlQueryPhraseBuilder TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestObject = new SqlQueryPhraseBuilder(MockTokenFactory.Object);
        }

        [TestMethod]
        public void CreateTableBuildsExpectedPhrase()
        {
            ISqlQueryToken createToken = MockTokenFactory.SetupMock(factory => factory.Keyword(SqlQueryKeyword.CREATE));
            ISqlQueryToken tableToken = MockTokenFactory.SetupMock(factory => factory.Keyword(SqlQueryKeyword.TABLE));
            ISqlQueryToken tableDefinitionToken = MockTokenFactory.SetupMock(factory => factory.TableDefinition(MockTable.Object));
            ISqlQueryToken expectedPhraseToken = MockTokenFactory.SetupMock(factory => factory.Phrase(createToken, tableToken, tableDefinitionToken));

            ISqlQueryToken actual = TestObject.CreateTableToken(MockTable.Object);

            Assert.AreSame(expectedPhraseToken, actual);
        }

        [TestMethod]
        public void DropTableBuildsExpectedPhrase()
        {
            ISqlQueryToken dropToken = MockTokenFactory.SetupMock(factory => factory.Keyword(SqlQueryKeyword.DROP));
            ISqlQueryToken tableToken = MockTokenFactory.SetupMock(factory => factory.Keyword(SqlQueryKeyword.TABLE));
            ISqlQueryToken tableNameToken = MockTokenFactory.SetupMock(factory => factory.TableName(MockTable.Object, false, true));
            ISqlQueryToken expectedPhraseToken = MockTokenFactory.SetupMock(factory => factory.Phrase(dropToken, tableToken, tableNameToken));

            ISqlQueryToken actual = TestObject.DropTableToken(MockTable.Object);

            Assert.AreSame(expectedPhraseToken, actual);
        }

        [TestMethod]
        public void OutputToTemporaryIdentityTokenBuildsExpectedPhrase()
        {
            ISqlTable insertedIdentityTable = MockTable.SetupMock(table => table.InsertedIdentityTable);
            ISqlQueryToken outputToken = MockTokenFactory.SetupMock(factory => factory.Keyword(SqlQueryKeyword.OUTPUT));
            ISqlQueryToken insertedIdentityToken = MockTokenFactory.SetupMock(factory => factory.InsertedIdentityName(MockTable.Object));
            ISqlQueryToken intoToken = MockTokenFactory.SetupMock(factory => factory.Keyword(SqlQueryKeyword.INTO));
            ISqlQueryToken tableNameToken = MockTokenFactory.SetupMock(factory => factory.TableName(insertedIdentityTable, false, true));
            ISqlQueryToken expectedPhraseToken = MockTokenFactory.SetupMock(factory => factory.Phrase(outputToken, insertedIdentityToken, intoToken, tableNameToken));

            ISqlQueryToken actual = TestObject.OutputToTemporaryIdentityToken(MockTable.Object);

            Assert.AreSame(expectedPhraseToken, actual);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(10)]
        [DataRow(13)]
        [DataRow(17)]
        public void InsertToTableTokenBuildsExpectedPhrase(int nonIdentityColumns)
        {
            List<ISqlColumn> testColumns = new List<ISqlColumn>();
            for (int i = 0; i < nonIdentityColumns; i++)
                testColumns.Add(MockColumn.Object);
            MockTable
                .Setup(table => table.NonIdentities)
                .Returns(testColumns);
            ISqlQueryToken columnNameToken = MockTokenFactory.SetupMock(factory => factory.ColumnName(MockColumn.Object, false));
            List<ISqlQueryToken> nonIdentityTokens = new List<ISqlQueryToken>();
            for (int i = 0; i < nonIdentityColumns; i++)
                nonIdentityTokens.Add(columnNameToken);
            ISqlQueryToken insertToken = MockTokenFactory.SetupMock(factory => factory.Keyword(SqlQueryKeyword.INSERT));
            ISqlQueryToken tableNameToken = MockTokenFactory.SetupMock(factory => factory.TableName(MockTable.Object, false, true));
            ISqlQueryToken groupedListToken = MockTokenFactory.SetupMock(factory => factory.GroupedList(nonIdentityTokens, true));
            ISqlQueryToken expectedPhraseToken = MockTokenFactory.SetupMock(factory => factory.Phrase(insertToken, tableNameToken, groupedListToken));

            ISqlQueryToken actual = TestObject.InsertToTableToken(MockTable.Object);

            Assert.AreSame(expectedPhraseToken, actual);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(10)]
        [DataRow(13)]
        [DataRow(17)]
        public void ValueEntitiesTokenBuildsExpectedPhrase(int valueEntitiesToAdd)
        {
            List<ValueEntity> testValueEntities = new List<ValueEntity>();
            for (int i = 0; i < valueEntitiesToAdd; i++)
                testValueEntities.Add(TestValueEntity);
            ISqlQueryToken valueEntityToken = MockTokenFactory.SetupMock(factory => factory.ValueEntity(MockParameterBuilder.Object, TestValueEntity));
            List<ISqlQueryToken> valueEntityTokens = new List<ISqlQueryToken>();
            for (int i = 0; i < valueEntitiesToAdd; i++)
                valueEntityTokens.Add(valueEntityToken);
            ISqlQueryToken valuesToken = MockTokenFactory.SetupMock(factory => factory.Keyword(SqlQueryKeyword.VALUES));
            ISqlQueryToken valueEntitiesToken = MockTokenFactory.SetupMock(factory => factory.UngroupedList(valueEntityTokens, true));
            ISqlQueryToken expectedPhraseToken = MockTokenFactory.SetupMock(factory => factory.Phrase(valuesToken, valueEntitiesToken));

            ISqlQueryToken actual = TestObject.ValueEntitiesToken(MockParameterBuilder.Object, testValueEntities);

            Assert.AreSame(expectedPhraseToken, actual);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(10)]
        [DataRow(13)]
        [DataRow(17)]
        public void SelectColumnsTokenBuildsExpectedPhrase(int columnsToAdd)
        {
            List<ISqlColumn> testColumns = new List<ISqlColumn>();
            for (int i = 0; i < columnsToAdd; i++)
                testColumns.Add(MockColumn.Object);
            ISqlQueryToken columnNameToken = MockTokenFactory.SetupMock(factory => factory.ColumnName(MockColumn.Object, true));
            List<ISqlQueryToken> columnNameTokens = new List<ISqlQueryToken>();
            for (int i = 0; i < columnsToAdd; i++)
                columnNameTokens.Add(columnNameToken);
            ISqlQueryToken selectToken = MockTokenFactory.SetupMock(factory => factory.Keyword(SqlQueryKeyword.SELECT));
            ISqlQueryToken columnNamesToken = MockTokenFactory.SetupMock(factory => factory.UngroupedList(columnNameTokens, true));
            ISqlQueryToken expectedPhraseToken = MockTokenFactory.SetupMock(factory => factory.Phrase(selectToken, columnNamesToken));

            ISqlQueryToken actual = TestObject.SelectColumnsToken(testColumns);

            Assert.AreSame(expectedPhraseToken, actual);
        }

        [TestMethod]
        public void FromTableTokenBuildsExpectedPhrase()
        {
            ISqlQueryToken fromToken = MockTokenFactory.SetupMock(factory => factory.Keyword(SqlQueryKeyword.FROM));
            ISqlQueryToken tableNameToken = MockTokenFactory.SetupMock(factory => factory.TableName(MockTable.Object, true, true));
            ISqlQueryToken expectedPhraseToken = MockTokenFactory.SetupMock(factory => factory.Phrase(fromToken, tableNameToken));

            ISqlQueryToken actual = TestObject.FromTableToken(MockTable.Object);

            Assert.AreSame(expectedPhraseToken, actual);
        }

        [TestMethod]
        public void JoinTableTokenBuildsExpectedPhrase()
        {
            MockJoinClause
                .Setup(joinClause => joinClause.JoinedTable)
                .Returns(MockTable.Object);
            ISqlQueryToken joinToken = MockTokenFactory.SetupMock(factory => factory.Keyword(SqlQueryKeyword.JOIN));
            ISqlQueryToken tableNameToken = MockTokenFactory.SetupMock(factory => factory.TableName(MockTable.Object, true, true));
            ISqlQueryToken joinClauseToken = MockTokenFactory.SetupMock(factory => factory.JoinClause(MockJoinClause.Object));
            ISqlQueryToken expectedPhraseToken = MockTokenFactory.SetupMock(factory => factory.Phrase(joinToken, tableNameToken, joinClauseToken));

            ISqlQueryToken actual = TestObject.JoinTableToken(MockJoinClause.Object);

            Assert.AreSame(expectedPhraseToken, actual);
        }

        [TestMethod]
        public void DeleteTableTokenBuildsExpectedPhrase()
        {
            ISqlQueryToken deleteToken = MockTokenFactory.SetupMock(factory => factory.Keyword(SqlQueryKeyword.DELETE));
            ISqlQueryToken tableNameToken = MockTokenFactory.SetupMock(factory => factory.TableName(MockTable.Object, true, false));
            ISqlQueryToken expectedPhraseToken = MockTokenFactory.SetupMock(factory => factory.Phrase(deleteToken, tableNameToken));

            ISqlQueryToken actual = TestObject.DeleteTableToken(MockTable.Object);

            Assert.AreSame(expectedPhraseToken, actual);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(10)]
        [DataRow(13)]
        [DataRow(17)]
        public void UpdateTableTokenBuildsExpectedPhrase(int setValuesToAdd)
        {
            List<ISqlQueryToken> setValueTokens = new List<ISqlQueryToken>();
            for (int i = 0; i < setValuesToAdd; i++)
                setValueTokens.Add(MockSetValueToken.Object);
            ISqlQueryToken updateToken = MockTokenFactory.SetupMock(factory => factory.Keyword(SqlQueryKeyword.UPDATE));
            ISqlQueryToken tableNameToken = MockTokenFactory.SetupMock(factory => factory.TableName(MockTable.Object, true, false));
            ISqlQueryToken setToken = MockTokenFactory.SetupMock(factory => factory.Keyword(SqlQueryKeyword.SET));
            ISqlQueryToken setValuesToken = MockTokenFactory.SetupMock(factory => factory.UngroupedList(setValueTokens, true));
            ISqlQueryToken expectedPhraseToken = MockTokenFactory.SetupMock(factory => factory.Phrase(
                updateToken,
                tableNameToken,
                setToken,
                setValuesToken));

            ISqlQueryToken actual = TestObject.UpdateTableToken(MockTable.Object, setValueTokens);

            Assert.AreSame(expectedPhraseToken, actual);
        }
    }
}
