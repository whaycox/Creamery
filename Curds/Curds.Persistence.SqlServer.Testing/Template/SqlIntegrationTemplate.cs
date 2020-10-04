using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using Whey.Domain;

namespace Curds.Persistence.Template
{
    using Abstraction;
    using Domain;
    using Implementation;

    [TestCategory(nameof(TestType.Integration))]
    public abstract class SqlIntegrationTemplate : SqlTemplate
    {
        protected TestEntity TestEntity = new TestEntity();
        protected OtherEntity OtherEntity = new OtherEntity();
        protected TestEnumEntity TestEnumEntity = new TestEnumEntity();
        protected GenericToken TestGenericToken = new GenericToken();
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
                .AddCurdsPersistence()
                .AddTransient<IChildRepository, ChildRepository>()
                .ConfigureEntity<GenericToken>()
                    .HasKey(token => token.ID)
                    .RegisterEntity()
                .AddSingleton(Mock.Of<ILogger<SqlConnectionContext>>());
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

        protected void FullyPopulateOtherEntity()
        {
            OtherEntity.NullableBoolValue = OtherEntity.BoolValue;
            OtherEntity.NullableByteValue = OtherEntity.ByteValue;
            OtherEntity.NullableShortValue = OtherEntity.ShortValue;
            OtherEntity.NullableIntValue = OtherEntity.IntValue;
            OtherEntity.NullableLongValue = OtherEntity.LongValue;
            OtherEntity.NullableDateTimeValue = OtherEntity.DateTimeValue;
            OtherEntity.NullableDateTimeOffsetValue = OtherEntity.DateTimeOffsetValue;
            OtherEntity.NullableDecimalValue = OtherEntity.DecimalValue;
            OtherEntity.NullableDoubleValue = OtherEntity.DoubleValue;
        }
    }
}
