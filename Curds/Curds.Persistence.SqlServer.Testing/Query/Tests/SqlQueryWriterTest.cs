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
        private List<IValueModel> TestValueModels = new List<IValueModel>();

        private Mock<ISqlQueryFormatter> MockFormatter = new Mock<ISqlQueryFormatter>();
        private Mock<ISqlQueryParameterBuilder> MockParameterBuilder = new Mock<ISqlQueryParameterBuilder>();
        private Mock<IEntityModel> MockEntityModel = new Mock<IEntityModel>();
        private Mock<IValueModel> MockValueModel = new Mock<IValueModel>();

        private SqlQueryWriter TestObject = null;

        private List<ISqlQueryToken> FormattedTokens = null;

        [TestInitialize]
        public void Init()
        {
            TestValueEntities.Add(TestValueEntity);
            TestIntValue.Int = TestInt;
            TestValueModels.Add(MockValueModel.Object);

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
            throw new NotImplementedException();
            //TestObject.From(MockEntityModel.Object);

            //TestObject.Flush();

            //MockFormatter.Verify(formatter => formatter.FormatTokens(It.IsAny<IEnumerable<ISqlQueryToken>>()), Times.Once);
            //Assert.AreEqual(1, FormattedTokens.Count);
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
            throw new NotImplementedException();
            //TestObject.From(MockEntityModel.Object);

            //TestObject.Flush();

            //Assert.AreEqual(0, TestObject.Tokens.Count);
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
            throw new NotImplementedException();
            //TestObject.CreateTemporaryIdentityTable(MockEntityModel.Object);

            //Assert.AreEqual(1, TestObject.Tokens.Count);
            //Assert.AreSame(MockPhraseToken.Object, TestObject.Tokens[0]);
        }

        [TestMethod]
        public void CreateTemporaryIdentityTableBuildsACreateKeywordToken()
        {
            throw new NotImplementedException();
            //TestObject.CreateTemporaryIdentityTable(MockEntityModel.Object);

            //MockTokenFactory.Verify(factory => factory.Keyword(SqlQueryKeyword.CREATE), Times.Once);
        }

        [TestMethod]
        public void CreateTemporaryIdentityTableBuildsATableKeywordToken()
        {
            throw new NotImplementedException();
            //TestObject.CreateTemporaryIdentityTable(MockEntityModel.Object);

            //MockTokenFactory.Verify(factory => factory.Keyword(SqlQueryKeyword.TABLE), Times.Once);
        }

        [TestMethod]
        public void CreateTemporaryIdentityTableBuildsATemporaryIdentityNameToken()
        {
            throw new NotImplementedException();
            //TestObject.CreateTemporaryIdentityTable(MockEntityModel.Object);

            //MockTokenFactory.Verify(factory => factory.TemporaryIdentityName(MockEntityModel.Object), Times.Once);
        }

        [TestMethod]
        public void CreateTemporaryIdentityTableBuildsAColumnListToken()
        {
            throw new NotImplementedException();
            //MockEntityModel
            //    .Setup(model => model.Identity)
            //    .Returns(MockValueModel.Object);

            //TestObject.CreateTemporaryIdentityTable(MockEntityModel.Object);

            //MockTokenFactory.Verify(factory => factory.ColumnList(new[] { MockValueModel.Object }, true), Times.Once);
        }

        [TestMethod]
        public void CreateTemporaryIdentityTableAddsExpectedPhrase()
        {
            throw new NotImplementedException();
            //ISqlQueryToken createKeyword = SetupFactoryForKeyword(SqlQueryKeyword.CREATE);
            //ISqlQueryToken tableKeyword = SetupFactoryForKeyword(SqlQueryKeyword.TABLE);
            //ISqlQueryToken temporaryIdentityName = SetupFactory(factory => factory.TemporaryIdentityName(It.IsAny<IEntityModel>()));
            //ISqlQueryToken columnList = SetupFactory(factory => factory.ColumnList(It.IsAny<IEnumerable<IValueModel>>(), It.IsAny<bool>()));

            //TestObject.CreateTemporaryIdentityTable(MockEntityModel.Object);

            //Assert.AreEqual(1, SuppliedPhraseTokens.Count);
            //ISqlQueryToken[] phraseTokens = SuppliedPhraseTokens[0];
            //Assert.AreEqual(4, phraseTokens.Length);
            //Assert.AreSame(createKeyword, phraseTokens[0]);
            //Assert.AreSame(tableKeyword, phraseTokens[1]);
            //Assert.AreSame(temporaryIdentityName, phraseTokens[2]);
            //Assert.AreSame(columnList, phraseTokens[3]);
        }

        [TestMethod]
        public void OutputIdentitiesToTemporaryTableAddsPhraseToTokens()
        {
            throw new NotImplementedException();
            //TestObject.OutputIdentitiesToTemporaryTable(MockEntityModel.Object);

            //Assert.AreEqual(1, TestObject.Tokens.Count);
            //Assert.AreSame(MockPhraseToken.Object, TestObject.Tokens[0]);
        }

        [TestMethod]
        public void OutputIdentitiesToTemporaryTableBuildsAnOutputKeywordToken()
        {
            throw new NotImplementedException();
            //TestObject.OutputIdentitiesToTemporaryTable(MockEntityModel.Object);

            //MockTokenFactory.Verify(factory => factory.Keyword(SqlQueryKeyword.OUTPUT), Times.Once);
        }

        [TestMethod]
        public void OutputIdentitiesToTemporaryTableBuildsAnInsertedIdentityNameToken()
        {
            throw new NotImplementedException();
            //TestObject.OutputIdentitiesToTemporaryTable(MockEntityModel.Object);

            //MockTokenFactory.Verify(factory => factory.InsertedIdentityName(MockEntityModel.Object), Times.Once);
        }

        [TestMethod]
        public void OutputIdentitiesToTemporaryTableBuildsAnIntoKeywordToken()
        {
            throw new NotImplementedException();
            //TestObject.OutputIdentitiesToTemporaryTable(MockEntityModel.Object);

            //MockTokenFactory.Verify(factory => factory.Keyword(SqlQueryKeyword.INTO), Times.Once);
        }

        [TestMethod]
        public void OutputIdentitiesToTemporaryTableBuildsATemporaryIdentityNameToken()
        {
            throw new NotImplementedException();
            //TestObject.OutputIdentitiesToTemporaryTable(MockEntityModel.Object);

            //MockTokenFactory.Verify(factory => factory.TemporaryIdentityName(MockEntityModel.Object), Times.Once);
        }

        [TestMethod]
        public void OutputIdentitiesToTemporaryTableAddsExpectedPhrase()
        {
            throw new NotImplementedException();
            //ISqlQueryToken outputToken = SetupFactoryForKeyword(SqlQueryKeyword.OUTPUT);
            //ISqlQueryToken insertedIdentity = SetupFactory(factory => factory.InsertedIdentityName(It.IsAny<IEntityModel>()));
            //ISqlQueryToken intoKeyword = SetupFactoryForKeyword(SqlQueryKeyword.INTO);
            //ISqlQueryToken temporaryIdentityName = SetupFactory(factory => factory.TemporaryIdentityName(It.IsAny<IEntityModel>()));

            //TestObject.OutputIdentitiesToTemporaryTable(MockEntityModel.Object);

            //Assert.AreEqual(1, SuppliedPhraseTokens.Count);
            //ISqlQueryToken[] phraseTokens = SuppliedPhraseTokens[0];
            //Assert.AreEqual(4, phraseTokens.Length);
            //Assert.AreSame(outputToken, phraseTokens[0]);
            //Assert.AreSame(insertedIdentity, phraseTokens[1]);
            //Assert.AreSame(intoKeyword, phraseTokens[2]);
            //Assert.AreSame(temporaryIdentityName, phraseTokens[3]);
        }

        [TestMethod]
        public void SelectTemporaryIdentityTableBuildsTwoPhrases()
        {
            throw new NotImplementedException();
            //TestObject.SelectTemporaryIdentityTable(MockEntityModel.Object);

            //MockTokenFactory.Verify(factory => factory.Phrase(It.IsAny<ISqlQueryToken[]>()), Times.Exactly(2));
        }

        [TestMethod]
        public void SelectTemporaryIdentityTableBuildsASelectKeywordToken()
        {
            throw new NotImplementedException();
            //TestObject.SelectTemporaryIdentityTable(MockEntityModel.Object);

            //MockTokenFactory.Verify(factory => factory.Keyword(SqlQueryKeyword.SELECT), Times.Once);
        }

        [TestMethod]
        public void SelectTemporaryIdentityTableBuildsAColumnListToken()
        {
            throw new NotImplementedException();
            //MockEntityModel
            //    .Setup(model => model.Identity)
            //    .Returns(MockValueModel.Object);

            //TestObject.SelectTemporaryIdentityTable(MockEntityModel.Object);

            //MockTokenFactory.Verify(factory => factory.ColumnList(new[] { MockValueModel.Object }, false), Times.Once);
        }

        [TestMethod]
        public void SelectTemporaryIdentityTableBuildsAFromKeywordToken()
        {
            throw new NotImplementedException();
            //TestObject.SelectTemporaryIdentityTable(MockEntityModel.Object);

            //MockTokenFactory.Verify(factory => factory.Keyword(SqlQueryKeyword.FROM), Times.Once);
        }

        [TestMethod]
        public void SelectTemporaryIdentityTableBuildsATemporaryIdentityNameToken()
        {
            throw new NotImplementedException();
            //TestObject.SelectTemporaryIdentityTable(MockEntityModel.Object);

            //MockTokenFactory.Verify(factory => factory.TemporaryIdentityName(MockEntityModel.Object), Times.Once);
        }

        [TestMethod]
        public void SelectTemporaryIdentityTableAddsTwoPhrasesToTokens()
        {
            throw new NotImplementedException();
            //TestObject.SelectTemporaryIdentityTable(MockEntityModel.Object);

            //Assert.AreEqual(2, TestObject.Tokens.Count);
            //Assert.AreSame(MockPhraseToken.Object, TestObject.Tokens[0]);
            //Assert.AreSame(MockPhraseToken.Object, TestObject.Tokens[1]);
        }

        [TestMethod]
        public void SelectTemporaryIdentityTableAddsExpectedFirstPhrase()
        {
            throw new NotImplementedException();
            //ISqlQueryToken selectKeyword = SetupFactoryForKeyword(SqlQueryKeyword.SELECT);
            //ISqlQueryToken columnList = SetupFactory(factory => factory.ColumnList(It.IsAny<IEnumerable<IValueModel>>(), It.IsAny<bool>()));

            //TestObject.SelectTemporaryIdentityTable(MockEntityModel.Object);

            //Assert.AreEqual(2, SuppliedPhraseTokens[0].Length);
            //Assert.AreSame(selectKeyword, SuppliedPhraseTokens[0][0]);
            //Assert.AreSame(columnList, SuppliedPhraseTokens[0][1]);
        }

        [TestMethod]
        public void SelectTemporaryIdentityTableAddsExpectedSecondPhrase()
        {
            throw new NotImplementedException();
            //ISqlQueryToken fromKeyword = SetupFactoryForKeyword(SqlQueryKeyword.FROM);
            //ISqlQueryToken temporaryIdentityName = SetupFactory(factory => factory.TemporaryIdentityName(MockEntityModel.Object));

            //TestObject.SelectTemporaryIdentityTable(MockEntityModel.Object);

            //Assert.AreEqual(2, SuppliedPhraseTokens[1].Length);
            //Assert.AreSame(fromKeyword, SuppliedPhraseTokens[1][0]);
            //Assert.AreSame(temporaryIdentityName, SuppliedPhraseTokens[1][1]);
        }

        [TestMethod]
        public void DropTemporaryIdentityTableAddsPhraseToTokens()
        {
            throw new NotImplementedException();
            //TestObject.DropTemporaryIdentityTable(MockEntityModel.Object);

            //Assert.AreEqual(1, TestObject.Tokens.Count);
            //Assert.AreSame(MockPhraseToken.Object, TestObject.Tokens[0]);
        }

        [TestMethod]
        public void DropTemporaryIdentityTableBuildsADropKeyword()
        {
            throw new NotImplementedException();
            //TestObject.DropTemporaryIdentityTable(MockEntityModel.Object);

            //MockTokenFactory.Verify(factory => factory.Keyword(SqlQueryKeyword.DROP), Times.Once);
        }

        [TestMethod]
        public void DropTemporaryIdentityTableBuildsATableKeyword()
        {
            throw new NotImplementedException();
            //TestObject.DropTemporaryIdentityTable(MockEntityModel.Object);

            //MockTokenFactory.Verify(factory => factory.Keyword(SqlQueryKeyword.TABLE), Times.Once);
        }

        [TestMethod]
        public void DropTemporaryIdentityTableBuildsATemporaryIdentityNameToken()
        {
            throw new NotImplementedException();
            //TestObject.DropTemporaryIdentityTable(MockEntityModel.Object);

            //MockTokenFactory.Verify(factory => factory.TemporaryIdentityName(MockEntityModel.Object), Times.Once);
        }

        [TestMethod]
        public void DropTemprorayIdentityTableAddsExpectedPhrase()
        {
            throw new NotImplementedException();
            //ISqlQueryToken dropKeyword = SetupFactoryForKeyword(SqlQueryKeyword.DROP);
            //ISqlQueryToken tableKeyword = SetupFactoryForKeyword(SqlQueryKeyword.TABLE);
            //ISqlQueryToken temporaryIdentityName = SetupFactory(factory => factory.TemporaryIdentityName(It.IsAny<IEntityModel>()));

            //TestObject.DropTemporaryIdentityTable(MockEntityModel.Object);

            //Assert.AreEqual(1, SuppliedPhraseTokens.Count);
            //ISqlQueryToken[] phraseTokens = SuppliedPhraseTokens[0];
            //Assert.AreEqual(3, phraseTokens.Length);
            //Assert.AreSame(dropKeyword, phraseTokens[0]);
            //Assert.AreSame(tableKeyword, phraseTokens[1]);
            //Assert.AreSame(temporaryIdentityName, phraseTokens[2]);
        }

        [TestMethod]
        public void InsertAddsPhraseToTokens()
        {
            throw new NotImplementedException();
            //TestObject.Insert(MockEntityModel.Object);

            //Assert.AreEqual(1, TestObject.Tokens.Count);
            //Assert.AreSame(MockPhraseToken.Object, TestObject.Tokens[0]);
        }

        [TestMethod]
        public void InsertBuildsInsertKeyword()
        {
            throw new NotImplementedException();
            //TestObject.Insert(MockEntityModel.Object);

            //MockTokenFactory.Verify(factory => factory.Keyword(SqlQueryKeyword.INSERT), Times.Once);
        }

        [TestMethod]
        public void InsertBuildsQualifiedObjectNameForTable()
        {
            throw new NotImplementedException();
            //TestObject.Insert(MockEntityModel.Object);

            //MockTokenFactory.Verify(factory => factory.QualifiedObjectName(MockEntityModel.Object), Times.Once);
        }

        [TestMethod]
        public void InsertBuildsColumnListWithoutDefinitions()
        {
            throw new NotImplementedException();
            //MockEntityModel
            //    .Setup(model => model.NonIdentities)
            //    .Returns(new[] { MockValueModel.Object });

            //TestObject.Insert(MockEntityModel.Object);

            //MockTokenFactory.Verify(factory => factory.ColumnList(new[] { MockValueModel.Object }, false));
        }

        [TestMethod]
        public void InsertBuildsExpectedPhrase()
        {
            throw new NotImplementedException();
            //ISqlQueryToken insertKeyword = SetupFactoryForKeyword(SqlQueryKeyword.INSERT);
            //ISqlQueryToken tableName = SetupFactory(factory => factory.QualifiedObjectName(It.IsAny<IEntityModel>()));
            //ISqlQueryToken columnList = SetupFactory(factory => factory.ColumnList(It.IsAny<IEnumerable<IValueModel>>(), It.IsAny<bool>()));

            //TestObject.Insert(MockEntityModel.Object);

            //Assert.AreEqual(1, SuppliedPhraseTokens.Count);
            //ISqlQueryToken[] phraseTokens = SuppliedPhraseTokens[0];
            //Assert.AreEqual(3, phraseTokens.Length);
            //Assert.AreSame(insertKeyword, phraseTokens[0]);
            //Assert.AreSame(tableName, phraseTokens[1]);
            //Assert.AreSame(columnList, phraseTokens[2]);
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
            throw new NotImplementedException();
            //TestObject.Select(TestValueModels);

            //Assert.AreEqual(1, TestObject.Tokens.Count);
            //Assert.AreSame(MockPhraseToken.Object, TestObject.Tokens[0]);
        }

        [TestMethod]
        public void SelectBuildsSelectKeyword()
        {
            throw new NotImplementedException();
            //TestObject.Select(TestValueModels);

            //MockTokenFactory.Verify(factory => factory.Keyword(SqlQueryKeyword.SELECT), Times.Once);
        }

        [TestMethod]
        public void SelectBuildsColumnListToken()
        {
            throw new NotImplementedException();
            //TestObject.Select(TestValueModels);

            //MockTokenFactory.Verify(factory => factory.SelectList(TestValueModels), Times.Once);
        }

        [TestMethod]
        public void SelectBuildsExpectedPhrase()
        {
            throw new NotImplementedException();
            //ISqlQueryToken selectKeyword = SetupFactoryForKeyword(SqlQueryKeyword.SELECT);
            //ISqlQueryToken selectList = SetupFactory(factory => factory.SelectList(It.IsAny<IEnumerable<IValueModel>>()));

            //TestObject.Select(TestValueModels);

            //Assert.AreEqual(1, SuppliedPhraseTokens.Count);
            //ISqlQueryToken[] phraseTokens = SuppliedPhraseTokens[0];
            //Assert.AreEqual(2, phraseTokens.Length);
            //Assert.AreSame(selectKeyword, phraseTokens[0]);
            //Assert.AreSame(selectList, phraseTokens[1]);
        }

        [TestMethod]
        public void FromAddsPhraseToTokens()
        {
            throw new NotImplementedException();
            //TestObject.From(MockEntityModel.Object);

            //Assert.AreEqual(1, TestObject.Tokens.Count);
            //Assert.AreSame(MockPhraseToken.Object, TestObject.Tokens[0]);
        }

        [TestMethod]
        public void FromBuildsFromKeyword()
        {
            throw new NotImplementedException();
            //TestObject.From(MockEntityModel.Object);

            //MockTokenFactory.Verify(factory => factory.Keyword(SqlQueryKeyword.FROM), Times.Once);
        }

        [TestMethod]
        public void FromBuildsQualifiedObjectNameToken()
        {
            throw new NotImplementedException();
            //TestObject.From(MockEntityModel.Object);

            //MockTokenFactory.Verify(factory => factory.QualifiedObjectName(MockEntityModel.Object), Times.Once);
        }

        [TestMethod]
        public void FromBuildsExpectedPhrase()
        {
            throw new NotImplementedException();
            //ISqlQueryToken fromKeyword = SetupFactoryForKeyword(SqlQueryKeyword.FROM);
            //ISqlQueryToken tableName = SetupFactory(factory => factory.QualifiedObjectName(It.IsAny<IEntityModel>()));

            //TestObject.From(MockEntityModel.Object);

            //Assert.AreEqual(1, SuppliedPhraseTokens.Count);
            //ISqlQueryToken[] phraseTokens = SuppliedPhraseTokens[0];
            //Assert.AreEqual(2, phraseTokens.Length);
            //Assert.AreSame(fromKeyword, phraseTokens[0]);
            //Assert.AreSame(tableName, phraseTokens[1]);
        }
    }
}
