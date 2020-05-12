using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Whey;

namespace Curds.Persistence.Query.Tests
{
    using Abstraction;
    using Domain;
    using Implementation;
    using Model.Abstraction;
    using Persistence.Abstraction;

    [TestClass]
    public class SqlTableTest
    {
        private List<IValueModel> TestValues = new List<IValueModel>();
        private List<IValueModel> TestKeys = new List<IValueModel>();
        private List<IValueModel> TestNonIdentities = new List<IValueModel>();
        private ValueEntity TestValueEntity = new ValueEntity();

        private Mock<IEntityModel> MockEntityModel = new Mock<IEntityModel>();
        private Mock<IEntity> MockEntity = new Mock<IEntity>();
        private Mock<ValueEntityDelegate> MockValueEntityDelegate = new Mock<ValueEntityDelegate>();
        private Mock<AssignIdentityDelegate> MockAssignIdentityDelegate = new Mock<AssignIdentityDelegate>();
        private Mock<ProjectEntityDelegate> MockProjectEntityDelegate = new Mock<ProjectEntityDelegate>();
        private Mock<ISqlQueryReader> MockQueryReader = new Mock<ISqlQueryReader>();

        private SqlTable TestObject = null;

        [TestInitialize]
        public void Init()
        {
            MockEntityModel
                .Setup(model => model.Values)
                .Returns(TestValues);
            MockEntityModel
                .Setup(model => model.Keys)
                .Returns(TestKeys);
            MockEntityModel
                .Setup(model => model.NonIdentities)
                .Returns(TestNonIdentities);
            MockEntityModel
                .Setup(model => model.ValueEntity)
                .Returns(MockValueEntityDelegate.Object);
            MockEntityModel
                .Setup(model => model.AssignIdentity)
                .Returns(MockAssignIdentityDelegate.Object);
            MockEntityModel
                .Setup(model => model.ProjectEntity)
                .Returns(MockProjectEntityDelegate.Object);
            MockValueEntityDelegate
                .Setup(del => del(It.IsAny<IEntity>()))
                .Returns(TestValueEntity);
            MockProjectEntityDelegate
                .Setup(del => del(It.IsAny<ISqlQueryReader>()))
                .Returns(MockEntity.Object);

            TestObject = new SqlTable
            {
                Model = MockEntityModel.Object,
            };
        }

        [TestMethod]
        public void SchemaComesFromModel()
        {
            MockEntityModel
                .Setup(model => model.Schema)
                .Returns(nameof(SchemaComesFromModel));

            Assert.AreEqual(nameof(SchemaComesFromModel), TestObject.Schema);
        }

        [TestMethod]
        public void NameComesFromModel()
        {
            MockEntityModel
                .Setup(model => model.Table)
                .Returns(nameof(NameComesFromModel));

            Assert.AreEqual(nameof(NameComesFromModel), TestObject.Name);
        }

        private void SetupForNValues(int values)
        {
            for (int i = 0; i < values; i++)
                TestValues.Add(Mock.Of<IValueModel>());
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(10)]
        [DataRow(17)]
        [DataRow(20)]
        public void ColumnsComeFromModelValues(int values)
        {
            SetupForNValues(values);

            IList<ISqlColumn> actual = TestObject.Columns;

            Assert.AreEqual(values, actual.Count);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(10)]
        [DataRow(17)]
        [DataRow(20)]
        public void ColumnsAreOfExpectedType(int values)
        {
            SetupForNValues(values);

            IList<ISqlColumn> actual = TestObject.Columns;

            for (int i = 0; i < TestValues.Count; i++)
            {
                SqlColumn actualColumn = actual[i].VerifyIsActually<SqlColumn>();
                Assert.AreSame(TestObject, actualColumn.Table);
                Assert.AreSame(TestValues[i], actualColumn.ValueModel);
            }
        }

        private void SetupForNKeys(int keys)
        {
            for (int i = 0; i < keys; i++)
                TestKeys.Add(Mock.Of<IValueModel>());
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(10)]
        [DataRow(17)]
        [DataRow(20)]
        public void KeysComeFromModelKeys(int keys)
        {
            SetupForNKeys(keys);

            IList<ISqlColumn> actual = TestObject.Keys;

            Assert.AreEqual(keys, actual.Count);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(10)]
        [DataRow(17)]
        [DataRow(20)]
        public void KeysAreOfExpectedType(int keys)
        {
            SetupForNKeys(keys);

            IList<ISqlColumn> actual = TestObject.Keys;

            for (int i = 0; i < TestKeys.Count; i++)
            {
                SqlColumn actualColumn = actual[i].VerifyIsActually<SqlColumn>();
                Assert.AreSame(TestObject, actualColumn.Table);
                Assert.AreSame(TestKeys[i], actualColumn.ValueModel);
            }
        }

        [TestMethod]
        public void IdentityComesFromModel()
        {
            IValueModel mockValue = Mock.Of<IValueModel>();
            MockEntityModel
                .Setup(model => model.Identity)
                .Returns(mockValue);

            ISqlColumn actual = TestObject.Identity;

            SqlColumn actualColumn = actual.VerifyIsActually<SqlColumn>();
            Assert.AreSame(TestObject, actualColumn.Table);
            Assert.AreSame(mockValue, actualColumn.ValueModel);
        }

        private void SetupForNNonIdentities(int nonIdentities)
        {
            for (int i = 0; i < nonIdentities; i++)
                TestNonIdentities.Add(Mock.Of<IValueModel>());
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(10)]
        [DataRow(17)]
        [DataRow(20)]
        public void NonIdentitiesComeFromModel(int nonIdentities)
        {
            SetupForNNonIdentities(nonIdentities);

            IEnumerable<ISqlColumn> actual = TestObject.NonIdentities;

            Assert.AreEqual(nonIdentities, actual.Count());
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(10)]
        [DataRow(17)]
        [DataRow(20)]
        public void NonIdentitiesAreOfExpectedType(int nonIdentities)
        {
            SetupForNNonIdentities(nonIdentities);

            IEnumerable<ISqlColumn> actual = TestObject.NonIdentities;
            List<ISqlColumn> actualList = actual.ToList();

            for (int i = 0; i < TestNonIdentities.Count; i++)
            {
                SqlColumn actualColumn = actualList[i].VerifyIsActually<SqlColumn>();
                Assert.AreSame(TestObject, actualColumn.Table);
                Assert.AreSame(TestNonIdentities[i], actualColumn.ValueModel);
            }
        }

        [TestMethod]
        public void ValueEntityPassesThroughToModel()
        {
            ValueEntity actual = TestObject.BuildValueEntity(MockEntity.Object);

            MockValueEntityDelegate.Verify(del => del(MockEntity.Object), Times.Once);
            Assert.AreSame(TestValueEntity, actual);
        }

        [TestMethod]
        public void AssignIdentityPassesThroughToModel()
        {
            TestObject.AssignIdentities(MockQueryReader.Object, MockEntity.Object);

            MockAssignIdentityDelegate.Verify(del => del(MockQueryReader.Object, MockEntity.Object), Times.Once);
        }

        [TestMethod]
        public void ProjectEntityPassesThroughToModel()
        {
            IEntity actual = TestObject.ProjectEntity(MockQueryReader.Object);

            MockProjectEntityDelegate.Verify(del => del(MockQueryReader.Object), Times.Once);
            Assert.AreSame(MockEntity.Object, actual);
        }
    }
}
