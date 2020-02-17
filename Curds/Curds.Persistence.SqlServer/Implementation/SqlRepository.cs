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

    public class SqlRepository<TModel, TEntity> : IRepository<TEntity>
        where TModel : IDataModel
        where TEntity : BaseEntity
    {
        protected ISqlConnectionContext ConnectionContext { get; }
        protected IModelMap<TModel> ModelMap { get; }
        protected ISqlQueryBuilder<TModel> QueryBuilder { get; }

        public SqlRepository(
            ISqlConnectionContext connectionContext,
            IModelMap<TModel> modelMap,
            ISqlQueryBuilder<TModel> queryBuilder)
        {
            ConnectionContext = connectionContext;
            QueryBuilder = queryBuilder;
        }

        public Task Insert(TEntity entity) => Insert(new List<TEntity> { entity });

        public async Task Insert(IEnumerable<TEntity> entities)
        {
            ISqlQuery insertQuery = QueryBuilder.Insert(model => model.Table<TEntity>(), entities);
            using (ISqlQueryReader reader = await ConnectionContext.ExecuteWithResult(insertQuery))
                insertQuery.ProcessResult(reader);
        }

        public Task<TEntity> Fetch(params object[] keys) => throw new NotImplementedException();

        public Task<List<TEntity>> FetchAll() => throw new NotImplementedException();

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
