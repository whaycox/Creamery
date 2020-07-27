using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Curds.Persistence.Query.Implementation
{
    using Abstraction;
    using Persistence.Abstraction;
    using Queries.Implementation;

    internal class SqlQueryBuilder<TDataModel> : ISqlQueryBuilder<TDataModel>
        where TDataModel : IDataModel
    {
        private IServiceProvider ServiceProvider { get; }

        private ISqlQueryContext<TDataModel> FreshContext => ServiceProvider.GetRequiredService<ISqlQueryContext<TDataModel>>();

        public SqlQueryBuilder(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public ISqlQuery Insert<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : IEntity
        {
            InsertQuery<TDataModel, TEntity> query = new InsertQuery<TDataModel, TEntity>(FreshContext);
            query.Entities.AddRange(entities);
            return query;
        }

        public ISqlUniverse<TDataModel, TEntity> From<TEntity>()
            where TEntity : IEntity => new SqlUniverse<TDataModel, TEntity>(FreshContext);
    }
}
