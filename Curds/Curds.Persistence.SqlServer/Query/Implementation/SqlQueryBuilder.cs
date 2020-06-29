using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Curds.Persistence.Query.Implementation
{
    using Abstraction;
    using Persistence.Abstraction;
    using Queries.Implementation;

    internal class SqlQueryBuilder<TModel> : ISqlQueryBuilder<TModel>
        where TModel : IDataModel
    {
        private IServiceProvider ServiceProvider { get; }

        private ISqlQueryTokenFactory TokenFactory => ServiceProvider.GetRequiredService<ISqlQueryTokenFactory>();
        private ISqlQueryPhraseBuilder PhraseBuilder => ServiceProvider.GetRequiredService<ISqlQueryPhraseBuilder>();
        private ISqlQueryContext<TModel> FreshContext => ServiceProvider.GetRequiredService<ISqlQueryContext<TModel>>();

        public SqlQueryBuilder(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public ISqlQuery Insert<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : IEntity
        {
            InsertQuery<TModel, TEntity> query = new InsertQuery<TModel, TEntity>(
                PhraseBuilder,
                FreshContext);
            query.Entities.AddRange(entities);
            return query;
        }

        public ISqlUniverse<TEntity> From<TEntity>()
            where TEntity : IEntity => new SqlUniverse<TModel, TEntity>(
                TokenFactory,
                PhraseBuilder,
                FreshContext);
    }
}
