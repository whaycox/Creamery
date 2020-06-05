using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Linq;

namespace Curds.Persistence.Implementation
{
    using Abstraction;
    using Domain;
    using Query.Abstraction;
    using Model.Abstraction;

    public class SqlRepository<TDataModel, TEntity> : IRepository<TDataModel, TEntity>
        where TDataModel : IDataModel
        where TEntity : IEntity
    {
        protected ISqlConnectionContext ConnectionContext { get; }
        protected ISqlQueryBuilder<TDataModel> QueryBuilder { get; }

        public SqlRepository(
            ISqlConnectionContext connectionContext,
            ISqlQueryBuilder<TDataModel> queryBuilder)
        {
            ConnectionContext = connectionContext;
            QueryBuilder = queryBuilder;
        }

        public Task Insert(TEntity entity) => Insert(new List<TEntity> { entity });

        public Task Insert(IEnumerable<TEntity> entities) => ConnectionContext.ExecuteWithResult(InsertQuery(entities));
        private ISqlQuery InsertQuery(IEnumerable<TEntity> entities) => QueryBuilder.Insert(entities);

        public async Task<TEntity> Fetch(params object[] keys)
        {
            IList<TEntity> entities = await ConnectionContext.ExecuteWithResult(FetchQuery(keys));
            if (entities.Count == 0)
                throw new KeyNotFoundException();
            if (entities.Count != 1)
                throw new Exception();
            return entities.First();
        }
        private ISqlQuery<TEntity> FetchQuery(object[] keys) => QueryBuilder
            .From<TEntity>()
            .Where(entity => entity.Keys == keys)
            .ProjectEntity();

        public Task<IList<TEntity>> FetchAll() => ConnectionContext.ExecuteWithResult(FetchAllQuery);
        private ISqlQuery<TEntity> FetchAllQuery => QueryBuilder
            .From<TEntity>()
            .ProjectEntity();

        public Task Update(Action<TEntity> modifier, params object[] keys)
        {
            throw new NotImplementedException();
        }

        public Task Delete(params object[] keys) => ConnectionContext.Execute(DeleteQuery(keys));
        private ISqlQuery DeleteQuery(object[] keys) => QueryBuilder
            .From<TEntity>()
            .Where(entity => entity.Keys == keys)
            .Delete();
    }
}
