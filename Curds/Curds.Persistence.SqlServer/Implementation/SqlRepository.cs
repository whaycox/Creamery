using System.Collections.Generic;
using System.Threading.Tasks;

namespace Curds.Persistence.Implementation
{
    using Abstraction;
    using Domain;
    using Query.Abstraction;

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

        protected async Task<TEntity> FetchSingleEntity(ISqlQuery<TEntity> entityQuery)
        {
            List<TEntity> entities = await FetchEntities(entityQuery);
            if (entities.Count == 0)
                throw new QueryResultCountException("No entities found with the supplied keys");
            if (entities.Count != 1)
                throw new QueryResultCountException($"Expected 1 entity but found {entities.Count} instead");
            return entities[0];
        }
        protected async Task<List<TEntity>> FetchEntities(ISqlQuery<TEntity> entityQuery)
        {
            await entityQuery.Execute();
            return entityQuery.Results;
        }

        public Task<TEntity> Fetch(params object[] keys) => FetchSingleEntity(FetchQuery(keys));
        private ISqlQuery<TEntity> FetchQuery(object[] keys) => QueryBuilder
            .From<TEntity>()
            .Where(entity => entity.Keys == keys)
            .Project();

        public Task<List<TEntity>> FetchAll() => FetchEntities(FetchAllQuery);
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
