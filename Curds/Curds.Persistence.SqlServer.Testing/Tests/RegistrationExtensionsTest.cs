using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Whey.Template;

namespace Curds.Persistence.Tests
{
    using Abstraction;
    using Domain;
    using Model.Configuration.Abstraction;
    using Model.Configuration.Domain;

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
                .WithSchema(TestSchema)
                .RegisterEntity();

            VerifyServiceHasInstance<EntityConfiguration<TestEntity>>(typeof(IEntityConfiguration), ServiceLifetime.Singleton, VerifyTestEntityHasSchema);
        }
        private bool VerifyTestEntityHasSchema(EntityConfiguration<TestEntity> actual) =>
            TestSchema == actual.Schema;

        [TestMethod]
        public void CanConfigureSchemaForEntityInModel()
        {
            TestServiceCollection
                .ConfigureEntity<ITestDataModel, TestEntity>()
                .WithSchema(TestSchema)
                .RegisterEntity();

            VerifyServiceHasInstance<ModelEntityConfiguration<ITestDataModel, TestEntity>>(typeof(IModelEntityConfiguration), ServiceLifetime.Singleton, VerifyTestEntityHasSchemaInModel);
        }
        private bool VerifyTestEntityHasSchemaInModel(ModelEntityConfiguration<ITestDataModel, TestEntity> actual) =>
            TestSchema == actual.Schema;

        [TestMethod]
        public void CanConfigureTableForEntity()
        {
            TestServiceCollection
                .ConfigureEntity<TestEntity>()
                .WithTableName(TestTable)
                .RegisterEntity();

            VerifyServiceHasInstance<EntityConfiguration<TestEntity>>(typeof(IEntityConfiguration), ServiceLifetime.Singleton, VerifyTestEntityHasTable);
        }
        private bool VerifyTestEntityHasTable(EntityConfiguration<TestEntity> actual) =>
            TestTable == actual.Table;

        [TestMethod]
        public void CanConfigureTableForEntityInModel()
        {
            TestServiceCollection
                .ConfigureEntity<ITestDataModel, TestEntity>()
                .WithTableName(TestTable)
                .RegisterEntity();

            VerifyServiceHasInstance<ModelEntityConfiguration<ITestDataModel, TestEntity>>(typeof(IModelEntityConfiguration), ServiceLifetime.Singleton, VerifyTestEntityHasTableInModel);
        }
        private bool VerifyTestEntityHasTableInModel(ModelEntityConfiguration<ITestDataModel, TestEntity> actual) =>
            TestTable == actual.Table;

        [TestMethod]
        public void CanConfigureAnIntIdentity()
        {
            TestServiceCollection
                .ConfigureEntity<TestEntity>()
                    .ConfigureColumn(entity => entity.ID)
                    .IsIdentity()
                    .RegisterColumn()
                .RegisterEntity();

            VerifyServiceHasInstance<EntityConfiguration<TestEntity>>(typeof(IEntityConfiguration), ServiceLifetime.Singleton, VerifyTestEntityHasIntIdentity);
        }
        private bool VerifyTestEntityHasIntIdentity(EntityConfiguration<TestEntity> actual)
        {
            Assert.AreEqual(1, actual.Columns.Count);
            IColumnConfiguration actualColumn = actual.Columns.First();
            return actualColumn.IsIdentity.Value;
        }

        [TestMethod]
        public void CanConfigureAnIntIdentityInModel()
        {
            TestServiceCollection
                .ConfigureEntity<ITestDataModel, TestEntity>()
                    .ConfigureColumn(entity => entity.ID)
                    .IsIdentity()
                    .RegisterColumn()
                .RegisterEntity();

            VerifyServiceHasInstance<ModelEntityConfiguration<ITestDataModel, TestEntity>>(typeof(IModelEntityConfiguration), ServiceLifetime.Singleton, VerifyTestEntityHasIntIdentityInModel);
        }
        private bool VerifyTestEntityHasIntIdentityInModel(ModelEntityConfiguration<ITestDataModel, TestEntity> actual)
        {
            Assert.AreEqual(1, actual.Columns.Count);
            IColumnConfiguration actualColumn = actual.Columns.First();
            return actualColumn.IsIdentity.Value;
        }

        [TestMethod]
        public void CanConfigureAColumnName()
        {
            TestServiceCollection
                .ConfigureEntity<TestEntity>()
                    .ConfigureColumn(entity => entity.Name)
                    .WithColumnName(nameof(CanConfigureAColumnName))
                    .RegisterColumn()
                .RegisterEntity();

            VerifyServiceHasInstance<EntityConfiguration<TestEntity>>(typeof(IEntityConfiguration), ServiceLifetime.Singleton, VerifyTestEntityHasCustomColumnName);
        }
        private bool VerifyTestEntityHasCustomColumnName(EntityConfiguration<TestEntity> actual)
        {
            Assert.AreEqual(1, actual.Columns.Count);
            IColumnConfiguration actualColumn = actual.Columns.First();
            return actualColumn.Name == nameof(CanConfigureAColumnName);
        }

        [TestMethod]
        public void CanConfigureAColumnNameInModel()
        {
            TestServiceCollection
                .ConfigureEntity<ITestDataModel, TestEntity>()
                    .ConfigureColumn(entity => entity.Name)
                    .WithColumnName(nameof(CanConfigureAColumnName))
                    .RegisterColumn()
                .RegisterEntity();

            VerifyServiceHasInstance<ModelEntityConfiguration<ITestDataModel, TestEntity>>(typeof(IModelEntityConfiguration), ServiceLifetime.Singleton, VerifyTestEntityHasCustomColumnNameInModel);
        }
        private bool VerifyTestEntityHasCustomColumnNameInModel(ModelEntityConfiguration<ITestDataModel, TestEntity> actual)
        {
            Assert.AreEqual(1, actual.Columns.Count);
            IColumnConfiguration actualColumn = actual.Columns.First();
            return actualColumn.Name == nameof(CanConfigureAColumnName);
        }
    }
}
