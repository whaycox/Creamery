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
        protected ISqlQueryBuilder<TDataModel> QueryBuilder { get; }

        public SqlRepository(ISqlQueryBuilder<TDataModel> queryBuilder)
        {
            QueryBuilder = queryBuilder;
        }

        public Task Insert(TEntity entity) => Insert(new List<TEntity> { entity });

        public Task Insert(IEnumerable<TEntity> entities) => InsertQuery(entities).Execute();
        private ISqlQuery InsertQuery(IEnumerable<TEntity> entities) => QueryBuilder.Insert(entities);

        protected async Task<IList<TEntity>> FetchEntities(ISqlQuery<TEntity> entityQuery)
        {
            await entityQuery.Execute();
            return entityQuery.Results;
        }

        public async Task<TEntity> Fetch(params object[] keys)
        {
            IList<TEntity> entities = await FetchEntities(FetchQuery(keys));
            if (entities.Count == 0)
                throw new KeyNotFoundException();
            if (entities.Count != 1)
                throw new Exception();
            return entities.First();
        }
        private ISqlQuery<TEntity> FetchQuery(object[] keys) => QueryBuilder
            .From<TEntity>()
            .Where(entity => entity.Keys == keys)
            .Project();

        public Task<IList<TEntity>> FetchAll() => FetchEntities(FetchAllQuery);
        private ISqlQuery<TEntity> FetchAllQuery => QueryBuilder
            .From<TEntity>()
            .Project();

        public IEntityUpdate<TEntity> Update(params object[] keys) => QueryBuilder
            .From<TEntity>()
            .Where(entity => entity.Keys == keys)
            .Update();

        public Task Delete(params object[] keys) => DeleteQuery(keys).Execute();
        private ISqlQuery DeleteQuery(object[] keys) => QueryBuilder
            .From<TEntity>()
            .Where(entity => entity.Keys == keys)
            .Delete();
    }
}
