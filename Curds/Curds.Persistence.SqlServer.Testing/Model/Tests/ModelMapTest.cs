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
        private Type TestEntityType = typeof(TestEntity);
        //private Table TestTable = new Table();
        //private Dictionary<Type, Table> TestTablesByType = new Dictionary<Type, Table>();
        //private Dictionary<Type, ValueEntityDelegate> TestValueEntityDelegatesByType = new Dictionary<Type, ValueEntityDelegate>();
        //private Dictionary<Type, AssignIdentityDelegate> TestAssignEntityDelegatesByType = new Dictionary<Type, AssignIdentityDelegate>();
        //private Dictionary<Type, ProjectEntityDelegate> TestProjectEntityDelegatesByType = new Dictionary<Type, ProjectEntityDelegate<IEntity>>();

        private Mock<IModelBuilder> MockModelBuilder = new Mock<IModelBuilder>();
        private Mock<IEntityModel> MockEntityModel = new Mock<IEntityModel>();
        //private Mock<ValueEntityDelegate> MockValueEntityDelegate = new Mock<ValueEntityDelegate>();
        //private Mock<AssignIdentityDelegate> MockAssignEntityDelegate = new Mock<AssignIdentityDelegate>();
        //private Mock<ProjectEntityDelegate<TestEntity>> MockProjectEntityDelegate = new Mock<ProjectEntityDelegate<TestEntity>>();

        private ModelMap<ITestDataModel> TestObject = null;

        [TestInitialize]
        public void Init()
        {
            //TestTablesByType.Add(TestEntityType, TestTable);
            //TestValueEntityDelegatesByType.Add(TestEntityType, MockValueEntityDelegate.Object);
            //TestAssignEntityDelegatesByType.Add(TestEntityType, MockAssignEntityDelegate.Object);
            //TestProjectEntityDelegatesByType.Add(TestEntityType, MockProjectEntityDelegate.Object);

            MockModelBuilder
                .Setup(builder => builder.BuildEntityModels<ITestDataModel>())
                .Returns(new[] { MockEntityModel.Object });
            MockEntityModel
                .Setup(model => model.EntityType)
                .Returns(TestEntityType);
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
            TestObject = new ModelMap<ITestDataModel>(MockModelBuilder.Object);
        }

        [TestMethod]
        public void BuildingMapBuildsEntityModelsFromBuilder()
        {
            BuildTestObject();

            MockModelBuilder.Verify(builder => builder.BuildEntityModels<ITestDataModel>(), Times.Once);
        }

        [TestMethod]
        public void CanFetchBuiltModelByEntityType()
        {
            BuildTestObject();

            IEntityModel actual = TestObject.Entity<TestEntity>();

            Assert.AreSame(MockEntityModel.Object, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void ThrowsWhenFetchingUnknownEntity()
        {
            BuildTestObject();

            TestObject.Entity<OtherEntity>();
        }
    }
}
