using Microsoft.Extensions.DependencyInjection;

namespace Curds.Persistence
{
    using Abstraction;
    using Domain;
    using Implementation;
    using Model.Abstraction;
    using Model.Configuration.Abstraction;
    using Model.Configuration.Implementation;
    using Model.Implementation;
    using Query.Abstraction;
    using Query.Formatters.Implementation;
    using Query.Implementation;

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
                .HasKey(entity => entity.ID)
                .ConfigureColumn(column => column.ID)
                    .IsIdentity()
                    .RegisterColumn()
                .RegisterEntity();

        private static IServiceCollection AddFactories(this IServiceCollection services) => services
            .AddSingleton<ISqlConnectionStringFactory, SqlConnectionStringFactory>()
            .AddSingleton<ISqlQueryReaderFactory, SqlQueryReaderFactory>()
            .AddSingleton<IModelConfigurationFactory, ModelConfigurationFactory>()
            .AddSingleton<IExpressionNodeFactory, ExpressionNodeFactory>()
            .AddSingleton<ISqlQueryExpressionVisitorFactory, SqlQueryExpressionVisitorFactory>()
            .AddTransient<ISqlQueryTokenFactory, SqlQueryTokenFactory>();

        private static IServiceCollection AddModelConstruction(this IServiceCollection services) => services
            .AddSingleton<ITypeMapper, TypeMapper>()
            .AddSingleton<IValueExpressionBuilder, ValueExpressionBuilder>()
            .AddSingleton<IAssignIdentityExpressionBuilder, AssignIdentityExpressionBuilder>()
            .AddSingleton<IProjectEntityExpressionBuilder, ProjectEntityExpressionBuilder>()
            .AddSingleton<IDelegateMapper, DelegateMapper>()
            .AddSingleton<IModelBuilder, ModelBuilder>()
            .AddSingleton(typeof(IModelMap<>), typeof(ModelMap<>));

        private static IServiceCollection AddQueryConstruction(this IServiceCollection services) => services
            .AddTransient<ISqlQueryParameterBuilder, SqlQueryParameterBuilder>()
            .AddTransient<ISqlQueryFormatter, ProperSqlQueryFormatter>()
            .AddTransient(typeof(ISqlQueryContext<>), typeof(SqlQueryContext<>))
            .AddTransient(typeof(ISqlQueryBuilder<>), typeof(SqlQueryBuilder<>))
            .AddTransient<ISqlQueryPhraseBuilder, SqlQueryPhraseBuilder>();
    }
}
