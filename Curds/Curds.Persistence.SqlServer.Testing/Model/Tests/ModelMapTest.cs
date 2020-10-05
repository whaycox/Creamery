using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace Curds.Persistence.Model.Tests
{
    using Abstraction;
    using Implementation;
    using Persistence.Abstraction;
    using Persistence.Domain;

    [TestClass]
    public class ModelMapTest
    {
        private Type TestEntityType = typeof(TestEntity);

        private Mock<IModelBuilder> MockModelBuilder = new Mock<IModelBuilder>();
        private Mock<IEntityModel> MockEntityModel = new Mock<IEntityModel>();

        private ModelMap<ITestDataModel> TestObject = null;

        [TestInitialize]
        public void Init()
        {
            MockModelBuilder
                .Setup(builder => builder.BuildEntityModels<ITestDataModel>())
                .Returns(new[] { MockEntityModel.Object });
            MockEntityModel
                .Setup(model => model.EntityType)
                .Returns(TestEntityType);

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
