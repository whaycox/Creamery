using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Curds.Persistence.Model.Tests
{
    using Implementation;
    using Persistence.Abstraction;
    using Abstraction;
    using Persistence.Domain;
    using Query.Domain;
    using Configuration.Abstraction;
    using Configuration.Domain;

    [TestClass]
    public class ModelBuilderTest
    {
        private List<(string tableName, Type tableType)> TestTableTypes = new List<(string tableName, Type tableType)>();
        private string TestTableName = nameof(TestTableName);
        private Type TestTableType = typeof(TestEntity);
        private string TestSchema = nameof(TestSchema);
        private string TestTable = nameof(TestTable);

        private Mock<ITypeMapper> MockTypeMapper = new Mock<ITypeMapper>();
        private Mock<IDelegateMapper> MockDelegateMapper = new Mock<IDelegateMapper>();
        private Mock<ValueEntityDelegate> MockValueEntityDelegate = new Mock<ValueEntityDelegate>();
        private Mock<IModelConfigurationFactory> MockConfigurationFactory = new Mock<IModelConfigurationFactory>();
        private Mock<IModelEntityConfiguration> MockConfiguration = new Mock<IModelEntityConfiguration>();

        private ModelBuilder TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestTableTypes.Add((TestTableName, TestTableType));

            MockTypeMapper
                .Setup(mapper => mapper.TableTypes<ITestDataModel>())
                .Returns(TestTableTypes);
            MockDelegateMapper
                .Setup(mapper => mapper.MapValueEntityDelegate<ITestDataModel>(It.IsAny<Type>()))
                .Returns(MockValueEntityDelegate.Object);
            MockConfigurationFactory
                .Setup(factory => factory.Build<ITestDataModel>(It.IsAny<Type>()))
                .Returns(MockConfiguration.Object);
            MockConfiguration
                .Setup(configuration => configuration.Schema)
                .Returns(TestSchema);
            MockConfiguration
                .Setup(configuration => configuration.Table)
                .Returns(TestTable);

            TestObject = new ModelBuilder(
                MockTypeMapper.Object,
                MockDelegateMapper.Object,
                MockConfigurationFactory.Object);
        }

        [TestMethod]
        public void TablesByNameFetchesTableTypesFromMapper()
        {
            TestObject.TablesByName<ITestDataModel>();

            MockTypeMapper.Verify(mapper => mapper.TableTypes<ITestDataModel>(), Times.Once);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(10)]
        [DataRow(14)]
        [DataRow(18)]
        public void TablesByNameFetchesConfigurationForEachTypeMapped(int numberOfTypes)
        {
            TestTableTypes.Clear();
            for (int i = 0; i < numberOfTypes; i++)
                TestTableTypes.Add(($"{TestTableName}{i}", TestTableType));

            Dictionary<string, Table> actual = TestObject.TablesByName<ITestDataModel>();

            Assert.AreEqual(numberOfTypes, actual.Count);
            MockConfigurationFactory.Verify(factory => factory.Build<ITestDataModel>(TestTableType), Times.Exactly(numberOfTypes));
        }

        [TestMethod]
        public void TablesByNameBuildsCorrectTable()
        {
            Dictionary<string, Table> actual = TestObject.TablesByName<ITestDataModel>();

            Assert.AreEqual(1, actual.Count);
            Table actualTable = actual[TestTableName];
            Assert.AreEqual(TestSchema, actualTable.Schema);
            Assert.AreEqual(TestTable, actualTable.Name);
        }

        [TestMethod]
        public void TablesByTypeFetchesTableTypesFromMapper()
        {
            TestObject.TablesByType<ITestDataModel>();

            MockTypeMapper.Verify(mapper => mapper.TableTypes<ITestDataModel>(), Times.Once);
        }

        [TestMethod]
        public void TablesByTypeFetchesConfigurationForMappedType()
        {
            Dictionary<Type, Table> actual = TestObject.TablesByType<ITestDataModel>();

            Assert.AreEqual(1, actual.Count);
            MockConfigurationFactory.Verify(factory => factory.Build<ITestDataModel>(TestTableType), Times.Once);
        }

        [TestMethod]
        public void TablesByTypeBuildsCorrectTable()
        {
            Dictionary<Type, Table> actual = TestObject.TablesByType<ITestDataModel>();

            Assert.AreEqual(1, actual.Count);
            Table actualTable = actual[TestTableType];
            Assert.AreEqual(TestSchema, actualTable.Schema);
            Assert.AreEqual(TestTable, actualTable.Name);
        }

        [TestMethod]
        public void ValueEntityDelegatesByTypeFetchesTableTypesFromMapper()
        {
            TestObject.ValueEntityDelegatesByType<ITestDataModel>();

            MockTypeMapper.Verify(mapper => mapper.TableTypes<ITestDataModel>(), Times.Once);
        }

        [TestMethod]
        public void ValueEntityDelegatesByTypeFetchesFromMapper()
        {
            Dictionary<Type, ValueEntityDelegate> actual = TestObject.ValueEntityDelegatesByType<ITestDataModel>();

            Assert.AreEqual(1, actual.Count);
            MockDelegateMapper.Verify(mapper => mapper.MapValueEntityDelegate<ITestDataModel>(TestTableType), Times.Once);
        }

        [TestMethod]
        public void ValueEntityDelegatesByTypeReturnsMappedDelegate()
        {
            Dictionary<Type, ValueEntityDelegate> actual = TestObject.ValueEntityDelegatesByType<ITestDataModel>();

            Assert.AreEqual(1, actual.Count);
            Assert.AreSame(MockValueEntityDelegate.Object, actual[TestTableType]);
        }
    }
}
