using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace Curds.Persistence.Implementation
{
    using Abstraction;
    using Domain;
    using Query.Implementation;
    using Model.Abstraction;

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
            where TEntity : BaseEntity
        {
            InsertQuery<TEntity> query = QueryExpressionParser.Parse(tableExpression);
            query.Entity = ModelMap.ValueEntity(entity);
            return query;
        }
    }
}
