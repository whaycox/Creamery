using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Curds.Persistence.Query.Queries.Tests
{
    using Abstraction;
    using Domain;
    using Implementation;
    using Persistence.Abstraction;
    using Persistence.Domain;
    using Query.Abstraction;
    using Template;

    [TestClass]
    public class InsertQueryTest : BaseSqlQueryTemplate
    {
        private TestEntity TestEntity = new TestEntity();
        private ValueEntity TestValueEntity = new ValueEntity();

        private Mock<ISqlTable> MockTable = new Mock<ISqlTable>();

        private InsertQuery<ITestDataModel, TestEntity> TestObject = null;

        [TestInitialize]
        public void Init()
        {
            MockQueryContext
                .Setup(context => context.AddTable<TestEntity>())
                .Returns(MockTable.Object);
            MockTable
                .Setup(model => model.BuildValueEntity(It.IsAny<IEntity>()))
                .Returns(TestValueEntity);

            TestObject = new InsertQuery<ITestDataModel, TestEntity>(
                MockPhraseBuilder.Object,
                MockQueryContext.Object);
            TestObject.Entities.Add(TestEntity);
        }

        [TestMethod]
        public void GenerateCommandBuildsCreateTemporaryIdentityPhrase()
        {
            TestObject.GenerateCommand();

            MockPhraseBuilder.Verify(builder => builder.CreateTemporaryIdentityToken(MockTable.Object), Times.Once);
        }

        [TestMethod]
        public void GenerateCommandBuildsInsertToTablePhrase()
        {
            TestObject.GenerateCommand();

            MockPhraseBuilder.Verify(builder => builder.InsertToTableToken(MockTable.Object), Times.Once);
        }

        [TestMethod]
        public void GenerateCommandBuildsOutputToTemporaryIdentityPhrase()
        {
            TestObject.GenerateCommand();

            MockPhraseBuilder.Verify(builder => builder.OutputToTemporaryIdentityToken(MockTable.Object), Times.Once);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(7)]
        [DataRow(10)]
        [DataRow(13)]
        [DataRow(20)]
        public void GenerateCommandBuildsValueEntityFromTableForEachEntity(int entities)
        {
            TestObject.Entities.Clear();
            for (int i = 0; i < entities; i++)
                TestObject.Entities.Add(TestEntity);

            TestObject.GenerateCommand();

            MockTable.Verify(table => table.BuildValueEntity(TestEntity), Times.Exactly(entities));
        }

        [TestMethod]
        public void GenerateCommandBuildsValueEntitiesPhrase()
        {
            List<ValueEntity> suppliedValueEntities = null;
            MockPhraseBuilder
                .Setup(builder => builder.ValueEntitiesToken(It.IsAny<ISqlQueryParameterBuilder>(), It.IsAny<IEnumerable<ValueEntity>>()))
                .Callback<ISqlQueryParameterBuilder, IEnumerable<ValueEntity>>((builder, entities) => suppliedValueEntities = entities.ToList());

            TestObject.GenerateCommand();

            MockPhraseBuilder.Verify(builder => builder.ValueEntitiesToken(MockParameterBuilder.Object, It.IsAny<IEnumerable<ValueEntity>>()), Times.Once);
            CollectionAssert.AreEqual(new[] { TestValueEntity }, suppliedValueEntities);
        }

        [TestMethod]
        public void GenerateCommandBuildsSelectNewIdentitiesPhrase()
        {
            TestObject.GenerateCommand();

            MockPhraseBuilder.Verify(builder => builder.SelectNewIdentitiesToken(MockTable.Object), Times.Once);
        }

        [TestMethod]
        public void GenerateCommandBuildsDropTemporaryPhrase()
        {
            TestObject.GenerateCommand();

            MockPhraseBuilder.Verify(builder => builder.DropTemporaryIdentityToken(MockTable.Object), Times.Once);
        }

        [DataTestMethod]
        [DataRow(1, 1)]
        [DataRow(1, 5)]
        [DataRow(1, 10)]
        [DataRow(5, 1)]
        [DataRow(5, 5)]
        [DataRow(5, 10)]
        public void GeneratedTokensAreExpected(int valueEntityTokens, int selectTokens)
        {
            List<ISqlQueryToken> expectedTokens = new List<ISqlQueryToken>();
            expectedTokens.Add(SetupPhraseBuilder(builder => builder.CreateTemporaryIdentityToken(It.IsAny<ISqlTable>())));
            expectedTokens.Add(SetupPhraseBuilder(builder => builder.InsertToTableToken(It.IsAny<ISqlTable>())));
            expectedTokens.Add(SetupPhraseBuilder(builder => builder.OutputToTemporaryIdentityToken(It.IsAny<ISqlTable>())));
            expectedTokens.AddRange(SetupPhraseBuilder(builder => builder.ValueEntitiesToken(It.IsAny<ISqlQueryParameterBuilder>(), It.IsAny<IEnumerable<ValueEntity>>()), valueEntityTokens));
            expectedTokens.AddRange(SetupPhraseBuilder(builder => builder.SelectNewIdentitiesToken(MockTable.Object), selectTokens));
            expectedTokens.Add(SetupPhraseBuilder(builder => builder.DropTemporaryIdentityToken(MockTable.Object)));

            TestObject.GenerateCommand();

            CollectionAssert.AreEqual(expectedTokens, FormattedTokens);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(7)]
        [DataRow(10)]
        [DataRow(13)]
        [DataRow(20)]
        public async Task ProcessResultsAssignsIdentitiesForEachEntity(int entities)
        {
            TestObject.Entities.Clear();
            var sequenceSetup = MockQueryReader.SetupSequence(reader => reader.Advance());
            for (int i = 0; i < entities; i++)
            {
                TestObject.Entities.Add(TestEntity);
                sequenceSetup.ReturnsAsync(true);
            }
            sequenceSetup.ReturnsAsync(false);

            await TestObject.ProcessResult(MockQueryReader.Object);

            MockTable.Verify(table => table.AssignIdentities(MockQueryReader.Object, TestEntity), Times.Exactly(entities));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task LessIdentitiesThanEntitiesThrows()
        {
            MockQueryReader
                .Setup(reader => reader.Advance())
                .ReturnsAsync(false);

            await TestObject.ProcessResult(MockQueryReader.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task MoreIdentitiesThanEntitiesThrows()
        {
            MockQueryReader
                .SetupSequence(reader => reader.Advance())
                .ReturnsAsync(true)
                .ReturnsAsync(true);

            await TestObject.ProcessResult(MockQueryReader.Object);
        }
    }
}
