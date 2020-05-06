using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System;

namespace Curds.Persistence.Query.Tests
{
    using Abstraction;
    using Domain;
    using Implementation;
    using Model.Abstraction;
    using Template;
    using Values.Domain;

    [TestClass]
    public class SqlQueryWriterTest : SqlQueryWriterTemplate
    {
        private List<ValueEntity> TestValueEntities = new List<ValueEntity>();
        private ValueEntity TestValueEntity = new ValueEntity();
        private IntValue TestIntValue = new IntValue { Name = nameof(TestIntValue) };
        private int TestInt = 10;
        private string TestFormattedTokens = nameof(TestFormattedTokens);
        private string TestParameterName = nameof(TestParameterName);
        private List<ISqlColumn> TestColumns = new List<ISqlColumn>();

        private Mock<ISqlQueryFormatter> MockFormatter = new Mock<ISqlQueryFormatter>();
        private Mock<ISqlQueryParameterBuilder> MockParameterBuilder = new Mock<ISqlQueryParameterBuilder>();
        private Mock<ISqlTable> MockTable = new Mock<ISqlTable>();
        private Mock<ISqlColumn> MockColumn = new Mock<ISqlColumn>();
        private Mock<ISqlUniverse> MockUniverse = new Mock<ISqlUniverse>();
        private Mock<ISqlUniverseFilter> MockUniverseFilter = new Mock<ISqlUniverseFilter>();

        private SqlQueryWriter TestObject = null;

        private List<ISqlQueryToken> FormattedTokens = null;

        [TestInitialize]
        public void Init()
        {
            TestValueEntities.Add(TestValueEntity);
            TestIntValue.Int = TestInt;
            TestColumns.Add(MockColumn.Object);

            MockFormatter
                .Setup(formatter => formatter.FormatTokens(It.IsAny<IEnumerable<ISqlQueryToken>>()))
                .Callback<IEnumerable<ISqlQueryToken>>(tokens => FormattedTokens = tokens.ToList())
                .Returns(TestFormattedTokens);

            TestObject = new SqlQueryWriter(
                MockTokenFactory.Object,
                MockFormatter.Object,
                MockParameterBuilder.Object);
        }

        [TestMethod]
        public void FlushFormatsStoredTokens()
        {
            TestObject.Insert(MockTable.Object);

            TestObject.Flush();

            MockFormatter.Verify(formatter => formatter.FormatTokens(It.IsAny<IEnumerable<ISqlQueryToken>>()), Times.Once);
            Assert.AreEqual(1, FormattedTokens.Count);
        }

        [TestMethod]
        public void FlushedCommandTextIsFormattedTokens()
        {
            SqlCommand actual = TestObject.Flush();

            Assert.AreEqual(TestFormattedTokens, actual.CommandText);
        }

        [TestMethod]
        public void FlushClearsTokens()
        {
            TestObject.Insert(MockTable.Object);

            TestObject.Flush();

            Assert.AreEqual(0, TestObject.Tokens.Count);
        }

        [TestMethod]
        public void FlushFlushesParameterBuilder()
        {
            TestObject.Flush();

            MockParameterBuilder.Verify(builder => builder.Flush(), Times.Once);
        }

        [TestMethod]
        public void FlushedCommandsParametersAreFromBuilder()
        {
            SqlParameter testParameter = new SqlParameter(nameof(testParameter), nameof(testParameter));
            MockParameterBuilder
                .Setup(builder => builder.Flush())
                .Returns(new[] { testParameter });

            SqlCommand actual = TestObject.Flush();

            Assert.AreEqual(1, actual.Parameters.Count);
            Assert.AreSame(testParameter, actual.Parameters[0]);
        }

        [TestMethod]
        public void FlushProducesCommandOfCorrectType()
        {
            SqlCommand actual = TestObject.Flush();

            Assert.AreEqual(CommandType.Text, actual.CommandType);
        }

        [TestMethod]
        public void FlushProducesCommandWithNoConnection()
        {
            SqlCommand actual = TestObject.Flush();

            Assert.IsNull(actual.Connection);
        }

        [TestMethod]
        public void FlushProducesCommandWithNoTransaction()
        {
            SqlCommand actual = TestObject.Flush();

            Assert.IsNull(actual.Transaction);
        }

        [TestMethod]
        public void CreateTemporaryIdentityTableAddsPhraseToTokens()
        {
            TestObject.CreateTemporaryIdentityTable(MockTable.Object);

            Assert.AreEqual(1, TestObject.Tokens.Count);
            Assert.AreSame(MockPhraseToken.Object, TestObject.Tokens[0]);
        }

        [TestMethod]
        public void CreateTemporaryIdentityTableBuildsACreateKeywordToken()
        {
            TestObject.CreateTemporaryIdentityTable(MockTable.Object);

            MockTokenFactory.Verify(factory => factory.Keyword(SqlQueryKeyword.CREATE), Times.Once);
        }

        [TestMethod]
        public void CreateTemporaryIdentityTableBuildsATableKeywordToken()
        {
            TestObject.CreateTemporaryIdentityTable(MockTable.Object);

            MockTokenFactory.Verify(factory => factory.Keyword(SqlQueryKeyword.TABLE), Times.Once);
        }

        [TestMethod]
        public void CreateTemporaryIdentityTableBuildsATemporaryIdentityNameToken()
        {
            TestObject.CreateTemporaryIdentityTable(MockTable.Object);

            MockTokenFactory.Verify(factory => factory.TemporaryIdentityName(MockTable.Object), Times.Once);
        }

        [TestMethod]
        public void CreateTemporaryIdentityTableBuildsAColumnListToken()
        {
            MockTable
                .Setup(table => table.Identity)
                .Returns(MockColumn.Object);

            TestObject.CreateTemporaryIdentityTable(MockTable.Object);

            MockTokenFactory.Verify(factory => factory.ColumnList(new[] { MockColumn.Object }, true), Times.Once);
        }

        [TestMethod]
        public void CreateTemporaryIdentityTableAddsExpectedPhrase()
        {
            ISqlQueryToken createKeyword = SetupFactoryForKeyword(SqlQueryKeyword.CREATE);
            ISqlQueryToken tableKeyword = SetupFactoryForKeyword(SqlQueryKeyword.TABLE);
            ISqlQueryToken temporaryIdentityName = SetupFactory(factory => factory.TemporaryIdentityName(It.IsAny<ISqlTable>()));
            ISqlQueryToken columnList = SetupFactory(factory => factory.ColumnList(It.IsAny<IEnumerable<ISqlColumn>>(), It.IsAny<bool>()));

            TestObject.CreateTemporaryIdentityTable(MockTable.Object);

            Assert.AreEqual(1, SuppliedPhraseTokens.Count);
            ISqlQueryToken[] phraseTokens = SuppliedPhraseTokens[0];
            Assert.AreEqual(4, phraseTokens.Length);
            Assert.AreSame(createKeyword, phraseTokens[0]);
            Assert.AreSame(tableKeyword, phraseTokens[1]);
            Assert.AreSame(temporaryIdentityName, phraseTokens[2]);
            Assert.AreSame(columnList, phraseTokens[3]);
        }

        [TestMethod]
        public void OutputIdentitiesToTemporaryTableAddsPhraseToTokens()
        {
            TestObject.OutputIdentitiesToTemporaryTable(MockTable.Object);

            Assert.AreEqual(1, TestObject.Tokens.Count);
            Assert.AreSame(MockPhraseToken.Object, TestObject.Tokens[0]);
        }

        [TestMethod]
        public void OutputIdentitiesToTemporaryTableBuildsAnOutputKeywordToken()
        {
            TestObject.OutputIdentitiesToTemporaryTable(MockTable.Object);

            MockTokenFactory.Verify(factory => factory.Keyword(SqlQueryKeyword.OUTPUT), Times.Once);
        }

        [TestMethod]
        public void OutputIdentitiesToTemporaryTableBuildsAnInsertedIdentityNameToken()
        {
            TestObject.OutputIdentitiesToTemporaryTable(MockTable.Object);

            MockTokenFactory.Verify(factory => factory.InsertedIdentityName(MockTable.Object), Times.Once);
        }

        [TestMethod]
        public void OutputIdentitiesToTemporaryTableBuildsAnIntoKeywordToken()
        {
            TestObject.OutputIdentitiesToTemporaryTable(MockTable.Object);

            MockTokenFactory.Verify(factory => factory.Keyword(SqlQueryKeyword.INTO), Times.Once);
        }

        [TestMethod]
        public void OutputIdentitiesToTemporaryTableBuildsATemporaryIdentityNameToken()
        {
            TestObject.OutputIdentitiesToTemporaryTable(MockTable.Object);

            MockTokenFactory.Verify(factory => factory.TemporaryIdentityName(MockTable.Object), Times.Once);
        }

        [TestMethod]
        public void OutputIdentitiesToTemporaryTableAddsExpectedPhrase()
        {
            ISqlQueryToken outputToken = SetupFactoryForKeyword(SqlQueryKeyword.OUTPUT);
            ISqlQueryToken insertedIdentity = SetupFactory(factory => factory.InsertedIdentityName(It.IsAny<ISqlTable>()));
            ISqlQueryToken intoKeyword = SetupFactoryForKeyword(SqlQueryKeyword.INTO);
            ISqlQueryToken temporaryIdentityName = SetupFactory(factory => factory.TemporaryIdentityName(It.IsAny<ISqlTable>()));

            TestObject.OutputIdentitiesToTemporaryTable(MockTable.Object);

            Assert.AreEqual(1, SuppliedPhraseTokens.Count);
            ISqlQueryToken[] phraseTokens = SuppliedPhraseTokens[0];
            Assert.AreEqual(4, phraseTokens.Length);
            Assert.AreSame(outputToken, phraseTokens[0]);
            Assert.AreSame(insertedIdentity, phraseTokens[1]);
            Assert.AreSame(intoKeyword, phraseTokens[2]);
            Assert.AreSame(temporaryIdentityName, phraseTokens[3]);
        }

        [TestMethod]
        public void SelectTemporaryIdentityTableBuildsTwoPhrases()
        {
            TestObject.SelectTemporaryIdentityTable(MockTable.Object);

            MockTokenFactory.Verify(factory => factory.Phrase(It.IsAny<ISqlQueryToken[]>()), Times.Exactly(2));
        }

        [TestMethod]
        public void SelectTemporaryIdentityTableBuildsASelectKeywordToken()
        {
            TestObject.SelectTemporaryIdentityTable(MockTable.Object);

            MockTokenFactory.Verify(factory => factory.Keyword(SqlQueryKeyword.SELECT), Times.Once);
        }

        [TestMethod]
        public void SelectTemporaryIdentityTableBuildsAColumnListToken()
        {
            MockTable
                .Setup(model => model.Identity)
                .Returns(MockColumn.Object);

            TestObject.SelectTemporaryIdentityTable(MockTable.Object);

            MockTokenFactory.Verify(factory => factory.ColumnList(new[] { MockColumn.Object }, false), Times.Once);
        }

        [TestMethod]
        public void SelectTemporaryIdentityTableBuildsAFromKeywordToken()
        {
            TestObject.SelectTemporaryIdentityTable(MockTable.Object);

            MockTokenFactory.Verify(factory => factory.Keyword(SqlQueryKeyword.FROM), Times.Once);
        }

        [TestMethod]
        public void SelectTemporaryIdentityTableBuildsATemporaryIdentityNameToken()
        {
            TestObject.SelectTemporaryIdentityTable(MockTable.Object);

            MockTokenFactory.Verify(factory => factory.TemporaryIdentityName(MockTable.Object), Times.Once);
        }

        [TestMethod]
        public void SelectTemporaryIdentityTableAddsTwoPhrasesToTokens()
        {
            TestObject.SelectTemporaryIdentityTable(MockTable.Object);

            Assert.AreEqual(2, TestObject.Tokens.Count);
            Assert.AreSame(MockPhraseToken.Object, TestObject.Tokens[0]);
            Assert.AreSame(MockPhraseToken.Object, TestObject.Tokens[1]);
        }

        [TestMethod]
        public void SelectTemporaryIdentityTableAddsExpectedFirstPhrase()
        {
            ISqlQueryToken selectKeyword = SetupFactoryForKeyword(SqlQueryKeyword.SELECT);
            ISqlQueryToken columnList = SetupFactory(factory => factory.ColumnList(It.IsAny<IEnumerable<ISqlColumn>>(), It.IsAny<bool>()));

            TestObject.SelectTemporaryIdentityTable(MockTable.Object);

            Assert.AreEqual(2, SuppliedPhraseTokens[0].Length);
            Assert.AreSame(selectKeyword, SuppliedPhraseTokens[0][0]);
            Assert.AreSame(columnList, SuppliedPhraseTokens[0][1]);
        }

        [TestMethod]
        public void SelectTemporaryIdentityTableAddsExpectedSecondPhrase()
        {
            ISqlQueryToken fromKeyword = SetupFactoryForKeyword(SqlQueryKeyword.FROM);
            ISqlQueryToken temporaryIdentityName = SetupFactory(factory => factory.TemporaryIdentityName(MockTable.Object));

            TestObject.SelectTemporaryIdentityTable(MockTable.Object);

            Assert.AreEqual(2, SuppliedPhraseTokens[1].Length);
            Assert.AreSame(fromKeyword, SuppliedPhraseTokens[1][0]);
            Assert.AreSame(temporaryIdentityName, SuppliedPhraseTokens[1][1]);
        }

        [TestMethod]
        public void DropTemporaryIdentityTableAddsPhraseToTokens()
        {
            TestObject.DropTemporaryIdentityTable(MockTable.Object);

            Assert.AreEqual(1, TestObject.Tokens.Count);
            Assert.AreSame(MockPhraseToken.Object, TestObject.Tokens[0]);
        }

        [TestMethod]
        public void DropTemporaryIdentityTableBuildsADropKeyword()
        {
            TestObject.DropTemporaryIdentityTable(MockTable.Object);

            MockTokenFactory.Verify(factory => factory.Keyword(SqlQueryKeyword.DROP), Times.Once);
        }

        [TestMethod]
        public void DropTemporaryIdentityTableBuildsATableKeyword()
        {
            TestObject.DropTemporaryIdentityTable(MockTable.Object);

            MockTokenFactory.Verify(factory => factory.Keyword(SqlQueryKeyword.TABLE), Times.Once);
        }

        [TestMethod]
        public void DropTemporaryIdentityTableBuildsATemporaryIdentityNameToken()
        {
            TestObject.DropTemporaryIdentityTable(MockTable.Object);

            MockTokenFactory.Verify(factory => factory.TemporaryIdentityName(MockTable.Object), Times.Once);
        }

        [TestMethod]
        public void DropTemprorayIdentityTableAddsExpectedPhrase()
        {
            ISqlQueryToken dropKeyword = SetupFactoryForKeyword(SqlQueryKeyword.DROP);
            ISqlQueryToken tableKeyword = SetupFactoryForKeyword(SqlQueryKeyword.TABLE);
            ISqlQueryToken temporaryIdentityName = SetupFactory(factory => factory.TemporaryIdentityName(It.IsAny<ISqlTable>()));

            TestObject.DropTemporaryIdentityTable(MockTable.Object);

            Assert.AreEqual(1, SuppliedPhraseTokens.Count);
            ISqlQueryToken[] phraseTokens = SuppliedPhraseTokens[0];
            Assert.AreEqual(3, phraseTokens.Length);
            Assert.AreSame(dropKeyword, phraseTokens[0]);
            Assert.AreSame(tableKeyword, phraseTokens[1]);
            Assert.AreSame(temporaryIdentityName, phraseTokens[2]);
        }

        [TestMethod]
        public void InsertAddsPhraseToTokens()
        {
            TestObject.Insert(MockTable.Object);

            Assert.AreEqual(1, TestObject.Tokens.Count);
            Assert.AreSame(MockPhraseToken.Object, TestObject.Tokens[0]);
        }

        [TestMethod]
        public void InsertBuildsInsertKeyword()
        {
            TestObject.Insert(MockTable.Object);

            MockTokenFactory.Verify(factory => factory.Keyword(SqlQueryKeyword.INSERT), Times.Once);
        }

        [TestMethod]
        public void InsertBuildsQualifiedObjectNameForTable()
        {
            TestObject.Insert(MockTable.Object);

            MockTokenFactory.Verify(factory => factory.QualifiedObjectName(MockTable.Object), Times.Once);
        }

        [TestMethod]
        public void InsertBuildsColumnListWithoutDefinitions()
        {
            MockTable
                .Setup(model => model.NonIdentities)
                .Returns(new[] { MockColumn.Object });

            TestObject.Insert(MockTable.Object);

            MockTokenFactory.Verify(factory => factory.ColumnList(new[] { MockColumn.Object }, false));
        }

        [TestMethod]
        public void InsertBuildsExpectedPhrase()
        {
            ISqlQueryToken insertKeyword = SetupFactoryForKeyword(SqlQueryKeyword.INSERT);
            ISqlQueryToken tableName = SetupFactory(factory => factory.QualifiedObjectName(It.IsAny<ISqlTable>()));
            ISqlQueryToken columnList = SetupFactory(factory => factory.ColumnList(It.IsAny<IEnumerable<ISqlColumn>>(), It.IsAny<bool>()));

            TestObject.Insert(MockTable.Object);

            Assert.AreEqual(1, SuppliedPhraseTokens.Count);
            ISqlQueryToken[] phraseTokens = SuppliedPhraseTokens[0];
            Assert.AreEqual(3, phraseTokens.Length);
            Assert.AreSame(insertKeyword, phraseTokens[0]);
            Assert.AreSame(tableName, phraseTokens[1]);
            Assert.AreSame(columnList, phraseTokens[2]);
        }

        [TestMethod]
        public void ValueEntitiesBuildsValuesKeyword()
        {
            TestObject.ValueEntities(TestValueEntities);

            MockTokenFactory.Verify(factory => factory.Keyword(SqlQueryKeyword.VALUES), Times.Once);
        }

        [TestMethod]
        public void ValueEntitiesBuildsValueEntitiesToken()
        {
            TestObject.ValueEntities(TestValueEntities);

            MockTokenFactory.Verify(factory => factory.ValueEntities(TestValueEntities), Times.Once);
        }

        [TestMethod]
        public void ValueEntitiesAddsExpectedTokens()
        {
            ISqlQueryToken valuesKeyword = SetupFactoryForKeyword(SqlQueryKeyword.VALUES);
            ISqlQueryToken valueEntities = SetupFactory(factory => factory.ValueEntities(It.IsAny<IEnumerable<ValueEntity>>()));

            TestObject.ValueEntities(TestValueEntities);

            Assert.AreEqual(2, TestObject.Tokens.Count);
            Assert.AreSame(valuesKeyword, TestObject.Tokens[0]);
            Assert.AreSame(valueEntities, TestObject.Tokens[1]);
        }

        [TestMethod]
        public void SelectAddsPhraseToTokens()
        {
            TestObject.Select(TestColumns);

            Assert.AreEqual(1, TestObject.Tokens.Count);
            Assert.AreSame(MockPhraseToken.Object, TestObject.Tokens[0]);
        }

        [TestMethod]
        public void SelectBuildsSelectKeyword()
        {
            TestObject.Select(TestColumns);

            MockTokenFactory.Verify(factory => factory.Keyword(SqlQueryKeyword.SELECT), Times.Once);
        }

        [TestMethod]
        public void SelectBuildsColumnListToken()
        {
            TestObject.Select(TestColumns);

            MockTokenFactory.Verify(factory => factory.SelectList(TestColumns), Times.Once);
        }

        [TestMethod]
        public void SelectBuildsExpectedPhrase()
        {
            ISqlQueryToken selectKeyword = SetupFactoryForKeyword(SqlQueryKeyword.SELECT);
            ISqlQueryToken selectList = SetupFactory(factory => factory.SelectList(It.IsAny<IEnumerable<ISqlColumn>>()));

            TestObject.Select(TestColumns);

            Assert.AreEqual(1, SuppliedPhraseTokens.Count);
            ISqlQueryToken[] phraseTokens = SuppliedPhraseTokens[0];
            Assert.AreEqual(2, phraseTokens.Length);
            Assert.AreSame(selectKeyword, phraseTokens[0]);
            Assert.AreSame(selectList, phraseTokens[1]);
        }

        private void SetupNTables(int tables)
        {
            List<ISqlTable> testTables = new List<ISqlTable>();
            for (int i = 0; i < tables; i++)
                testTables.Add(MockTable.Object);
            MockUniverse
                .Setup(universe => universe.Tables)
                .Returns(testTables);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(10)]
        [DataRow(15)]
        public void FromAddsPhraseForEachTableToTokens(int tables)
        {
            SetupNTables(tables);

            TestObject.From(MockUniverse.Object);

            Assert.AreEqual(tables, TestObject.Tokens.Count);
            foreach (ISqlQueryToken token in TestObject.Tokens)
                Assert.AreSame(MockPhraseToken.Object, token);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(10)]
        [DataRow(15)]
        public void FromBuildsFromKeywordForEachTable(int tables)
        {
            SetupNTables(tables);

            TestObject.From(MockUniverse.Object);

            MockTokenFactory.Verify(factory => factory.Keyword(SqlQueryKeyword.FROM), Times.Exactly(tables));
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(10)]
        [DataRow(15)]
        public void FromBuildsQualifiedObjectNameTokenForEachTable(int tables)
        {
            SetupNTables(tables);

            TestObject.From(MockUniverse.Object);

            MockTokenFactory.Verify(factory => factory.QualifiedObjectName(MockTable.Object), Times.Exactly(tables));
        }

        [TestMethod]
        public void FromBuildsExpectedTablePhrase()
        {
            SetupNTables(1);
            ISqlQueryToken fromKeyword = SetupFactoryForKeyword(SqlQueryKeyword.FROM);
            ISqlQueryToken tableName = SetupFactory(factory => factory.QualifiedObjectName(It.IsAny<ISqlTable>()));

            TestObject.From(MockUniverse.Object);

            Assert.AreEqual(1, SuppliedPhraseTokens.Count);
            ISqlQueryToken[] phraseTokens = SuppliedPhraseTokens[0];
            Assert.AreEqual(2, phraseTokens.Length);
            Assert.AreSame(fromKeyword, phraseTokens[0]);
            Assert.AreSame(tableName, phraseTokens[1]);
        }

        private void SetupNFilters(int filters)
        {
            List<ISqlUniverseFilter> testFilters = new List<ISqlUniverseFilter>();
            for (int i = 0; i < filters; i++)
                testFilters.Add(MockUniverseFilter.Object);
            MockUniverse
                .Setup(universe => universe.Filters)
                .Returns(testFilters);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(10)]
        [DataRow(15)]
        public void FromAddsPhraseForEachFilterToTokens(int filters)
        {
            SetupNFilters(filters);

            TestObject.From(MockUniverse.Object);

            Assert.AreEqual(filters, TestObject.Tokens.Count);
            foreach (ISqlQueryToken token in TestObject.Tokens)
                Assert.AreSame(MockPhraseToken.Object, token);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(10)]
        [DataRow(15)]
        public void FromBuildsWhereKeywordForEachFilter(int filters)
        {
            SetupNFilters(filters);

            TestObject.From(MockUniverse.Object);

            MockTokenFactory.Verify(factory => factory.Keyword(SqlQueryKeyword.WHERE), Times.Exactly(filters));
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(10)]
        [DataRow(15)]
        public void FromBuildsUniverseFilterForEachFilter(int filters)
        {
            SetupNFilters(filters);

            TestObject.From(MockUniverse.Object);

            MockTokenFactory.Verify(factory => factory.UniverseFilter(MockUniverseFilter.Object), Times.Exactly(filters));
        }

        [TestMethod]
        public void FromBuildsExpectedFilterPhrase()
        {
            SetupNFilters(1);
            ISqlQueryToken whereKeyword = SetupFactoryForKeyword(SqlQueryKeyword.WHERE);
            ISqlQueryToken universeFilter = SetupFactory(factory => factory.UniverseFilter(It.IsAny<ISqlUniverseFilter>()));

            TestObject.From(MockUniverse.Object);

            Assert.AreEqual(1, SuppliedPhraseTokens.Count);
            ISqlQueryToken[] phraseTokens = SuppliedPhraseTokens[0];
            Assert.AreEqual(2, phraseTokens.Length);
            Assert.AreSame(whereKeyword, phraseTokens[0]);
            Assert.AreSame(universeFilter, phraseTokens[1]);
        }

        [TestMethod]
        public void FromBuildsTablesBeforeFilters()
        {
            int callOrder = 0;
            MockUniverse
                .Setup(universe => universe.Tables)
                .Callback(() => Assert.AreEqual(callOrder++, 0));
            MockUniverse
                .Setup(universe => universe.Filters)
                .Callback(() => Assert.AreEqual(callOrder++, 1));

            TestObject.From(MockUniverse.Object);

            MockUniverse.Verify(universe => universe.Tables, Times.Once);
            MockUniverse.Verify(universe => universe.Filters, Times.Once);
        }
    }
}
