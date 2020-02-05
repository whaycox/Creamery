using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Curds.Persistence.Model.Configuration.Tests
{
    using Abstraction;
    using Domain;
    using Implementation;
    using Persistence.Abstraction;
    using Persistence.Domain;

    [TestClass]
    public class ModelConfigurationFactoryTest
    {
        private GlobalConfiguration TestGlobalConfiguration = new GlobalConfiguration { Schema = nameof(TestGlobalConfiguration) };
        private GlobalConfiguration OtherGlobalConfiguration = new GlobalConfiguration { Schema = nameof(OtherGlobalConfiguration) };
        private List<GlobalConfiguration> TestGlobalConfigurations = new List<GlobalConfiguration>();
        private EntityConfiguration<TestEntity> TestEntityConfiguration = new EntityConfiguration<TestEntity>();
        private EntityConfiguration<OtherEntity> OtherEntityConfiguration = new EntityConfiguration<OtherEntity>();
        private EntityConfiguration<BaseEntity> BaseEntityConfiguration = new EntityConfiguration<BaseEntity>();
        private EntityConfiguration<SimpleEntity> SimpleEntityConfiguration = new EntityConfiguration<SimpleEntity>();
        private List<IEntityConfiguration> TestEntityConfigurations = new List<IEntityConfiguration>();
        private ModelConfiguration<ITestDataModel> TestModelConfiguration = new ModelConfiguration<ITestDataModel> { Schema = nameof(TestModelConfiguration) };
        private ModelConfiguration<ITestDataModel> OtherModelConfiguration = new ModelConfiguration<ITestDataModel> { Schema = nameof(OtherModelConfiguration) };
        private List<IModelConfiguration> TestModelConfigurations = new List<IModelConfiguration>();
        private ModelEntityConfiguration<ITestDataModel, TestEntity> TestModelEntityConfiguration = new ModelEntityConfiguration<ITestDataModel, TestEntity>();
        private ModelEntityConfiguration<ITestDataModel, SimpleEntity> SimpleModelEntityConfiguration = new ModelEntityConfiguration<ITestDataModel, SimpleEntity>();
        private ModelEntityConfiguration<ITestDataModel, BaseEntity> BaseModelEntityConfiguration = new ModelEntityConfiguration<ITestDataModel, BaseEntity>();
        private List<IModelEntityConfiguration> TestModelEntityConfigurations = new List<IModelEntityConfiguration>();

        private ModelConfigurationFactory TestObject = null;

        private void BuildTestObject()
        {
            TestObject = new ModelConfigurationFactory(
                TestGlobalConfigurations,
                TestEntityConfigurations,
                TestModelConfigurations,
                TestModelEntityConfigurations);
        }

        [TestMethod]
        public void ModelIsDefaultWithNoConfigurations()
        {
            BuildTestObject();

            IModelEntityConfiguration configuration = TestObject.Build<ITestDataModel>(typeof(TestEntity));

            Assert.AreEqual(typeof(TestEntity), configuration.EntityType);
            Assert.AreEqual(typeof(ITestDataModel), configuration.ModelType);
            Assert.IsNull(configuration.Identity);
            Assert.IsNull(configuration.Schema);
            Assert.AreEqual(nameof(TestEntity), configuration.Table);
        }

        [TestMethod]
        public void GlobalConfigurationChangesSchema()
        {
            TestGlobalConfigurations.Add(TestGlobalConfiguration);
            BuildTestObject();

            IModelEntityConfiguration configuration = TestObject.Build<ITestDataModel>(typeof(TestEntity));

            Assert.IsNull(configuration.Identity);
            Assert.AreEqual(nameof(TestGlobalConfiguration), configuration.Schema);
            Assert.AreEqual(nameof(TestEntity), configuration.Table);
        }

        [TestMethod]
        public void GlobalConfigurationCanOverwriteAnother()
        {
            TestGlobalConfigurations.Add(TestGlobalConfiguration);
            TestGlobalConfigurations.Add(OtherGlobalConfiguration);
            BuildTestObject();

            IModelEntityConfiguration configuration = TestObject.Build<ITestDataModel>(typeof(TestEntity));

            Assert.IsNull(configuration.Identity);
            Assert.AreEqual(nameof(OtherGlobalConfiguration), configuration.Schema);
            Assert.AreEqual(nameof(TestEntity), configuration.Table);
        }

        [TestMethod]
        public void EntityConfigurationOverwritesGlobals()
        {
            TestGlobalConfigurations.Add(TestGlobalConfiguration);
            TestGlobalConfigurations.Add(OtherGlobalConfiguration);
            TestEntityConfiguration.Schema = nameof(TestEntityConfiguration);
            TestEntityConfigurations.Add(TestEntityConfiguration);
            BuildTestObject();

            IModelEntityConfiguration configuration = TestObject.Build<ITestDataModel>(typeof(TestEntity));

            Assert.AreEqual(nameof(TestEntityConfiguration), configuration.Schema);
        }

        [TestMethod]
        public void EntityConfigurationDoesntHaveToOverwrite()
        {
            TestGlobalConfigurations.Add(TestGlobalConfiguration);
            TestEntityConfiguration.Identity = nameof(TestEntityConfiguration);
            TestEntityConfiguration.Table = nameof(TestEntityConfiguration);
            TestEntityConfigurations.Add(TestEntityConfiguration);
            BuildTestObject();

            IModelEntityConfiguration configuration = TestObject.Build<ITestDataModel>(typeof(TestEntity));

            Assert.AreEqual(nameof(TestEntityConfiguration), configuration.Identity);
            Assert.AreEqual(nameof(TestGlobalConfiguration), configuration.Schema);
            Assert.AreEqual(nameof(TestEntityConfiguration), configuration.Table);
        }

        [TestMethod]
        public void EntityConfigurationChainsUpToBaseEntity()
        {
            TestEntityConfiguration.Table = nameof(TestEntityConfiguration);
            TestEntityConfigurations.Add(TestEntityConfiguration);
            SimpleEntityConfiguration.Schema = nameof(SimpleEntityConfiguration);
            TestEntityConfigurations.Add(SimpleEntityConfiguration);
            BaseEntityConfiguration.Identity = nameof(BaseEntityConfiguration);
            TestEntityConfigurations.Add(BaseEntityConfiguration);
            BuildTestObject();

            IModelEntityConfiguration configuration = TestObject.Build<ITestDataModel>(typeof(TestEntity));

            Assert.AreEqual(nameof(BaseEntityConfiguration), configuration.Identity);
            Assert.AreEqual(nameof(SimpleEntityConfiguration), configuration.Schema);
            Assert.AreEqual(nameof(TestEntityConfiguration), configuration.Table);
        }

        [TestMethod]
        public void ModelConfigurationOverwritesEntities()
        {
            TestEntityConfiguration.Schema = nameof(TestEntityConfiguration);
            TestEntityConfigurations.Add(TestEntityConfiguration);
            TestModelConfigurations.Add(TestModelConfiguration);
            BuildTestObject();

            IModelEntityConfiguration configuration = TestObject.Build<ITestDataModel>(typeof(TestEntity));

            Assert.AreEqual(nameof(TestModelConfiguration), configuration.Schema);
        }

        [TestMethod]
        public void ModelConfigurationOverwritesAnother()
        {
            TestModelConfigurations.Add(TestModelConfiguration);
            TestModelConfigurations.Add(OtherModelConfiguration);
            BuildTestObject();

            IModelEntityConfiguration configuration = TestObject.Build<ITestDataModel>(typeof(TestEntity));

            Assert.IsNull(configuration.Identity);
            Assert.AreEqual(nameof(OtherModelConfiguration), configuration.Schema);
            Assert.AreEqual(nameof(TestEntity), configuration.Table);
        }

        [TestMethod]
        public void ModelEntityConfigurationOverwritesAllOthers()
        {
            TestGlobalConfigurations.Add(TestGlobalConfiguration);
            TestEntityConfiguration.Schema = nameof(TestEntityConfiguration);
            TestEntityConfigurations.Add(TestEntityConfiguration);
            TestModelConfigurations.Add(TestModelConfiguration);
            TestModelEntityConfiguration.Schema = nameof(TestModelEntityConfiguration);
            TestModelEntityConfigurations.Add(TestModelEntityConfiguration);
            BuildTestObject();

            IModelEntityConfiguration configuration = TestObject.Build<ITestDataModel>(typeof(TestEntity));

            Assert.IsNull(configuration.Identity);
            Assert.AreEqual(nameof(TestModelEntityConfiguration), configuration.Schema);
            Assert.AreEqual(nameof(TestEntity), configuration.Table);
        }

        [TestMethod]
        public void ModelEntityConfigurationDoesntHaveToOverwrite()
        {
            TestGlobalConfigurations.Add(TestGlobalConfiguration);
            TestModelEntityConfiguration.Table = nameof(TestModelEntityConfiguration);
            TestModelEntityConfigurations.Add(TestModelEntityConfiguration);
            BuildTestObject();

            IModelEntityConfiguration configuration = TestObject.Build<ITestDataModel>(typeof(TestEntity));

            Assert.IsNull(configuration.Identity);
            Assert.AreEqual(nameof(TestGlobalConfiguration), configuration.Schema);
            Assert.AreEqual(nameof(TestModelEntityConfiguration), configuration.Table);
        }

        [TestMethod]
        public void ModelEntityConfigurationChainsUpToBaseEntity()
        {
            TestModelEntityConfiguration.Table = nameof(TestModelEntityConfiguration);
            TestModelEntityConfigurations.Add(TestModelEntityConfiguration);
            SimpleModelEntityConfiguration.Schema = nameof(SimpleModelEntityConfiguration);
            TestModelEntityConfigurations.Add(SimpleModelEntityConfiguration);
            BaseModelEntityConfiguration.Identity = nameof(BaseModelEntityConfiguration);
            TestModelEntityConfigurations.Add(BaseModelEntityConfiguration);
            BuildTestObject();

            IModelEntityConfiguration configuration = TestObject.Build<ITestDataModel>(typeof(TestEntity));

            Assert.AreEqual(nameof(BaseModelEntityConfiguration), configuration.Identity);
            Assert.AreEqual(nameof(SimpleModelEntityConfiguration), configuration.Schema);
            Assert.AreEqual(nameof(TestModelEntityConfiguration), configuration.Table);
        }

        [TestMethod]
        public void ModelEntityConfigurationsDontApplyToDifferentModel()
        {
            TestGlobalConfigurations.Add(TestGlobalConfiguration);
            TestEntityConfiguration.Identity = nameof(TestEntityConfiguration);
            TestEntityConfigurations.Add(TestEntityConfiguration);
            TestModelEntityConfiguration.Table = nameof(TestModelEntityConfiguration);
            TestModelEntityConfigurations.Add(TestModelEntityConfiguration);
            BuildTestObject();

            IModelEntityConfiguration configuration = TestObject.Build<IOtherDataModel>(typeof(TestEntity));

            Assert.AreEqual(typeof(IOtherDataModel), configuration.ModelType);
            Assert.AreEqual(nameof(TestEntityConfiguration), configuration.Identity);
            Assert.AreEqual(nameof(TestGlobalConfiguration), configuration.Schema);
            Assert.AreEqual(nameof(TestEntity), configuration.Table);
        }
    }
}
