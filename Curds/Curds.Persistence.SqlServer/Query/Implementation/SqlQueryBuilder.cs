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

        public ISqlQuery Insert<TEntity>(Expression<Func<TModel, ITable<TEntity>>> tableExpression, TEntity entity)
            where TEntity : BaseEntity => Insert(tableExpression, new List<TEntity> { entity });

        public ISqlQuery Insert<TEntity>(Expression<Func<TModel, ITable<TEntity>>> tableExpression, IEnumerable<TEntity> entities) 
            where TEntity : BaseEntity
        {
            InsertQuery<TEntity> query = QueryExpressionParser.Parse(tableExpression);
            foreach (TEntity entity in entities)
                query.Entities.Add(ModelMap.ValueEntity(entity));
            return query;
        }
    }
}
