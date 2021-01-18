using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

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
        private EntityConfiguration<TestEntity> TestEntityConfiguration = new EntityConfiguration<TestEntity> { Schema = nameof(TestEntityConfiguration), Table = nameof(TestEntityConfiguration) };
        private ColumnConfiguration<TestEntity, string> TestEntityColumnConfiguration = new ColumnConfiguration<TestEntity, string>(nameof(TestEntityColumnConfiguration)) { Name = nameof(TestEntityColumnConfiguration) };
        private EntityConfiguration<OtherEntity> OtherEntityConfiguration = new EntityConfiguration<OtherEntity>();
        private EntityConfiguration<BaseEntity> BaseEntityConfiguration = new EntityConfiguration<BaseEntity> { Schema = nameof(BaseEntityConfiguration), Table = nameof(BaseEntityConfiguration) };
        private ColumnConfiguration<BaseEntity, string> BaseEntityColumnConfiguration = new ColumnConfiguration<BaseEntity, string>(nameof(TestEntityColumnConfiguration)) { Name = nameof(BaseEntityColumnConfiguration) };
        private EntityConfiguration<TestSimpleEntity> SimpleEntityConfiguration = new EntityConfiguration<TestSimpleEntity> { Schema = nameof(SimpleEntityConfiguration), Table = nameof(SimpleEntityConfiguration) };
        private ColumnConfiguration<TestSimpleEntity, string> SimpleEntityColumnConfiguration = new ColumnConfiguration<TestSimpleEntity, string>(nameof(SimpleEntityColumnConfiguration)) { Name = nameof(SimpleEntityColumnConfiguration) };
        private List<IEntityConfiguration> TestEntityConfigurations = new List<IEntityConfiguration>();
        private ModelConfiguration<ITestDataModel> TestModelConfiguration = new ModelConfiguration<ITestDataModel> { Schema = nameof(TestModelConfiguration) };
        private ModelConfiguration<ITestDataModel> OtherModelConfiguration = new ModelConfiguration<ITestDataModel> { Schema = nameof(OtherModelConfiguration) };
        private List<IModelConfiguration> TestModelConfigurations = new List<IModelConfiguration>();
        private ModelEntityConfiguration<ITestDataModel, TestEntity> TestModelEntityConfiguration = new ModelEntityConfiguration<ITestDataModel, TestEntity> { Schema = nameof(TestModelEntityConfiguration), Table = nameof(TestModelEntityConfiguration) };
        private ModelColumnConfiguration<ITestDataModel, TestEntity, string> TestModelEntityColumnConfiguration = new ModelColumnConfiguration<ITestDataModel, TestEntity, string>(nameof(TestModelEntityColumnConfiguration)) { Name = nameof(TestModelEntityColumnConfiguration) };
        private ModelEntityConfiguration<ITestDataModel, TestSimpleEntity> SimpleModelEntityConfiguration = new ModelEntityConfiguration<ITestDataModel, TestSimpleEntity> { Schema = nameof(SimpleModelEntityConfiguration), Table = nameof(SimpleModelEntityConfiguration) };
        private ModelColumnConfiguration<ITestDataModel, TestSimpleEntity, string> SimpleModelEntityColumnConfiguration = new ModelColumnConfiguration<ITestDataModel, TestSimpleEntity, string>(nameof(SimpleModelEntityColumnConfiguration)) { Name = nameof(SimpleModelEntityColumnConfiguration) };
        private ModelEntityConfiguration<ITestDataModel, BaseEntity> BaseModelEntityConfiguration = new ModelEntityConfiguration<ITestDataModel, BaseEntity> { Schema = nameof(BaseModelEntityConfiguration), Table = nameof(BaseModelEntityConfiguration) };
        private ModelColumnConfiguration<ITestDataModel, BaseEntity, string> BaseModelEntityColumnConfiguration = new ModelColumnConfiguration<ITestDataModel, BaseEntity, string>(nameof(TestModelEntityColumnConfiguration)) { Name = nameof(BaseModelEntityColumnConfiguration) };
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

            CompiledConfiguration<ITestDataModel> configuration = TestObject.Build<ITestDataModel>(typeof(TestEntity));

            Assert.AreEqual(typeof(TestEntity), configuration.EntityType);
            Assert.AreEqual(typeof(ITestDataModel), configuration.ModelType);
            Assert.IsNull(configuration.Schema);
            Assert.AreEqual(nameof(TestEntity), configuration.Table);
            Assert.AreEqual(0, configuration.Columns.Count());
        }

        [TestMethod]
        public void GlobalConfigurationChangesSchema()
        {
            TestGlobalConfigurations.Add(TestGlobalConfiguration);
            BuildTestObject();

            CompiledConfiguration<ITestDataModel> configuration = TestObject.Build<ITestDataModel>(typeof(TestEntity));

            Assert.AreEqual(nameof(TestGlobalConfiguration), configuration.Schema);
        }

        [TestMethod]
        public void GlobalConfigurationCanOverwriteAnother()
        {
            TestGlobalConfigurations.Add(TestGlobalConfiguration);
            TestGlobalConfigurations.Add(OtherGlobalConfiguration);
            BuildTestObject();

            CompiledConfiguration<ITestDataModel> configuration = TestObject.Build<ITestDataModel>(typeof(TestEntity));

            Assert.AreEqual(nameof(OtherGlobalConfiguration), configuration.Schema);
        }

        [TestMethod]
        public void GlobalConfigurationIgnoresNullSchema()
        {
            TestGlobalConfigurations.Add(TestGlobalConfiguration);
            OtherGlobalConfiguration.Schema = null;
            TestGlobalConfigurations.Add(OtherGlobalConfiguration);
            BuildTestObject();

            CompiledConfiguration<ITestDataModel> configuration = TestObject.Build<ITestDataModel>(typeof(TestEntity));

            Assert.AreEqual(nameof(TestGlobalConfiguration), configuration.Schema);
        }

        [TestMethod]
        public void EntityConfigurationOverwritesGlobals()
        {
            TestGlobalConfigurations.Add(TestGlobalConfiguration);
            TestGlobalConfigurations.Add(OtherGlobalConfiguration);
            TestEntityConfigurations.Add(TestEntityConfiguration);
            BuildTestObject();

            CompiledConfiguration<ITestDataModel> configuration = TestObject.Build<ITestDataModel>(typeof(TestEntity));

            Assert.AreEqual(nameof(TestEntityConfiguration), configuration.Schema);
            Assert.AreEqual(nameof(TestEntityConfiguration), configuration.Table);
        }

        [TestMethod]
        public void EntityConfigurationDoesntHaveToOverwrite()
        {
            TestGlobalConfigurations.Add(TestGlobalConfiguration);
            TestEntityConfiguration.Schema = null;
            TestEntityConfigurations.Add(TestEntityConfiguration);
            BuildTestObject();

            CompiledConfiguration<ITestDataModel> configuration = TestObject.Build<ITestDataModel>(typeof(TestEntity));

            Assert.AreEqual(nameof(TestGlobalConfiguration), configuration.Schema);
            Assert.AreEqual(nameof(TestEntityConfiguration), configuration.Table);
        }

        [TestMethod]
        public void EntityConfigurationChainsUpToBaseEntity()
        {
            SimpleEntityConfiguration.Table = null;
            TestEntityConfigurations.Add(SimpleEntityConfiguration);
            TestEntityConfigurations.Add(BaseEntityConfiguration);
            BuildTestObject();

            CompiledConfiguration<ITestDataModel> configuration = TestObject.Build<ITestDataModel>(typeof(TestEntity));

            Assert.AreEqual(nameof(SimpleEntityConfiguration), configuration.Schema);
            Assert.AreEqual(nameof(BaseEntityConfiguration), configuration.Table);
        }

        [TestMethod]
        public void EntityConfigurationGetsConfiguredColumns()
        {
            TestEntityConfiguration.Columns.Add(SimpleEntityColumnConfiguration);
            TestEntityConfiguration.Columns.Add(TestEntityColumnConfiguration);
            TestEntityConfigurations.Add(TestEntityConfiguration);
            BuildTestObject();

            CompiledConfiguration<ITestDataModel> configuration = TestObject.Build<ITestDataModel>(typeof(TestEntity));

            Assert.AreEqual(2, configuration.Columns.Count);
        }

        [TestMethod]
        public void EntityConfigurationIgnoresNullColumnName()
        {
            TestEntityConfiguration.Columns.Add(BaseEntityColumnConfiguration);
            TestEntityColumnConfiguration.Name = null;
            TestEntityConfiguration.Columns.Add(TestEntityColumnConfiguration);
            TestEntityConfigurations.Add(TestEntityConfiguration);
            BuildTestObject();

            CompiledConfiguration<ITestDataModel> configuration = TestObject.Build<ITestDataModel>(typeof(TestEntity));

            Assert.AreEqual(1, configuration.Columns.Count);
            CompiledColumnConfiguration<ITestDataModel> actualColumn = configuration.Columns[TestEntityColumnConfiguration.ValueName];
            Assert.AreEqual(BaseEntityColumnConfiguration.Name, actualColumn.Name);
        }

        [TestMethod]
        public void EntityConfigurationIgnoresNullColumnIdentity()
        {
            TestEntityConfiguration.Columns.Add(BaseEntityColumnConfiguration);
            ColumnConfiguration<TestSimpleEntity, string> testConfiguration = new ColumnConfiguration<TestSimpleEntity, string>(nameof(TestEntityColumnConfiguration)) { IsIdentity = true };
            TestEntityConfiguration.Columns.Add(testConfiguration);
            TestEntityConfiguration.Columns.Add(TestEntityColumnConfiguration);
            TestEntityConfigurations.Add(TestEntityConfiguration);
            BuildTestObject();

            CompiledConfiguration<ITestDataModel> configuration = TestObject.Build<ITestDataModel>(typeof(TestEntity));

            Assert.AreEqual(1, configuration.Columns.Count);
            CompiledColumnConfiguration<ITestDataModel> actualColumn = configuration.Columns[TestEntityColumnConfiguration.ValueName];
            Assert.IsTrue(actualColumn.IsIdentity);
        }

        [TestMethod]
        public void EntityConfigurationBuildsProperColumn()
        {
            TestEntityConfiguration.Columns.Add(TestEntityColumnConfiguration);
            TestEntityConfigurations.Add(TestEntityConfiguration);
            BuildTestObject();

            CompiledConfiguration<ITestDataModel> configuration = TestObject.Build<ITestDataModel>(typeof(TestEntity));

            CompiledColumnConfiguration<ITestDataModel> actualColumn = configuration.Columns[TestEntityColumnConfiguration.ValueName];
            Assert.IsFalse(actualColumn.IsIdentity);
            Assert.AreEqual(TestEntityColumnConfiguration.ValueName, actualColumn.ValueName);
            Assert.AreEqual(TestEntityColumnConfiguration.Name, actualColumn.Name);
        }

        [TestMethod]
        public void EntityConfigurationColumnsIsAppliedAtEachTypeInChain()
        {
            BaseEntityColumnConfiguration.IsIdentity = true;
            BaseEntityConfiguration.Columns.Add(BaseEntityColumnConfiguration);
            TestEntityConfigurations.Add(BaseEntityConfiguration);
            TestEntityConfiguration.Columns.Add(TestEntityColumnConfiguration);
            TestEntityConfigurations.Add(TestEntityConfiguration);
            BuildTestObject();

            CompiledConfiguration<ITestDataModel> configuration = TestObject.Build<ITestDataModel>(typeof(TestEntity));

            CompiledColumnConfiguration<ITestDataModel> actualColumn = configuration.Columns[TestEntityColumnConfiguration.ValueName];
            Assert.IsTrue(actualColumn.IsIdentity);
            Assert.AreEqual(TestEntityColumnConfiguration.ValueName, actualColumn.ValueName);
            Assert.AreEqual(TestEntityColumnConfiguration.Name, actualColumn.Name);
        }

        [TestMethod]
        public void ModelConfigurationOverwritesEntities()
        {
            TestEntityConfigurations.Add(TestEntityConfiguration);
            TestModelConfigurations.Add(TestModelConfiguration);
            BuildTestObject();

            CompiledConfiguration<ITestDataModel> configuration = TestObject.Build<ITestDataModel>(typeof(TestEntity));

            Assert.AreEqual(nameof(TestModelConfiguration), configuration.Schema);
        }

        [TestMethod]
        public void ModelConfigurationOverwritesAnother()
        {
            TestModelConfigurations.Add(TestModelConfiguration);
            TestModelConfigurations.Add(OtherModelConfiguration);
            BuildTestObject();

            CompiledConfiguration<ITestDataModel> configuration = TestObject.Build<ITestDataModel>(typeof(TestEntity));

            Assert.AreEqual(nameof(OtherModelConfiguration), configuration.Schema);
        }

        [TestMethod]
        public void ModelConfigurationIgnoresNullSchema()
        {
            TestModelConfigurations.Add(TestModelConfiguration);
            OtherModelConfiguration.Schema = null;
            TestModelConfigurations.Add(OtherModelConfiguration);
            BuildTestObject();

            CompiledConfiguration<ITestDataModel> configuration = TestObject.Build<ITestDataModel>(typeof(TestEntity));

            Assert.AreEqual(nameof(TestModelConfiguration), configuration.Schema);
        }

        [TestMethod]
        public void ModelEntityConfigurationOverwritesAllOthers()
        {
            TestGlobalConfigurations.Add(TestGlobalConfiguration);
            TestEntityConfigurations.Add(TestEntityConfiguration);
            TestModelConfigurations.Add(TestModelConfiguration);
            TestModelEntityConfigurations.Add(TestModelEntityConfiguration);
            BuildTestObject();

            CompiledConfiguration<ITestDataModel> configuration = TestObject.Build<ITestDataModel>(typeof(TestEntity));

            Assert.AreEqual(nameof(TestModelEntityConfiguration), configuration.Schema);
            Assert.AreEqual(nameof(TestModelEntityConfiguration), configuration.Table);
        }

        [TestMethod]
        public void ModelEntityConfigurationDoesntHaveToOverwrite()
        {
            TestGlobalConfigurations.Add(TestGlobalConfiguration);
            TestModelEntityConfiguration.Schema = null;
            TestModelEntityConfigurations.Add(TestModelEntityConfiguration);
            BuildTestObject();

            CompiledConfiguration<ITestDataModel> configuration = TestObject.Build<ITestDataModel>(typeof(TestEntity));

            Assert.AreEqual(nameof(TestGlobalConfiguration), configuration.Schema);
            Assert.AreEqual(nameof(TestModelEntityConfiguration), configuration.Table);
        }

        [TestMethod]
        public void ModelEntityConfigurationChainsUpToBaseEntity()
        {
            SimpleModelEntityConfiguration.Table = null;
            TestModelEntityConfigurations.Add(SimpleModelEntityConfiguration);
            TestModelEntityConfigurations.Add(BaseModelEntityConfiguration);
            BuildTestObject();

            CompiledConfiguration<ITestDataModel> configuration = TestObject.Build<ITestDataModel>(typeof(TestEntity));

            Assert.AreEqual(nameof(SimpleModelEntityConfiguration), configuration.Schema);
            Assert.AreEqual(nameof(BaseModelEntityConfiguration), configuration.Table);
        }

        [TestMethod]
        public void ModelEntityConfigurationsDontApplyToDifferentModel()
        {
            TestGlobalConfigurations.Add(TestGlobalConfiguration);
            TestEntityConfiguration.Schema = null;
            TestEntityConfigurations.Add(TestEntityConfiguration);
            TestModelEntityConfigurations.Add(TestModelEntityConfiguration);
            BuildTestObject();

            CompiledConfiguration<IOtherDataModel> configuration = TestObject.Build<IOtherDataModel>(typeof(TestEntity));

            Assert.AreEqual(typeof(IOtherDataModel), configuration.ModelType);
            Assert.AreEqual(nameof(TestGlobalConfiguration), configuration.Schema);
            Assert.AreEqual(nameof(TestEntityConfiguration), configuration.Table);
        }

        [TestMethod]
        public void ModelEntityConfigurationGetsConfiguredColumns()
        {
            TestModelEntityConfiguration.Columns.Add(SimpleModelEntityColumnConfiguration);
            TestModelEntityConfiguration.Columns.Add(TestModelEntityColumnConfiguration);
            TestModelEntityConfigurations.Add(TestModelEntityConfiguration);
            BuildTestObject();

            CompiledConfiguration<ITestDataModel> configuration = TestObject.Build<ITestDataModel>(typeof(TestEntity));

            Assert.AreEqual(2, configuration.Columns.Count);
        }

        [TestMethod]
        public void ModelEntityConfigurationBuildsProperColumn()
        {
            TestModelEntityConfiguration.Columns.Add(TestModelEntityColumnConfiguration);
            TestModelEntityConfigurations.Add(TestModelEntityConfiguration);
            BuildTestObject();

            CompiledConfiguration<ITestDataModel> configuration = TestObject.Build<ITestDataModel>(typeof(TestEntity));

            CompiledColumnConfiguration<ITestDataModel> actualColumn = configuration.Columns[TestModelEntityColumnConfiguration.ValueName];
            Assert.IsFalse(actualColumn.IsIdentity);
            Assert.AreEqual(TestModelEntityColumnConfiguration.ValueName, actualColumn.ValueName);
            Assert.AreEqual(TestModelEntityColumnConfiguration.Name, actualColumn.Name);
        }

        [TestMethod]
        public void ModelEntityConfigurationColumnsIsAppliedAtEachTypeInChain()
        {
            BaseModelEntityColumnConfiguration.IsIdentity = true;
            BaseModelEntityConfiguration.Columns.Add(BaseModelEntityColumnConfiguration);
            TestModelEntityConfigurations.Add(BaseModelEntityConfiguration);
            TestModelEntityConfiguration.Columns.Add(TestModelEntityColumnConfiguration);
            TestModelEntityConfigurations.Add(TestModelEntityConfiguration);
            BuildTestObject();

            CompiledConfiguration<ITestDataModel> configuration = TestObject.Build<ITestDataModel>(typeof(TestEntity));

            CompiledColumnConfiguration<ITestDataModel> actualColumn = configuration.Columns[TestModelEntityColumnConfiguration.ValueName];
            Assert.IsTrue(actualColumn.IsIdentity);
            Assert.AreEqual(TestModelEntityColumnConfiguration.ValueName, actualColumn.ValueName);
            Assert.AreEqual(TestModelEntityColumnConfiguration.Name, actualColumn.Name);
        }
    }
}
