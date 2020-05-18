using System.Collections.Generic;
using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq;

namespace Curds.Persistence.Query.Implementation
{
    using Abstraction;
    using Model.Abstraction;
    using Persistence.Abstraction;
    using Persistence.Domain;
    using Domain;
    using Queries.Implementation;

    internal abstract class SqlUniverse<TModel> : ISqlUniverse
        where TModel : IDataModel
    {
        protected ISqlQueryContext<TModel> QueryContext { get; }

        public IEnumerable<ISqlTable> Tables => QueryContext.Tables;

        public IEnumerable<ISqlQueryToken> Filters => FilterCollection;
        private List<ISqlQueryToken> FilterCollection { get; } = new List<ISqlQueryToken>();

        public SqlUniverse(ISqlQueryContext<TModel> queryContext)
        {
            QueryContext = queryContext;
        }

        protected void AddFilter(ISqlQueryToken universeFilter) => FilterCollection.Add(universeFilter);
    }

    internal class SqlUniverse<TModel, TEntity> : SqlUniverse<TModel>, ISqlUniverse<TEntity>
        where TModel : IDataModel
        where TEntity : IEntity
    {
        private ISqlTable Table { get; }

        public SqlUniverse(ISqlQueryContext<TModel> queryContext)
            : base(queryContext)
        {
            Table = queryContext.AddTable<TEntity>();
        }

        public ISqlQuery<TEntity> ProjectEntity() => new ProjectEntityQuery<TModel, TEntity>(QueryContext)
        {
            ProjectedTable = Table,
            Source = this,
        };

        public ISqlUniverse<TEntity> Where(Expression<Func<TEntity, bool>> filterExpression)
        {
            AddFilter(QueryContext.ParseQueryExpression(filterExpression));
            return this;
        }
    }
}
