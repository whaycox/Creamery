using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq.Expressions;

namespace Curds.Persistence
{
    using Abstraction;
    using Domain;
    using Implementation;
    using Model.Abstraction;
    using Model.Implementation;
    using Model.Configuration.Abstraction;
    using Model.Configuration.Domain;
    using Model.Configuration.Implementation;
    using Query.Abstraction;
    using Query.Implementation;
    using Query.Formatters.Implementation;

    public static class RegistrationExtensions
    {
        public static IServiceCollection AddCurdsPersistence(this IServiceCollection services) => services
            .AddCurdsCore()
            .AddFactories()
            .AddModelConstruction()
            .AddQueryConstruction()
            .AddScoped<ISqlConnectionContext, SqlConnectionContext>()
            .AddTransient(typeof(IRepository<,>), typeof(SqlRepository<,>))
            .ConfigureEntity<SimpleEntity>()
                .ConfigureColumn(column => column.ID)
                .IsIdentity()
                .RegisterColumn()
            .RegisterEntity();

        private static IServiceCollection AddFactories(this IServiceCollection services) => services
            .AddSingleton<ISqlConnectionStringFactory, SqlConnectionStringFactory>()
            .AddSingleton<ISqlQueryReaderFactory, SqlQueryReaderFactory>()
            .AddSingleton<IModelConfigurationFactory, ModelConfigurationFactory>()
            .AddScoped<ISqlQueryTokenFactory, SqlQueryTokenFactory>();

        private static IServiceCollection AddModelConstruction(this IServiceCollection services) => services
            .AddSingleton<ITypeMapper, TypeMapper>()
            .AddSingleton<IValueExpressionBuilder, ValueExpressionBuilder>()
            .AddSingleton<IAssignIdentityExpressionBuilder, AssignIdentityExpressionBuilder>()
            .AddSingleton<IDelegateMapper, DelegateMapper>()
            .AddSingleton<IModelBuilder, ModelBuilder>()
            .AddSingleton(typeof(IModelMap<>), typeof(ModelMap<>));

        private static IServiceCollection AddQueryConstruction(this IServiceCollection services) => services
            .AddTransient(typeof(ISqlQueryBuilder<>), typeof(SqlQueryBuilder<>))
            .AddTransient<ISqlQueryWriter, SqlQueryWriter>()
            .AddTransient<ISqlQueryFormatter, ProperSqlQueryFormatter>()
            .AddScoped<ISqlQueryParameterBuilder, SqlQueryParameterBuilder>();

        private static readonly IExpressionParser ExpressionParser = new ExpressionParser();

        public static IServiceCollection ConfigureDefaultSchema(this IServiceCollection services, string defaultSchema) => services
            .AddSingleton(typeof(IGlobalConfiguration), new GlobalConfiguration { Schema = defaultSchema });

        public static IServiceCollection ConfigureDefaultSchema<TModel>(this IServiceCollection services, string defaultSchema)
            where TModel : IDataModel => services
            .AddSingleton(typeof(IModelConfiguration), new ModelConfiguration<TModel> { Schema = defaultSchema });

        public static EntityConfiguration<TEntity> ConfigureEntity<TEntity>(this IServiceCollection services)
            where TEntity : IEntity => new EntityConfiguration<TEntity> { ServiceCollection = services };

        public static ModelEntityConfiguration<TModel, TEntity> ConfigureEntity<TModel, TEntity>(this IServiceCollection services)
            where TModel : IDataModel
            where TEntity : IEntity => new ModelEntityConfiguration<TModel, TEntity> { ServiceCollection = services };

        public static EntityConfiguration<TEntity> WithSchema<TEntity>(this EntityConfiguration<TEntity> configuration, string schemaName)
            where TEntity : IEntity
        {
            configuration.Schema = schemaName;
            return configuration;
        }

        public static ModelEntityConfiguration<TModel, TEntity> WithSchema<TModel, TEntity>(this ModelEntityConfiguration<TModel, TEntity> configuration, string schemaName)
            where TModel : IDataModel
            where TEntity : IEntity
        {
            configuration.Schema = schemaName;
            return configuration;
        }

        public static EntityConfiguration<TEntity> WithTableName<TEntity>(this EntityConfiguration<TEntity> configuration, string tableName)
            where TEntity : IEntity
        {
            configuration.Table = tableName;
            return configuration;
        }

        public static ModelEntityConfiguration<TModel, TEntity> WithTableName<TModel, TEntity>(this ModelEntityConfiguration<TModel, TEntity> configuration, string tableName)
            where TModel : IDataModel
            where TEntity : IEntity
        {
            configuration.Table = tableName;
            return configuration;
        }

        public static IServiceCollection RegisterEntity<TEntity>(this EntityConfiguration<TEntity> configuration)
            where TEntity : IEntity
        {
            IServiceCollection services = configuration.ServiceCollection;
            configuration.ServiceCollection = null;
            services.AddSingleton<IEntityConfiguration>(configuration);
            return services;
        }

        public static IServiceCollection RegisterEntity<TModel, TEntity>(this ModelEntityConfiguration<TModel, TEntity> configuration)
            where TModel : IDataModel
            where TEntity : IEntity
        {
            IServiceCollection services = configuration.ServiceCollection;
            configuration.ServiceCollection = null;
            services.AddSingleton<IModelEntityConfiguration>(configuration);
            return services;
        }

        public static ColumnConfiguration<TEntity, TValue> ConfigureColumn<TEntity, TValue>(this EntityConfiguration<TEntity> configuration, Expression<Func<TEntity, TValue>> valueSelectionExpression)
            where TEntity : IEntity
        {
            ColumnConfiguration<TEntity, TValue> column = new ColumnConfiguration<TEntity, TValue>(ExpressionParser.ParseEntityValueSelection(valueSelectionExpression))
            {
                EntityConfiguration = configuration,
            };
            return column;
        }

        public static ModelColumnConfiguration<TModel, TEntity, TValue> ConfigureColumn<TModel, TEntity, TValue>(this ModelEntityConfiguration<TModel, TEntity> configuration, Expression<Func<TEntity, TValue>> valueSelectionExpression)
            where TModel : IDataModel
            where TEntity : IEntity
        {
            ModelColumnConfiguration<TModel, TEntity, TValue> column = new ModelColumnConfiguration<TModel, TEntity, TValue>(ExpressionParser.ParseEntityValueSelection(valueSelectionExpression))
            {
                EntityConfiguration = configuration,
            };
            return column;
        }

        public static ColumnConfiguration<TEntity, byte> IsIdentity<TEntity>(this ColumnConfiguration<TEntity, byte> configuration)
            where TEntity : IEntity
        {
            configuration.IsIdentity = true;
            return configuration;
        }

        public static ColumnConfiguration<TEntity, short> IsIdentity<TEntity>(this ColumnConfiguration<TEntity, short> configuration)
            where TEntity : IEntity
        {
            configuration.IsIdentity = true;
            return configuration;
        }

        public static ColumnConfiguration<TEntity, int> IsIdentity<TEntity>(this ColumnConfiguration<TEntity, int> configuration)
            where TEntity : IEntity
        {
            configuration.IsIdentity = true;
            return configuration;
        }

        public static ColumnConfiguration<TEntity, long> IsIdentity<TEntity>(this ColumnConfiguration<TEntity, long> configuration)
            where TEntity : IEntity
        {
            configuration.IsIdentity = true;
            return configuration;
        }

        public static ColumnConfiguration<TEntity, TValue> WithColumnName<TEntity, TValue>(this ColumnConfiguration<TEntity, TValue> configuration, string columnName)
            where TEntity : IEntity
        {
            configuration.Name = columnName;
            return configuration;
        }

        public static ModelColumnConfiguration<TModel, TEntity, byte> IsIdentity<TModel, TEntity>(this ModelColumnConfiguration<TModel, TEntity, byte> configuration)
            where TModel : IDataModel
            where TEntity : IEntity
        {
            configuration.IsIdentity = true;
            return configuration;
        }

        public static ModelColumnConfiguration<TModel, TEntity, short> IsIdentity<TModel, TEntity>(this ModelColumnConfiguration<TModel, TEntity, short> configuration)
            where TModel : IDataModel
            where TEntity : IEntity
        {
            configuration.IsIdentity = true;
            return configuration;
        }

        public static ModelColumnConfiguration<TModel, TEntity, int> IsIdentity<TModel, TEntity>(this ModelColumnConfiguration<TModel, TEntity, int> configuration)
            where TModel : IDataModel
            where TEntity : IEntity
        {
            configuration.IsIdentity = true;
            return configuration;
        }

        public static ModelColumnConfiguration<TModel, TEntity, long> IsIdentity<TModel, TEntity>(this ModelColumnConfiguration<TModel, TEntity, long> configuration)
            where TModel : IDataModel
            where TEntity : IEntity
        {
            configuration.IsIdentity = true;
            return configuration;
        }

        public static ModelColumnConfiguration<TModel, TEntity, TValue> WithColumnName<TModel, TEntity, TValue>(this ModelColumnConfiguration<TModel, TEntity, TValue> configuration, string columnName)
            where TModel : IDataModel
            where TEntity : IEntity
        {
            configuration.Name = columnName;
            return configuration;
        }

        public static EntityConfiguration<TEntity> RegisterColumn<TEntity, TValue>(this ColumnConfiguration<TEntity, TValue> configuration)
            where TEntity : IEntity
        {
            EntityConfiguration<TEntity> entityConfiguration = configuration.EntityConfiguration;
            configuration.EntityConfiguration = null;
            entityConfiguration.Columns.Add(configuration);
            return entityConfiguration;
        }

        public static ModelEntityConfiguration<TModel, TEntity> RegisterColumn<TModel, TEntity, TValue>(this ModelColumnConfiguration<TModel, TEntity, TValue> configuration)
            where TModel : IDataModel
            where TEntity : IEntity
        {
            ModelEntityConfiguration<TModel, TEntity> entityConfiguration = configuration.EntityConfiguration;
            configuration.EntityConfiguration = null;
            entityConfiguration.Columns.Add(configuration);
            return entityConfiguration;
        }
    }
}
