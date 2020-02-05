using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq.Expressions;

namespace Curds.Persistence
{
    using Abstraction;
    using Domain;
    using Implementation;
    using Model.Configuration.Domain;
    using Model.Abstraction;
    using Model.Configuration.Abstraction;

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

        public static EntityConfiguration<TEntity> HasSchema<TEntity>(this EntityConfiguration<TEntity> configuration, string schemaName)
            where TEntity : BaseEntity
        {
            configuration.Schema = schemaName;
            return configuration;
        }

        public static ModelEntityConfiguration<TModel, TEntity> HasSchema<TModel, TEntity>(this ModelEntityConfiguration<TModel, TEntity> configuration, string schemaName)
            where TModel : IDataModel
            where TEntity : BaseEntity
        {
            configuration.Schema = schemaName;
            return configuration;
        }

        public static EntityConfiguration<TEntity> HasTable<TEntity>(this EntityConfiguration<TEntity> configuration, string tableName)
            where TEntity : BaseEntity
        {
            configuration.Table = tableName;
            return configuration;
        }

        public static ModelEntityConfiguration<TModel, TEntity> HasTable<TModel, TEntity>(this ModelEntityConfiguration<TModel, TEntity> configuration, string tableName)
            where TModel : IDataModel
            where TEntity : BaseEntity
        {
            configuration.Table = tableName;
            return configuration;
        }

        public static EntityConfiguration<TEntity> HasIdentity<TEntity>(this EntityConfiguration<TEntity> configuration, Expression<Func<TEntity, int>> intSelectionExpression)
                where TEntity : BaseEntity
        {
            configuration.Identity = ExpressionParser.ParseEntityValueSelection(intSelectionExpression);
            return configuration;
        }

        public static ModelEntityConfiguration<TModel, TEntity> HasIdentity<TModel, TEntity>(this ModelEntityConfiguration<TModel, TEntity> configuration, Expression<Func<TEntity, int>> intSelectionExpression)
            where TModel : IDataModel
            where TEntity : BaseEntity
        {
            configuration.Identity = ExpressionParser.ParseEntityValueSelection(intSelectionExpression);
            return configuration;
        }

        public static IServiceCollection Register<TEntity>(this EntityConfiguration<TEntity> configuration)
                where TEntity : BaseEntity
        {
            IServiceCollection services = configuration.ServiceCollection;
            configuration.ServiceCollection = null;
            services.AddSingleton<IEntityConfiguration>(configuration);
            return services;
        }

        public static IServiceCollection Register<TModel, TEntity>(this ModelEntityConfiguration<TModel, TEntity> configuration)
            where TModel : IDataModel
            where TEntity : BaseEntity
        {
            IServiceCollection services = configuration.ServiceCollection;
            configuration.ServiceCollection = null;
            services.AddSingleton<IModelEntityConfiguration>(configuration);
            return services;
        }
    }
}
