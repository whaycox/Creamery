using System.Collections.Generic;

namespace Curds.Persistence.Query.Implementation
{
    using Abstraction;
    using Model.Abstraction;
    using Persistence.Abstraction;

    internal class SqlQueryBuilder<TModel> : ISqlQueryBuilder<TModel>
        where TModel : IDataModel
    {
        private IModelMap<TModel> ModelMap { get; }

        public SqlQueryBuilder(IModelMap<TModel> modelMap)
        {
            ModelMap = modelMap;
        }

        public ISqlQuery Insert<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : IEntity
        {
            InsertQuery<TEntity> query = new InsertQuery<TEntity>();
            query.Model = ModelMap.Entity<TEntity>();
            query.Entities.AddRange(entities);
            return query;
        }

        public ISqlUniverse<TEntity> From<TEntity>()
            where TEntity : IEntity
        {
            SqlUniverse<TEntity> universe = new SqlUniverse<TEntity>();
            universe.Model = ModelMap.Entity<TEntity>();
            return universe;
        }
    }
}
