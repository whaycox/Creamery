using System.Collections.Generic;
using System.Threading.Tasks;

namespace Curds.Persistence.Implementation
{
    using Abstraction;
    using Domain;
    using Query.Abstraction;

    public class SqlRepository<TDataModel, TEntity> : BaseSqlRepository, IRepository<TDataModel, TEntity>
        where TDataModel : IDataModel
        where TEntity : class, IEntity
    {
        protected ISqlQueryBuilder<TDataModel> QueryBuilder { get; }

        public SqlRepository(ISqlQueryBuilder<TDataModel> queryBuilder)
        {
            QueryBuilder = queryBuilder;
        }

        public Task Insert(TEntity entity) => Insert(new List<TEntity> { entity });

        public Task Insert(IEnumerable<TEntity> entities) => InsertQuery(entities).Execute();
        private ISqlQuery InsertQuery(IEnumerable<TEntity> entities) => QueryBuilder.Insert(entities);

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
