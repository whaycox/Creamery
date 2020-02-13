using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Curds.Persistence
{
    using Abstraction;
    using Domain;
    using Implementation;
    using Model.Abstraction;
    using Model.Configuration.Abstraction;
    using Model.Configuration.Domain;
    using Model.Domain;

    public static class RegistrationExtensions
    {
        private static readonly IExpressionParser ExpressionParser = new ExpressionParser();

        public static IServiceCollection RegisterModel<TModel>(this IServiceCollection services)
            where TModel : IDataModel => services
            .AddTransient(provider => provider.GetService<IModelMapFactory>().Build<TModel>());

        public static IServiceCollection ConfigureDefaultSchema(this IServiceCollection services, string defaultSchema) => services
            .AddSingleton(typeof(IGlobalConfiguration), new GlobalConfiguration { Schema = defaultSchema });

        public static IServiceCollection ConfigureDefaultSchema<TModel>(this IServiceCollection services, string defaultSchema)
            where TModel : IDataModel => services
            .AddSingleton(typeof(IModelConfiguration), new ModelConfiguration<TModel> { Schema = defaultSchema });

        public static EntityConfiguration<TEntity> ConfigureEntity<TEntity>(this IServiceCollection services)
            where TEntity : BaseEntity => new EntityConfiguration<TEntity> { ServiceCollection = services };

        public static ModelEntityConfiguration<TModel, TEntity> ConfigureEntity<TModel, TEntity>(this IServiceCollection services)
            where TModel : IDataModel
            where TEntity : BaseEntity => new ModelEntityConfiguration<TModel, TEntity> { ServiceCollection = services };

        public static EntityConfiguration<TEntity> WithSchema<TEntity>(this EntityConfiguration<TEntity> configuration, string schemaName)
            where TEntity : BaseEntity
        {
            configuration.Schema = schemaName;
            return configuration;
        }

        public static ModelEntityConfiguration<TModel, TEntity> WithSchema<TModel, TEntity>(this ModelEntityConfiguration<TModel, TEntity> configuration, string schemaName)
            where TModel : IDataModel
            where TEntity : BaseEntity
        {
            configuration.Schema = schemaName;
            return configuration;
        }

        public static EntityConfiguration<TEntity> WithTableName<TEntity>(this EntityConfiguration<TEntity> configuration, string tableName)
            where TEntity : BaseEntity
        {
            configuration.Table = tableName;
            return configuration;
        }

        public static ModelEntityConfiguration<TModel, TEntity> WithTableName<TModel, TEntity>(this ModelEntityConfiguration<TModel, TEntity> configuration, string tableName)
            where TModel : IDataModel
            where TEntity : BaseEntity
        {
            configuration.Table = tableName;
            return configuration;
        }

        public static IServiceCollection RegisterEntity<TEntity>(this EntityConfiguration<TEntity> configuration)
                where TEntity : BaseEntity
        {
            IServiceCollection services = configuration.ServiceCollection;
            configuration.ServiceCollection = null;
            services.AddSingleton<IEntityConfiguration>(configuration);
            return services;
        }

        public static IServiceCollection RegisterEntity<TModel, TEntity>(this ModelEntityConfiguration<TModel, TEntity> configuration)
            where TModel : IDataModel
            where TEntity : BaseEntity
        {
            IServiceCollection services = configuration.ServiceCollection;
            configuration.ServiceCollection = null;
            services.AddSingleton<IModelEntityConfiguration>(configuration);
            return services;
        }

        public static ColumnConfiguration<TEntity> ConfigureColumn<TEntity, TValue>(this EntityConfiguration<TEntity> configuration, Expression<Func<TEntity, TValue>> valueSelectionExpression)
            where TEntity : BaseEntity
        {
            ColumnConfiguration<TEntity> column = new ColumnConfiguration<TEntity>(ExpressionParser.ParseEntityValueSelection(valueSelectionExpression))
            {
                EntityConfiguration = configuration,
            };
            return column;
        }

        public static ModelColumnConfiguration<TModel, TEntity> ConfigureColumn<TModel, TEntity, TValue>(this ModelEntityConfiguration<TModel, TEntity> configuration, Expression<Func<TEntity, TValue>> valueSelectionExpression)
            where TModel : IDataModel
            where TEntity : BaseEntity
        {
            ModelColumnConfiguration<TModel, TEntity> column = new ModelColumnConfiguration<TModel, TEntity>(ExpressionParser.ParseEntityValueSelection(valueSelectionExpression))
            {
                EntityConfiguration = configuration,
            };
            return column;
        }

        public static ColumnConfiguration<TEntity> IsIdentity<TEntity>(this ColumnConfiguration<TEntity> configuration)
                where TEntity : BaseEntity
        {
            configuration.IsIdentity = true;
            return configuration;
        }

        public static ModelColumnConfiguration<TModel, TEntity> IsIdentity<TModel, TEntity>(this ModelColumnConfiguration<TModel, TEntity> configuration)
            where TModel : IDataModel
            where TEntity : BaseEntity
        {
            configuration.IsIdentity = true;
            return configuration;
        }

        public static EntityConfiguration<TEntity> RegisterColumn<TEntity>(this ColumnConfiguration<TEntity> configuration)
                where TEntity : BaseEntity
        {
            EntityConfiguration<TEntity> entityConfiguration = configuration.EntityConfiguration;
            configuration.EntityConfiguration = null;
            entityConfiguration.Columns.Add(configuration);
            return entityConfiguration;
        }

        public static ModelEntityConfiguration<TModel, TEntity> RegisterColumn<TModel, TEntity>(this ModelColumnConfiguration<TModel, TEntity> configuration)
            where TModel : IDataModel
            where TEntity : BaseEntity
        {
            ModelEntityConfiguration<TModel, TEntity> entityConfiguration = configuration.EntityConfiguration;
            configuration.EntityConfiguration = null;
            entityConfiguration.Columns.Add(configuration);
            return entityConfiguration;
        }
    }
}
