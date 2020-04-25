using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Curds.Persistence.Query.Tests
{
    using Abstraction;
    using Domain;
    using Implementation;
    using Model.Domain;
    using Persistence.Domain;
    using Values.Domain;
    using Template;

    [TestClass]
    public class SqlQueryWriterTest : SqlQueryWriterTemplate
    {
        private List<ValueEntity> TestValueEntities = new List<ValueEntity>();
        private ValueEntity TestValueEntity = new ValueEntity<TestEntity>();
        private IntValue TestIntValue = new IntValue { Name = nameof(TestIntValue) };
        private int TestInt = 10;
        private string TestFormattedTokens = nameof(TestFormattedTokens);
        private string TestParameterName = nameof(TestParameterName);

        private Mock<ISqlQueryFormatter> MockFormatter = new Mock<ISqlQueryFormatter>();
        private Mock<ISqlQueryParameterBuilder> MockParameterBuilder = new Mock<ISqlQueryParameterBuilder>();

        private SqlQueryWriter TestObject = null;

        private List<ISqlQueryToken> FormattedTokens = null;

        [TestInitialize]
        public void Init()
        {
            TestValueEntities.Add(TestValueEntity);
            TestIntValue.Int = TestInt;

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
            TestObject.From(TestTable);

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
            TestObject.From(TestTable);

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
            TestObject.CreateTemporaryIdentityTable(TestTable);

            Assert.AreEqual(1, TestObject.Tokens.Count);
            Assert.AreSame(MockPhraseToken.Object, TestObject.Tokens[0]);
        }

        [TestMethod]
        public void CreateTemporaryIdentityTableBuildsACreateKeywordToken()
        {
            TestObject.CreateTemporaryIdentityTable(TestTable);

            MockTokenFactory.Verify(factory => factory.Keyword(SqlQueryKeyword.CREATE), Times.Once);
        }

        [TestMethod]
        public void CreateTemporaryIdentityTableBuildsATableKeywordToken()
        {
            TestObject.CreateTemporaryIdentityTable(TestTable);

            MockTokenFactory.Verify(factory => factory.Keyword(SqlQueryKeyword.TABLE), Times.Once);
        }

        [TestMethod]
        public void CreateTemporaryIdentityTableBuildsATemporaryIdentityNameToken()
        {
            TestObject.CreateTemporaryIdentityTable(TestTable);

            MockTokenFactory.Verify(factory => factory.TemporaryIdentityName(TestTable), Times.Once);
        }

        [TestMethod]
        public void CreateTemporaryIdentityTableBuildsAColumnListToken()
        {
            TestColumnOne.IsIdentity = true;
            TestTable.Columns.Add(TestColumnOne);

            TestObject.CreateTemporaryIdentityTable(TestTable);

            MockTokenFactory.Verify(factory => factory.ColumnList(TestTable.IdentityColumns, true), Times.Once);
        }

        [TestMethod]
        public void CreateTemporaryIdentityTableAddsExpectedPhrase()
        {
            ISqlQueryToken createKeyword = SetupFactoryForKeyword(SqlQueryKeyword.CREATE);
            ISqlQueryToken tableKeyword = SetupFactoryForKeyword(SqlQueryKeyword.TABLE);
            ISqlQueryToken temporaryIdentityName = SetupFactory(factory => factory.TemporaryIdentityName(It.IsAny<Table>()));
            ISqlQueryToken columnList = SetupFactory(factory => factory.ColumnList(It.IsAny<IEnumerable<Column>>(), It.IsAny<bool>()));

            TestObject.CreateTemporaryIdentityTable(TestTable);

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
            TestObject.OutputIdentitiesToTemporaryTable(TestTable);

            Assert.AreEqual(1, TestObject.Tokens.Count);
            Assert.AreSame(MockPhraseToken.Object, TestObject.Tokens[0]);
        }

        [TestMethod]
        public void OutputIdentitiesToTemporaryTableBuildsAnOutputKeywordToken()
        {
            TestObject.OutputIdentitiesToTemporaryTable(TestTable);

            MockTokenFactory.Verify(factory => factory.Keyword(SqlQueryKeyword.OUTPUT), Times.Once);
        }

        [TestMethod]
        public void OutputIdentitiesToTemporaryTableBuildsAnInsertedIdentityNameToken()
        {
            TestObject.OutputIdentitiesToTemporaryTable(TestTable);

            MockTokenFactory.Verify(factory => factory.InsertedIdentityName(TestTable), Times.Once);
        }

        [TestMethod]
        public void OutputIdentitiesToTemporaryTableBuildsAnIntoKeywordToken()
        {
            TestObject.OutputIdentitiesToTemporaryTable(TestTable);

            MockTokenFactory.Verify(factory => factory.Keyword(SqlQueryKeyword.INTO), Times.Once);
        }

        [TestMethod]
        public void OutputIdentitiesToTemporaryTableBuildsATemporaryIdentityNameToken()
        {
            TestObject.OutputIdentitiesToTemporaryTable(TestTable);

            MockTokenFactory.Verify(factory => factory.TemporaryIdentityName(TestTable), Times.Once);
        }

        [TestMethod]
        public void OutputIdentitiesToTemporaryTableAddsExpectedPhrase()
        {
            ISqlQueryToken outputToken = SetupFactoryForKeyword(SqlQueryKeyword.OUTPUT);
            ISqlQueryToken insertedIdentity = SetupFactory(factory => factory.InsertedIdentityName(It.IsAny<Table>()));
            ISqlQueryToken intoKeyword = SetupFactoryForKeyword(SqlQueryKeyword.INTO);
            ISqlQueryToken temporaryIdentityName = SetupFactory(factory => factory.TemporaryIdentityName(It.IsAny<Table>()));

            TestObject.OutputIdentitiesToTemporaryTable(TestTable);

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
            TestObject.SelectTemporaryIdentityTable(TestTable);

            MockTokenFactory.Verify(factory => factory.Phrase(It.IsAny<ISqlQueryToken[]>()), Times.Exactly(2));
        }

        [TestMethod]
        public void SelectTemporaryIdentityTableBuildsASelectKeywordToken()
        {
            TestObject.SelectTemporaryIdentityTable(TestTable);

            MockTokenFactory.Verify(factory => factory.Keyword(SqlQueryKeyword.SELECT), Times.Once);
        }

        [TestMethod]
        public void SelectTemporaryIdentityTableBuildsAColumnListToken()
        {
            TestColumnOne.IsIdentity = true;
            TestTable.Columns.Add(TestColumnOne);

            TestObject.SelectTemporaryIdentityTable(TestTable);

            MockTokenFactory.Verify(factory => factory.ColumnList(TestTable.IdentityColumns, false), Times.Once);
        }

        [TestMethod]
        public void SelectTemporaryIdentityTableBuildsAFromKeywordToken()
        {
            TestObject.SelectTemporaryIdentityTable(TestTable);

            MockTokenFactory.Verify(factory => factory.Keyword(SqlQueryKeyword.FROM), Times.Once);
        }

        [TestMethod]
        public void SelectTemporaryIdentityTableBuildsATemporaryIdentityNameToken()
        {
            TestObject.SelectTemporaryIdentityTable(TestTable);

            MockTokenFactory.Verify(factory => factory.TemporaryIdentityName(TestTable), Times.Once);
        }

        [TestMethod]
        public void SelectTemporaryIdentityTableAddsTwoPhrasesToTokens()
        {
            TestObject.SelectTemporaryIdentityTable(TestTable);

            Assert.AreEqual(2, TestObject.Tokens.Count);
            Assert.AreSame(MockPhraseToken.Object, TestObject.Tokens[0]);
            Assert.AreSame(MockPhraseToken.Object, TestObject.Tokens[1]);
        }

        [TestMethod]
        public void SelectTemporaryIdentityTableAddsExpectedFirstPhrase()
        {
            ISqlQueryToken selectKeyword = SetupFactoryForKeyword(SqlQueryKeyword.SELECT);
            ISqlQueryToken columnList = SetupFactory(factory => factory.ColumnList(It.IsAny<IEnumerable<Column>>(), It.IsAny<bool>()));

            TestObject.SelectTemporaryIdentityTable(TestTable);

            Assert.AreEqual(2, SuppliedPhraseTokens[0].Length);
            Assert.AreSame(selectKeyword, SuppliedPhraseTokens[0][0]);
            Assert.AreSame(columnList, SuppliedPhraseTokens[0][1]);
        }

        [TestMethod]
        public void SelectTemporaryIdentityTableAddsExpectedSecondPhrase()
        {
            ISqlQueryToken fromKeyword = SetupFactoryForKeyword(SqlQueryKeyword.FROM);
            ISqlQueryToken temporaryIdentityName = SetupFactory(factory => factory.TemporaryIdentityName(TestTable));

            TestObject.SelectTemporaryIdentityTable(TestTable);

            Assert.AreEqual(2, SuppliedPhraseTokens[1].Length);
            Assert.AreSame(fromKeyword, SuppliedPhraseTokens[1][0]);
            Assert.AreSame(temporaryIdentityName, SuppliedPhraseTokens[1][1]);
        }

        [TestMethod]
        public void DropTemporaryIdentityTableAddsPhraseToTokens()
        {
            TestObject.DropTemporaryIdentityTable(TestTable);

            Assert.AreEqual(1, TestObject.Tokens.Count);
            Assert.AreSame(MockPhraseToken.Object, TestObject.Tokens[0]);
        }

        [TestMethod]
        public void DropTemporaryIdentityTableBuildsADropKeyword()
        {
            TestObject.DropTemporaryIdentityTable(TestTable);

            MockTokenFactory.Verify(factory => factory.Keyword(SqlQueryKeyword.DROP), Times.Once);
        }

        [TestMethod]
        public void DropTemporaryIdentityTableBuildsATableKeyword()
        {
            TestObject.DropTemporaryIdentityTable(TestTable);

            MockTokenFactory.Verify(factory => factory.Keyword(SqlQueryKeyword.TABLE), Times.Once);
        }

        [TestMethod]
        public void DropTemporaryIdentityTableBuildsATemporaryIdentityNameToken()
        {
            TestObject.DropTemporaryIdentityTable(TestTable);

            MockTokenFactory.Verify(factory => factory.TemporaryIdentityName(TestTable), Times.Once);
        }

        [TestMethod]
        public void DropTemprorayIdentityTableAddsExpectedPhrase()
        {
            ISqlQueryToken dropKeyword = SetupFactoryForKeyword(SqlQueryKeyword.DROP);
            ISqlQueryToken tableKeyword = SetupFactoryForKeyword(SqlQueryKeyword.TABLE);
            ISqlQueryToken temporaryIdentityName = SetupFactory(factory => factory.TemporaryIdentityName(It.IsAny<Table>()));

            TestObject.DropTemporaryIdentityTable(TestTable);

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
            TestObject.Insert(TestTable);

            Assert.AreEqual(1, TestObject.Tokens.Count);
            Assert.AreSame(MockPhraseToken.Object, TestObject.Tokens[0]);
        }

        [TestMethod]
        public void InsertBuildsInsertKeyword()
        {
            TestObject.Insert(TestTable);

            MockTokenFactory.Verify(factory => factory.Keyword(SqlQueryKeyword.INSERT), Times.Once);
        }

        [TestMethod]
        public void InsertBuildsQualifiedObjectNameForTable()
        {
            TestObject.Insert(TestTable);

            MockTokenFactory.Verify(factory => factory.QualifiedObjectName(TestTable), Times.Once);
        }

        [TestMethod]
        public void InsertBuildsColumnListWithoutDefinitions()
        {
            TestTable.Columns.Add(TestColumnOne);

            TestObject.Insert(TestTable);

            MockTokenFactory.Verify(factory => factory.ColumnList(TestTable.NotIdentityColumns, false));
        }

        [TestMethod]
        public void InsertBuildsExpectedPhrase()
        {
            ISqlQueryToken insertKeyword = SetupFactoryForKeyword(SqlQueryKeyword.INSERT);
            ISqlQueryToken tableName = SetupFactory(factory => factory.QualifiedObjectName(It.IsAny<Table>()));
            ISqlQueryToken columnList = SetupFactory(factory => factory.ColumnList(It.IsAny<IEnumerable<Column>>(), It.IsAny<bool>()));

            TestObject.Insert(TestTable);

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
        public void SelectBuildsSelectKeyword()
        {
            TestObject.Select(TestTable.Columns);

            MockTokenFactory.Verify(factory => factory.Keyword(SqlQueryKeyword.SELECT), Times.Once);
        }

        [TestMethod]
        public void SelectBuildsColumnListToken()
        {
            TestTable.Columns.Add(TestColumnOne);

            TestObject.Select(TestTable.Columns);

            MockTokenFactory.Verify(factory => factory.ColumnList(TestTable.Columns, false), Times.Once);
        }

        [TestMethod]
        public void SelectAddsExpectedTokens()
        {
            TestTable.Columns.Add(TestColumnOne);
            ISqlQueryToken selectKeyword = SetupFactoryForKeyword(SqlQueryKeyword.SELECT);
            ISqlQueryToken columnList = SetupFactory(factory => factory.ColumnList(It.IsAny<IEnumerable<Column>>(), It.IsAny<bool>()));

            TestObject.Select(TestTable.Columns);

            Assert.AreEqual(2, TestObject.Tokens.Count);
            Assert.AreSame(selectKeyword, TestObject.Tokens[0]);
            Assert.AreSame(columnList, TestObject.Tokens[1]);
        }

        [TestMethod]
        public void FromAddsPhraseToTokens()
        {
            TestObject.From(TestTable);

            Assert.AreEqual(1, TestObject.Tokens.Count);
            Assert.AreSame(MockPhraseToken.Object, TestObject.Tokens[0]);
        }

        [TestMethod]
        public void FromBuildsFromKeyword()
        {
            TestObject.From(TestTable);

            MockTokenFactory.Verify(factory => factory.Keyword(SqlQueryKeyword.FROM), Times.Once);
        }

        [TestMethod]
        public void FromBuildsQualifiedObjectNameToken()
        {
            TestObject.From(TestTable);

            MockTokenFactory.Verify(factory => factory.QualifiedObjectName(TestTable), Times.Once);
        }

        [TestMethod]
        public void FromBuildsExpectedPhrase()
        {
            ISqlQueryToken fromKeyword = SetupFactoryForKeyword(SqlQueryKeyword.FROM);
            ISqlQueryToken tableName = SetupFactory(factory => factory.QualifiedObjectName(It.IsAny<Table>()));

            TestObject.From(TestTable);

            Assert.AreEqual(1, SuppliedPhraseTokens.Count);
            ISqlQueryToken[] phraseTokens = SuppliedPhraseTokens[0];
            Assert.AreEqual(2, phraseTokens.Length);
            Assert.AreSame(fromKeyword, phraseTokens[0]);
            Assert.AreSame(tableName, phraseTokens[1]);
        }
    }
}
