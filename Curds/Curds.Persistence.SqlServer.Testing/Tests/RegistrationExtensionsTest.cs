using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Whey.Template;
using System;

namespace Curds.Persistence.Tests
{
    using Abstraction;
    using Domain;
    using Model.Configuration.Domain;
    using Model.Configuration.Abstraction;

    [TestClass]
    public class RegistrationExtensionsTest : RegistrationExtensionsTemplate
    {
        private string TestSchema = nameof(TestSchema);
        private string TestTable = nameof(TestTable);

        [TestMethod]
        public void CanConfigureDefaultSchemaForAllEntities()
        {
            TestServiceCollection.ConfigureDefaultSchema(TestSchema);

            VerifyServiceHasInstance<GlobalConfiguration>(typeof(IGlobalConfiguration), ServiceLifetime.Singleton, VerifyDefaultSchemaInstance);
        }
        private bool VerifyDefaultSchemaInstance(GlobalConfiguration actual) =>
            TestSchema == actual.Schema;

        [TestMethod]
        public void CanConfigureDefaultSchemaForAllEntitiesInModel()
        {
            TestServiceCollection.ConfigureDefaultSchema<ITestDataModel>(TestSchema);

            VerifyServiceHasInstance<ModelConfiguration<ITestDataModel>>(typeof(IModelConfiguration), ServiceLifetime.Singleton, VerifyDefaultSchemaInstanceInModel);
        }
        private bool VerifyDefaultSchemaInstanceInModel(ModelConfiguration<ITestDataModel> actual) =>
            TestSchema == actual.Schema;

        [TestMethod]
        public void CanConfigureSchemaForEntity()
        {
            TestServiceCollection
                .ConfigureEntity<TestEntity>()
                .HasSchema(TestSchema)
                .Register();

            VerifyServiceHasInstance<EntityConfiguration<TestEntity>>(typeof(IEntityConfiguration), ServiceLifetime.Singleton, VerifyTestEntityHasSchema);
        }
        private bool VerifyTestEntityHasSchema(EntityConfiguration<TestEntity> actual) =>
            TestSchema == actual.Schema;

        [TestMethod]
        public void CanConfigureSchemaForEntityInModel()
        {
            TestServiceCollection
                .ConfigureEntity<ITestDataModel, TestEntity>()
                .HasSchema(TestSchema)
                .Register();

            VerifyServiceHasInstance<ModelEntityConfiguration<ITestDataModel, TestEntity>>(typeof(IModelEntityConfiguration), ServiceLifetime.Singleton, VerifyTestEntityHasSchemaInModel);
        }
        private bool VerifyTestEntityHasSchemaInModel(ModelEntityConfiguration<ITestDataModel, TestEntity> actual) =>
            TestSchema == actual.Schema;

        [TestMethod]
        public void CanConfigureTableForEntity()
        {
            TestServiceCollection
                .ConfigureEntity<TestEntity>()
                .HasTable(TestTable)
                .Register();

            VerifyServiceHasInstance<EntityConfiguration<TestEntity>>(typeof(IEntityConfiguration), ServiceLifetime.Singleton, VerifyTestEntityHasTable);
        }
        private bool VerifyTestEntityHasTable(EntityConfiguration<TestEntity> actual) =>
            TestTable == actual.Table;

        [TestMethod]
        public void CanConfigureTableForEntityInModel()
        {
            TestServiceCollection
                .ConfigureEntity<ITestDataModel, TestEntity>()
                .HasTable(TestTable)
                .Register();

            VerifyServiceHasInstance<ModelEntityConfiguration<ITestDataModel, TestEntity>>(typeof(IModelEntityConfiguration), ServiceLifetime.Singleton, VerifyTestEntityHasTableInModel);
        }
        private bool VerifyTestEntityHasTableInModel(ModelEntityConfiguration<ITestDataModel, TestEntity> actual) =>
            TestTable == actual.Table;

        [TestMethod]
        public void CanConfigureAnIntIdentity()
        {
            TestServiceCollection
                .ConfigureEntity<TestEntity>()
                .HasIdentity(entity => entity.ID)
                .Register();

            VerifyServiceHasInstance<EntityConfiguration<TestEntity>>(typeof(IEntityConfiguration), ServiceLifetime.Singleton, VerifyTestEntityHasIntIdentity);
        }
        private bool VerifyTestEntityHasIntIdentity(EntityConfiguration<TestEntity> actual) =>
            actual.Identity == nameof(TestEntity.ID);

        [TestMethod]
        public void CanConfigureAnIntIdentityInModel()
        {
            TestServiceCollection
                .ConfigureEntity<ITestDataModel, TestEntity>()
                .HasIdentity(entity => entity.ID)
                .Register();

            VerifyServiceHasInstance<ModelEntityConfiguration<ITestDataModel, TestEntity>>(typeof(IModelEntityConfiguration), ServiceLifetime.Singleton, VerifyTestEntityHasIntIdentityInModel);
        }
        private bool VerifyTestEntityHasIntIdentityInModel(ModelEntityConfiguration<ITestDataModel, TestEntity> actual) =>
            actual.Identity == nameof(TestEntity.ID);
    }
}
