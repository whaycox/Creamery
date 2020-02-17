using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Curds.Persistence.Query.Implementation
{
    using Abstraction;
    using Model.Abstraction;
    using Persistence.Abstraction;
    using Persistence.Domain;

    internal class SqlQueryBuilder<TModel> : ISqlQueryBuilder<TModel>
        where TModel : IDataModel
    {
        private IModelMap<TModel> ModelMap { get; }
        private ISqlQueryExpressionParser<TModel> QueryExpressionParser { get; }

        public SqlQueryBuilder(
            IModelMap<TModel> modelMap,
            ISqlQueryExpressionParser<TModel> queryExpressionParser)
        {
            ModelMap = modelMap;
            QueryExpressionParser = queryExpressionParser;
        }

        public ISqlQuery Insert<TEntity>(Expression<Func<TModel, ITable<TEntity>>> tableExpression, IEnumerable<TEntity> entities) 
            where TEntity : BaseEntity
        {
            InsertQuery<TEntity> query = QueryExpressionParser.Parse(tableExpression);
            query.AssignIdentityDelegate = ModelMap.AssignIdentityDelegate<TEntity>();
            foreach (TEntity entity in entities)
                query.Entities.Add(ModelMap.ValueEntity(entity));
            return query;
        }
    }
}
