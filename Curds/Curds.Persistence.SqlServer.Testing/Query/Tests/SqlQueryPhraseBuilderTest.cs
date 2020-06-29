using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Curds.Persistence.Query.Tests
{
    using Abstraction;
    using Domain;
    using Implementation;

    [TestClass]
    public class SqlQueryPhraseBuilderTest
    {
        private List<ISqlColumn> TestNonIdentityColumns = new List<ISqlColumn>();
        private List<ValueEntity> TestValueEntities = new List<ValueEntity>();
        private ValueEntity TestValueEntity = new ValueEntity();
        private List<ISqlQueryToken> TestSetValueTokens = new List<ISqlQueryToken>();

        private Mock<ISqlQueryTokenFactory> MockTokenFactory = new Mock<ISqlQueryTokenFactory>();
        private Mock<ISqlTable> MockTable = new Mock<ISqlTable>();
        private Mock<ISqlColumn> MockIdentityColumn = new Mock<ISqlColumn>();
        private Mock<ISqlColumn> MockNonIdentityColumn = new Mock<ISqlColumn>();
        private Mock<ISqlQueryParameterBuilder> MockParameterBuilder = new Mock<ISqlQueryParameterBuilder>();
        private Mock<ISqlQueryToken> MockSetValueToken = new Mock<ISqlQueryToken>();

        private SqlQueryPhraseBuilder TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestNonIdentityColumns.Add(MockNonIdentityColumn.Object);
            TestValueEntities.Add(TestValueEntity);
            TestSetValueTokens.Add(MockSetValueToken.Object);

            MockTable
                .Setup(table => table.Identity)
                .Returns(MockIdentityColumn.Object);
            MockTable
                .Setup(table => table.NonIdentities)
                .Returns(TestNonIdentityColumns);

            TestObject = new SqlQueryPhraseBuilder(MockTokenFactory.Object);
        }

        private ISqlQueryToken SetupTokenFactory(Expression<Func<ISqlQueryTokenFactory, ISqlQueryToken>> factoryExpression)
        {
            ISqlQueryToken testToken = Mock.Of<ISqlQueryToken>();
            MockTokenFactory
                .Setup(factoryExpression)
                .Returns(testToken);
            return testToken;
        }

        [TestMethod]
        public void CreateTemporaryIdentityTokenBuildsExpectedPhrase()
        {
            ISqlQueryToken createToken = SetupTokenFactory(factory => factory.Keyword(SqlQueryKeyword.CREATE));
            ISqlQueryToken tableToken = SetupTokenFactory(factory => factory.Keyword(SqlQueryKeyword.TABLE));
            ISqlQueryToken temporaryIdentityToken = SetupTokenFactory(factory => factory.TemporaryIdentityName(MockTable.Object));
            ISqlQueryToken columnListToken = SetupTokenFactory(factory => factory.ColumnList(new[] { MockIdentityColumn.Object }, true));
            ISqlQueryToken expectedPhraseToken = SetupTokenFactory(factory => factory.Phrase(createToken, tableToken, temporaryIdentityToken, columnListToken));

            ISqlQueryToken actual = TestObject.CreateTemporaryIdentityToken(MockTable.Object);

            Assert.AreSame(expectedPhraseToken, actual);
        }

        [TestMethod]
        public void OutputToTemporaryIdentityTokenBuildsExpectedPhrase()
        {
            ISqlQueryToken outputToken = SetupTokenFactory(factory => factory.Keyword(SqlQueryKeyword.OUTPUT));
            ISqlQueryToken insertedIdentityToken = SetupTokenFactory(factory => factory.InsertedIdentityName(MockTable.Object));
            ISqlQueryToken intoToken = SetupTokenFactory(factory => factory.Keyword(SqlQueryKeyword.INTO));
            ISqlQueryToken temporaryIdentityToken = SetupTokenFactory(factory => factory.TemporaryIdentityName(MockTable.Object));
            ISqlQueryToken expectedPhraseToken = SetupTokenFactory(factory => factory.Phrase(outputToken, insertedIdentityToken, intoToken, temporaryIdentityToken));

            ISqlQueryToken actual = TestObject.OutputToTemporaryIdentityToken(MockTable.Object);

            Assert.AreSame(expectedPhraseToken, actual);
        }

        [TestMethod]
        public void DropTemporaryIdentityTokenBuildsExpectedPhrase()
        {
            ISqlQueryToken dropToken = SetupTokenFactory(factory => factory.Keyword(SqlQueryKeyword.DROP));
            ISqlQueryToken tableToken = SetupTokenFactory(factory => factory.Keyword(SqlQueryKeyword.TABLE));
            ISqlQueryToken temporaryIdentityToken = SetupTokenFactory(factory => factory.TemporaryIdentityName(MockTable.Object));
            ISqlQueryToken expectedPhraseToken = SetupTokenFactory(factory => factory.Phrase(dropToken, tableToken, temporaryIdentityToken));

            ISqlQueryToken actual = TestObject.DropTemporaryIdentityToken(MockTable.Object);

            Assert.AreSame(expectedPhraseToken, actual);
        }

        [TestMethod]
        public void SelectNewIdentitiesTokenBuildsExpectedPhrase()
        {
            ISqlQueryToken selectToken = SetupTokenFactory(factory => factory.Keyword(SqlQueryKeyword.SELECT));
            ISqlQueryToken selectListToken = SetupTokenFactory(factory => factory.SelectList(new[] { MockIdentityColumn.Object }));
            ISqlQueryToken selectPhrase = SetupTokenFactory(factory => factory.Phrase(selectToken, selectListToken));
            ISqlQueryToken fromToken = SetupTokenFactory(factory => factory.Keyword(SqlQueryKeyword.FROM));
            ISqlQueryToken temporaryIdentityToken = SetupTokenFactory(factory => factory.TemporaryIdentityName(MockTable.Object));
            ISqlQueryToken universePhrase = SetupTokenFactory(factory => factory.Phrase(fromToken, temporaryIdentityToken));

            IEnumerable<ISqlQueryToken> actual = TestObject.SelectNewIdentitiesToken(MockTable.Object);

            CollectionAssert.AreEqual(new[] { selectPhrase, universePhrase }, actual.ToList());
        }

        [TestMethod]
        public void InsertToTableTokenBuildsExpectedPhrase()
        {
            ISqlQueryToken insertToken = SetupTokenFactory(factory => factory.Keyword(SqlQueryKeyword.INSERT));
            ISqlQueryToken objectNameToken = SetupTokenFactory(factory => factory.QualifiedObjectName(MockTable.Object));
            ISqlQueryToken columnListToken = SetupTokenFactory(factory => factory.ColumnList(TestNonIdentityColumns, false));
            ISqlQueryToken expectedPhraseToken = SetupTokenFactory(factory => factory.Phrase(insertToken, objectNameToken, columnListToken));

            ISqlQueryToken actual = TestObject.InsertToTableToken(MockTable.Object);

            Assert.AreSame(expectedPhraseToken, actual);
        }

        [TestMethod]
        public void ValueEntitiesTokenBuildsExpectedPhrase()
        {
            ISqlQueryToken valuesToken = SetupTokenFactory(factory => factory.Keyword(SqlQueryKeyword.VALUES));
            ISqlQueryToken valueEntitiesToken = SetupTokenFactory(factory => factory.ValueEntities(MockParameterBuilder.Object, TestValueEntities));

            IEnumerable<ISqlQueryToken> actual = TestObject.ValueEntitiesToken(MockParameterBuilder.Object, TestValueEntities);

            CollectionAssert.AreEqual(new[] { valuesToken, valueEntitiesToken }, actual.ToList());
        }

        [TestMethod]
        public void SelectColumnsTokenBuildsExpectedPhrase()
        {
            ISqlQueryToken selectToken = SetupTokenFactory(factory => factory.Keyword(SqlQueryKeyword.SELECT));
            ISqlQueryToken selectListToken = SetupTokenFactory(factory => factory.SelectList(TestNonIdentityColumns));
            ISqlQueryToken expectedPhraseToken = SetupTokenFactory(factory => factory.Phrase(selectToken, selectListToken));

            ISqlQueryToken actual = TestObject.SelectColumnsToken(TestNonIdentityColumns);

            Assert.AreSame(expectedPhraseToken, actual);
        }

        [TestMethod]
        public void DeleteTableTokenBuildsExpectedPhrase()
        {
            ISqlQueryToken deleteToken = SetupTokenFactory(factory => factory.Keyword(SqlQueryKeyword.DELETE));
            ISqlQueryToken objectNameToken = SetupTokenFactory(factory => factory.QualifiedObjectName(MockTable.Object));
            ISqlQueryToken expectedPhraseToken = SetupTokenFactory(factory => factory.Phrase(deleteToken, objectNameToken));

            ISqlQueryToken actual = TestObject.DeleteTableToken(MockTable.Object);

            Assert.AreSame(expectedPhraseToken, actual);
        }

        [TestMethod]
        public void UpdateTableTokenBuildsExpectedPhrase()
        {
            ISqlQueryToken updateToken = SetupTokenFactory(factory => factory.Keyword(SqlQueryKeyword.UPDATE));
            ISqlQueryToken objectNameToken = SetupTokenFactory(factory => factory.QualifiedObjectName(MockTable.Object));
            ISqlQueryToken expectedPhraseToken = SetupTokenFactory(factory => factory.Phrase(updateToken, objectNameToken));

            ISqlQueryToken actual = TestObject.UpdateTableToken(MockTable.Object);

            Assert.AreSame(expectedPhraseToken, actual);
        }

        [TestMethod]
        public void SetValuesTokenBuildsExpectedPhrase()
        {
            ISqlQueryToken setToken = SetupTokenFactory(factory => factory.Keyword(SqlQueryKeyword.SET));
            ISqlQueryToken setValuesToken = SetupTokenFactory(factory => factory.SetValues(TestSetValueTokens));
            ISqlQueryToken expectedPhraseToken = SetupTokenFactory(factory => factory.Phrase(setToken, setValuesToken));

            ISqlQueryToken actual = TestObject.SetValuesToken(TestSetValueTokens);

            Assert.AreSame(expectedPhraseToken, actual);
        }

        [TestMethod]
        public void FromUniverseTokenBuildsExpectedPhrase()
        {
            throw new NotImplementedException();
        }
    }
}
