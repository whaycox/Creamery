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
    using Domain;

    [TestClass]
    public class ModelMapTest
    {
        private Dictionary<string, Table> TestTablesByName = new Dictionary<string, Table>();
        private string TestTableName = nameof(TestTableName);
        private Table TestTable = new Table();
        private Dictionary<Type, Table> TestTablesByType = new Dictionary<Type, Table>();
        private Type TestTableType = typeof(TestEntity);
        private Dictionary<Type, ValueEntityDelegate> TestValueEntityDelegatesByType = new Dictionary<Type, ValueEntityDelegate>();
        private TestEntity TestEntity = new TestEntity();
        private ValueEntity TestValueEntity = new ValueEntity<TestEntity>();

        private Mock<IModelBuilder> MockModelBuilder = new Mock<IModelBuilder>();
        private Mock<ValueEntityDelegate> MockValueEntityDelegate = new Mock<ValueEntityDelegate>();

        private ModelMap<ITestDataModel> TestObject = null;

        private void BuildTestObject()
        {
            TestObject = new ModelMap<ITestDataModel>(MockModelBuilder.Object);
        }

        [TestMethod]
        public void BuildingMapGetsTablesByType()
        {
            BuildTestObject();

            MockModelBuilder.Verify(builder => builder.TablesByType<ITestDataModel>(), Times.Once);
        }

        [TestMethod]
        public void BuildingMapGetsValueEntityDelegatesByType()
        {
            BuildTestObject();

            MockModelBuilder.Verify(builder => builder.ValueEntityDelegatesByType<ITestDataModel>(), Times.Once);
        }

        [TestMethod]
        public void CanGetMappedTableByType()
        {
            TestTablesByType.Add(TestTableType, TestTable);
            MockModelBuilder
                .Setup(builder => builder.TablesByType<ITestDataModel>())
                .Returns(TestTablesByType);
            BuildTestObject();

            throw new NotImplementedException();
            //Table actual = TestObject.Table(TestTableType);

            //Assert.AreSame(TestTable, actual);
        }

        [TestMethod]
        public void ValueEntityInvokesDelegateByType()
        {
            MockValueEntityDelegate
                .Setup(del => del(It.IsAny<BaseEntity>()))
                .Returns(TestValueEntity);
            TestValueEntityDelegatesByType.Add(TestTableType, MockValueEntityDelegate.Object);
            MockModelBuilder
                .Setup(builder => builder.ValueEntityDelegatesByType<ITestDataModel>())
                .Returns(TestValueEntityDelegatesByType);
            BuildTestObject();

            throw new NotImplementedException();
            //ValueEntity actual = TestObject.ValueEntity(TestEntity);

            //MockValueEntityDelegate.Verify(del => del(TestEntity), Times.Once);
            //Assert.AreSame(TestValueEntity, actual);
        }
    }
}
