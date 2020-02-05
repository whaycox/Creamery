using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Curds.Persistence.Implementation
{
    using Domain;
    using Abstraction;

    public class SqlRepository<TModel, TEntity> : IRepository<TEntity>
        where TModel : IDataModel
        where TEntity : BaseEntity
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

        public Task Insert(TEntity entity) => ConnectionContext.Execute(
            QueryBuilder.Insert(model => model.Table<TEntity>(), entity));

        public Task Insert(IEnumerable<TEntity> entities) => throw new NotImplementedException();
        
        public async Task<TEntity> Fetch(params object[] keys) => throw new NotImplementedException();

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
