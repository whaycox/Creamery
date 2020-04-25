using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Curds.Persistence.Query.Implementation
{
    using Abstraction;
    using Curds.Persistence.Domain;
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
            where TEntity : BaseEntity
        {
            InsertQuery<TEntity> query = new InsertQuery<TEntity>();
            query.Table = ModelMap.Table(typeof(TEntity));
            query.AssignIdentityDelegate = ModelMap.AssignIdentityDelegate<TEntity>();
            foreach (TEntity entity in entities)
                query.Entities.Add(ModelMap.ValueEntity(entity));
            return query;
        }

        public ISqlUniverse<TEntity> From<TEntity>() 
            where TEntity : BaseEntity
        {
            SqlUniverse<TEntity> universe = new SqlUniverse<TEntity>();
            universe.Table = ModelMap.Table(typeof(TEntity));
            return universe;
        }
    }
}
