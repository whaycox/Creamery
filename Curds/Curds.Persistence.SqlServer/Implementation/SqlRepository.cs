using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Curds.Persistence.Implementation
{
    using Abstraction;
    using Domain;
    using Query.Abstraction;
    using Model.Abstraction;

    public class SqlRepository<TModel, TEntity> : IRepository<TModel, TEntity>
        where TModel : IDataModel
        where TEntity : IEntity
    {
        protected ISqlConnectionContext ConnectionContext { get; }
        protected ISqlQueryBuilder<TModel> QueryBuilder { get; }

        public SqlRepository(
            ISqlConnectionContext connectionContext,
            ISqlQueryBuilder<TModel> queryBuilder)
        {
            ConnectionContext = connectionContext;
            QueryBuilder = queryBuilder;
        }

        public Task Insert(TEntity entity) => Insert(new List<TEntity> { entity });

        public Task Insert(IEnumerable<TEntity> entities) => 
            ConnectionContext.ExecuteWithResult(
                QueryBuilder.Insert(entities));

        public Task<TEntity> Fetch(params object[] keys) => throw new NotImplementedException();

        public Task<List<TEntity>> FetchAll() =>
            ConnectionContext.ExecuteWithResult(
                QueryBuilder.From<TEntity>()
                .ProjectEntity());

        public Task Update(Action<TEntity> modifier, params object[] keys)
        {
            throw new NotImplementedException();
        }

        public Task Delete(params object[] keys)
        {
            throw new NotImplementedException();
        }
    }
}
