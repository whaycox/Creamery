using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Curds.Persistence.Template
{
    using Domain;

    public abstract class SqlIntegrationTemplate : SqlTemplate
    {
        protected string TestSchema = nameof(TestSchema);

        protected IServiceCollection TestServiceCollection = new ServiceCollection();
        protected IServiceProvider TestServiceProvider = null;

        [TestInitialize]
        public void Init()
        {
            TestServiceCollection.Configure<SqlConnectionInformation>(info =>
            {
                info.Server = TestServer;
                info.Database = TestDatabase;
            });
        }

        protected void BuildServiceProvider()
        {
            TestServiceProvider = TestServiceCollection.BuildServiceProvider();
        }

        protected void RegisterServices()
        {
            TestServiceCollection
                .AddCurdsPersistence();
        }

        protected void ConfigureCustomTestEntity()
        {
            TestServiceCollection
                .ConfigureDefaultSchema(TestSchema)
                .ConfigureEntity<TestEntity>()
                    .WithTableName("TestCustomEntity")
                    .ConfigureColumn(entity => entity.ID)
                        .WithColumnName("CustomIdentityField")
                        .RegisterColumn()
                    .ConfigureColumn(entity => entity.Name)
                        .WithColumnName("SomeOtherName")
                        .RegisterColumn()
                    .RegisterEntity();
        }
    }
}
