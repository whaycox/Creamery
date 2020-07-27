using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Whey;

namespace Curds.Persistence.Query.Tests
{
    using Abstraction;
    using Domain;
    using Implementation;
    using Persistence.Abstraction;

    [TestClass]
    public class InsertedIdentitySqlTableTest
    {
        private ValueEntity TestValueEntity = new ValueEntity();

        private Mock<ISqlTable> MockTable = new Mock<ISqlTable>();
        private Mock<ISqlColumn> MockColumn = new Mock<ISqlColumn>();
        private Mock<IEntity> MockEntity = new Mock<IEntity>();
        private Mock<ISqlQueryReader> MockQueryReader = new Mock<ISqlQueryReader>();

        private InsertedIdentitySqlTable TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestObject = new InsertedIdentitySqlTable(MockTable.Object);
        }

        [TestMethod]
        public void TableComesFromConstructor()
        {
            Assert.AreSame(MockTable.Object, TestObject.Table);
        }

        [TestMethod]
        public void EntityTypeComesFromTable()
        {
            MockTable
                .Setup(table => table.EntityType)
                .Returns(typeof(int));

            Type actual = TestObject.EntityType;

            Assert.AreEqual(typeof(int), actual);
        }

        [TestMethod]
        public void SchemaIsEmpty()
        {
            Assert.AreEqual(string.Empty, TestObject.Schema);
        }

        [TestMethod]
        public void NameIsModifiedTableName()
        {
            MockTable
                .Setup(table => table.Name)
                .Returns(nameof(NameIsModifiedTableName));

            string actual = TestObject.Name;

            Assert.AreEqual($"#{nameof(NameIsModifiedTableName)}_Identities", actual);
        }

        [TestMethod]
        public void AliasIsFromTable()
        {
            MockTable
                .Setup(table => table.Alias)
                .Returns(nameof(AliasIsFromTable));

            Assert.AreEqual(nameof(AliasIsFromTable), TestObject.Alias);
        }

        [TestMethod]
        public void ColumnsAreTableIdentity()
        {
            MockTable
                .Setup(table => table.Identity)
                .Returns(MockColumn.Object);

            List<ISqlColumn> actual = TestObject.Columns;

            CollectionAssert.AreEqual(new[] { MockColumn.Object }, actual);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(7)]
        [DataRow(13)]
        public void KeysAreFromTable(int keyColumns)
        {
            List<ISqlColumn> testColumns = MockTable.SetupMock(table => table.Keys, keyColumns);

            List<ISqlColumn> actual = TestObject.Keys;

            CollectionAssert.AreEqual(testColumns, actual);
        }

        [TestMethod]
        public void KeyColumnIsFromTable()
        {
            MockTable
                .Setup(table => table.KeyColumn)
                .Returns(MockColumn.Object);

            ISqlColumn actual = TestObject.KeyColumn;

            Assert.AreSame(MockColumn.Object, actual);
        }

        [TestMethod]
        public void IdentityIsFromTable()
        {
            MockTable
                .Setup(table => table.Identity)
                .Returns(MockColumn.Object);

            ISqlColumn actual = TestObject.Identity;

            Assert.AreSame(MockColumn.Object, actual);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(7)]
        [DataRow(13)]
        public void NonIdentitiesAreEmpty(int nonIdentityColumns)
        {
            List<ISqlColumn> testColumns = MockTable.SetupMock(table => table.NonIdentities, nonIdentityColumns);

            IEnumerable<ISqlColumn> actual = TestObject.NonIdentities;

            Assert.AreEqual(0, actual.Count());
        }

        [TestMethod]
        public void InsertedIdentityTableReturnsSelf()
        {
            ISqlTable actual = TestObject.InsertedIdentityTable;

            Assert.AreSame(TestObject, actual);
        }

        [TestMethod]
        public void BuildValueEntityIsFromTable()
        {
            MockTable
                .Setup(table => table.BuildValueEntity(It.IsAny<IEntity>()))
                .Returns(TestValueEntity);

            ValueEntity actual = TestObject.BuildValueEntity(MockEntity.Object);

            Assert.AreSame(TestValueEntity, actual);
        }

        [TestMethod]
        public void AssignIdentitiesPassesToTable()
        {
            TestObject.AssignIdentities(MockQueryReader.Object, MockEntity.Object);

            MockTable.Verify(table => table.AssignIdentities(MockQueryReader.Object, MockEntity.Object), Times.Once);
        }

        [TestMethod]
        public void ProjectEntityIsFromTable()
        {
            MockTable
                .Setup(table => table.ProjectEntity(It.IsAny<ISqlQueryReader>()))
                .Returns(MockEntity.Object);

            IEntity actual = TestObject.ProjectEntity(MockQueryReader.Object);

            Assert.AreSame(MockEntity.Object, actual);
        }
    }
}
