using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Curds.Persistence.Query.Tests
{
    using Implementation;
    using Domain;
    using Abstraction;
    using Tokens.Implementation;
    using Model.Domain;
    using Template;
    using Persistence.Domain;
    using Values.Domain;
    using Model.Abstraction;

    [TestClass]
    public class SqlQueryTokenFactoryTest
    {
        private List<IValueModel> TestValueModels = new List<IValueModel>();
        private string TestSchema = nameof(TestSchema);
        private string TestTableName = nameof(TestTableName);
        private string TestValueName = nameof(TestValueName);

        private Mock<IEntityModel> MockEntityModel = new Mock<IEntityModel>();
        private Mock<IValueModel> MockValueModel = new Mock<IValueModel>();
        private Mock<ISqlQueryParameterBuilder> MockParameterBuilder = new Mock<ISqlQueryParameterBuilder>();

        private SqlQueryTokenFactory TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestValueModels.Add(MockValueModel.Object);

            MockEntityModel
                .Setup(model => model.Schema)
                .Returns(TestSchema);
            MockEntityModel
                .Setup(model => model.Table)
                .Returns(TestTableName);
            MockValueModel
                .Setup(model => model.Name)
                .Returns(TestValueName);

            TestObject = new SqlQueryTokenFactory(MockParameterBuilder.Object);
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
            ISqlQueryToken actual = TestObject.ColumnList(TestValueModels, testIncludeDefinitions);

            Assert.IsInstanceOfType(actual, typeof(ColumnListSqlQueryToken));
            ColumnListSqlQueryToken columnList = (ColumnListSqlQueryToken)actual;
            CollectionAssert.AreEqual(TestValueModels, columnList.Values);
            Assert.AreEqual(testIncludeDefinitions, columnList.IncludeDefinition);
            Assert.IsTrue(columnList.IncludeGrouping);
        }

        [TestMethod]
        public void SelectListBuildsExpectedToken()
        {
            ISqlQueryToken actual = TestObject.SelectList(TestValueModels);

            Assert.IsInstanceOfType(actual, typeof(ColumnListSqlQueryToken));
            ColumnListSqlQueryToken columnList = (ColumnListSqlQueryToken)actual;
            CollectionAssert.AreEqual(TestValueModels, columnList.Values);
            Assert.IsFalse(columnList.IncludeDefinition);
            Assert.IsFalse(columnList.IncludeGrouping);
        }

        [TestMethod]
        public void TemporaryIdentityNameBuildsExpectedToken()
        {
            ISqlQueryToken actual = TestObject.TemporaryIdentityName(MockEntityModel.Object);

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
        public void QualifiedObjectNameBuildsExpectedToken()
        {
            ISqlQueryToken actual = TestObject.QualifiedObjectName(MockEntityModel.Object);

            Assert.IsInstanceOfType(actual, typeof(QualifiedObjectSqlQueryToken));
            QualifiedObjectSqlQueryToken qualifiedName = (QualifiedObjectSqlQueryToken)actual;
            Assert.AreEqual(2, qualifiedName.Names.Count);
            Assert.AreEqual(TestSchema, qualifiedName.Names[0].Name);
            Assert.AreEqual(TestTableName, qualifiedName.Names[1].Name);
        }

        [TestMethod]
        public void InsertedIdentityNameBuildsExpectedToken()
        {
            MockEntityModel
                .Setup(model => model.Identity)
                .Returns(MockValueModel.Object);

            ISqlQueryToken actual = TestObject.InsertedIdentityName(MockEntityModel.Object);

            Assert.IsInstanceOfType(actual, typeof(InsertedIdentityColumnSqlQueryToken));
            InsertedIdentityColumnSqlQueryToken inserted = (InsertedIdentityColumnSqlQueryToken)actual;
            Assert.AreEqual(2, inserted.Names.Count);
            Assert.AreEqual(nameof(SqlQueryKeyword.inserted), inserted.Names[0].Name);
            Assert.AreEqual(TestValueName, inserted.Names[1].Name);
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
                .Setup(builder => builder.RegisterNewParamater(testValue))
                .Returns(nameof(ValueEntitiesBuildsExpectedToken));

            ISqlQueryToken actual = TestObject.ValueEntities(testEntities);

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
