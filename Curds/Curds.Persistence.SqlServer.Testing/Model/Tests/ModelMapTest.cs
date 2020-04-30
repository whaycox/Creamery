using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace Curds.Persistence.Model.Tests
{
    using Abstraction;
    using Domain;
    using Implementation;
    using Persistence.Abstraction;
    using Persistence.Domain;

    [TestClass]
    public class ModelMapTest
    {
        //private Type TestEntityType = typeof(TestEntity);
        //private Table TestTable = new Table();
        //private Dictionary<Type, Table> TestTablesByType = new Dictionary<Type, Table>();
        //private Dictionary<Type, ValueEntityDelegate> TestValueEntityDelegatesByType = new Dictionary<Type, ValueEntityDelegate>();
        //private Dictionary<Type, AssignIdentityDelegate> TestAssignEntityDelegatesByType = new Dictionary<Type, AssignIdentityDelegate>();
        //private Dictionary<Type, ProjectEntityDelegate> TestProjectEntityDelegatesByType = new Dictionary<Type, ProjectEntityDelegate<IEntity>>();

        //private Mock<IModelBuilder> MockModelBuilder = new Mock<IModelBuilder>();
        //private Mock<ValueEntityDelegate> MockValueEntityDelegate = new Mock<ValueEntityDelegate>();
        //private Mock<AssignIdentityDelegate> MockAssignEntityDelegate = new Mock<AssignIdentityDelegate>();
        //private Mock<ProjectEntityDelegate<TestEntity>> MockProjectEntityDelegate = new Mock<ProjectEntityDelegate<TestEntity>>();

        private ModelMap<ITestDataModel> TestObject = null;

        [TestInitialize]
        public void Init()
        {
            throw new NotImplementedException();
            //TestTablesByType.Add(TestEntityType, TestTable);
            //TestValueEntityDelegatesByType.Add(TestEntityType, MockValueEntityDelegate.Object);
            //TestAssignEntityDelegatesByType.Add(TestEntityType, MockAssignEntityDelegate.Object);
            //TestProjectEntityDelegatesByType.Add(TestEntityType, MockProjectEntityDelegate.Object);

            //MockModelBuilder
            //    .Setup(builder => builder.TablesByType<ITestDataModel>())
            //    .Returns(TestTablesByType);
            //MockModelBuilder
            //    .Setup(builder => builder.ValueEntityDelegatesByType<ITestDataModel>())
            //    .Returns(TestValueEntityDelegatesByType);
            //MockModelBuilder
            //    .Setup(builder => builder.AssignIdentityDelegatesByType<ITestDataModel>())
            //    .Returns(TestAssignEntityDelegatesByType);
            //MockModelBuilder
            //    .Setup(builder => builder.ProjectEntityDelegatesByType<ITestDataModel>())
            //    .Returns(TestProjectEntityDelegatesByType);

        }

        private void BuildTestObject()
        {
            throw new NotImplementedException();
            //TestObject = new ModelMap<ITestDataModel>(MockModelBuilder.Object);
        }

        [TestMethod]
        public void BuildingMapGetsTablesByType()
        {
            throw new NotImplementedException();
            //BuildTestObject();

            //MockModelBuilder.Verify(builder => builder.TablesByType<ITestDataModel>(), Times.Once);
        }

        [TestMethod]
        public void BuildingMapGetsValueEntityDelegatesByType()
        {
            throw new NotImplementedException();
            //BuildTestObject();

            //MockModelBuilder.Verify(builder => builder.ValueEntityDelegatesByType<ITestDataModel>(), Times.Once);
        }

        [TestMethod]
        public void BuildingMapGetsAssignIdentityDelegatesByType()
        {
            throw new NotImplementedException();
            //BuildTestObject();

            //MockModelBuilder.Verify(builder => builder.AssignIdentityDelegatesByType<ITestDataModel>(), Times.Once);
        }

        [TestMethod]
        public void BuildingMapGetsProjectEntityDelegatesByType()
        {
            throw new NotImplementedException();
            //BuildTestObject();

            //MockModelBuilder.Verify(builder => builder.ProjectEntityDelegatesByType<ITestDataModel>(), Times.Once);
        }

        [TestMethod]
        public void EntityReturnsExpectedType()
        {
            throw new NotImplementedException();
            //BuildTestObject();

            //IEntityModel<TestEntity> actual = TestObject.Entity<TestEntity>();

            //Assert.IsInstanceOfType(actual, typeof(EntityModel<TestEntity>));
        }

        [TestMethod]
        public void EntityModelHasDelegatesFromDictionaries()
        {
            throw new NotImplementedException();
            //BuildTestObject();

            //IEntityModel<TestEntity> actual = TestObject.Entity<TestEntity>();

            //EntityModel<TestEntity> model = (EntityModel<TestEntity>)actual;
            //Assert.AreSame(TestTable, model._table);
            //Assert.AreSame(MockValueEntityDelegate.Object, model.ValueEntity);
            //Assert.AreSame(MockAssignEntityDelegate.Object, model.AssignIdentity);
            //Assert.AreSame(MockProjectEntityDelegate.Object, model.ProjectEntity);
        }
    }
}
