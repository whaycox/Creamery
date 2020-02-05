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
    using Persistence.Domain;
    using Domain;
    using Abstraction;
    using Query.Domain;
    using Configuration.Abstraction;
    using Configuration.Domain;

    [TestClass]
    public class TypeMapperTest
    {
        private ModelEntityConfiguration<ITestDataModel, TestEntity> TestConfiguration = new ModelEntityConfiguration<ITestDataModel, TestEntity>();
        private string TestSchema = nameof(TestSchema);
        private string TestTable = nameof(TestTable);
        private string TestIdentity = nameof(TestEntity.ID);

        private Mock<IModelConfigurationFactory> MockModelConfigurationFactory = new Mock<IModelConfigurationFactory>();

        private TypeMapper TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestConfiguration.Schema = TestSchema;
            TestConfiguration.Table = TestTable;
            TestConfiguration.Identity = TestIdentity;

            MockModelConfigurationFactory
                .Setup(factory => factory.Build<ITestDataModel>(It.IsAny<Type>()))
                .Returns(TestConfiguration);

            TestObject = new TypeMapper(MockModelConfigurationFactory.Object);
        }

        [TestMethod]
        public void MapsTableTypes()
        {
            var actual = TestObject.TableTypes<ITestDataModel>();

            Assert.AreEqual(1, actual.Count());
            var first = actual.First();
            Assert.AreEqual(nameof(ITestDataModel.Test), first.tableName);
            Assert.AreEqual(typeof(TestEntity), first.tableType);
        }

        [TestMethod]
        [ExpectedException(typeof(ModelException))]
        public void NonTablePropertiesThrowMappingTableTypes()
        {
            TestObject.TableTypes<IPlainEntityPropertyModel>();
        }

        [TestMethod]
        [ExpectedException(typeof(ModelException))]
        public void WrongGenericPropertiesThrowMappingTableTypes()
        {
            TestObject.TableTypes<IWrongGenericTypePropertyModel>();
        }

        [TestMethod]
        [ExpectedException(typeof(ModelException))]
        public void NonInterfaceModelThrowsMappingTableTypes()
        {
            TestObject.TableTypes<NotAnInterfaceModel>();
        }

        [TestMethod]
        public void MapTableHasConfigurationSchemaAndTable()
        {
            Table actual = TestObject.MapTable<ITestDataModel>(typeof(TestEntity));

            Assert.AreEqual(TestSchema, actual.Schema);
            Assert.AreEqual(TestTable, actual.Name);
        }

        [TestMethod]
        public void MappedTableHasAllPropertiesAsColumns()
        {
            Table actual = TestObject.MapTable<ITestDataModel>(typeof(TestEntity));

            Assert.AreEqual(2, actual.Columns.Count);
            Assert.AreEqual(nameof(TestEntity.ID), actual.Columns[0].Name);
            Assert.IsTrue(actual.Columns[0].IsIdentity);
            Assert.AreEqual(nameof(TestEntity.Name), actual.Columns[1].Name);
            Assert.IsFalse(actual.Columns[1].IsIdentity);
        }
    }
}
