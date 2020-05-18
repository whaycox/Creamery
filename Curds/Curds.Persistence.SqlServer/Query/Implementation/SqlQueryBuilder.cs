using System.Collections.Generic;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace Curds.Persistence.Query.Implementation
{
    using Abstraction;
    using Model.Abstraction;
    using Persistence.Abstraction;
    using Queries.Implementation;

    internal class SqlQueryBuilder<TModel> : ISqlQueryBuilder<TModel>
        where TModel : IDataModel
    {
        private IServiceProvider ServiceProvider { get; }
        private ISqlQueryContext<TModel> FreshContext => ServiceProvider.GetRequiredService<ISqlQueryContext<TModel>>();

        public SqlQueryBuilder(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public ISqlQuery Insert<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : IEntity
        {
            InsertQuery<TModel, TEntity> query = new InsertQuery<TModel, TEntity>(FreshContext);
            query.Entities.AddRange(entities);
            return query;
        }

        public ISqlUniverse<TEntity> From<TEntity>()
            where TEntity : IEntity => new SqlUniverse<TModel, TEntity>(FreshContext);
    }
}
