using System;
using System.Linq.Expressions;

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
            where TEntity : BaseEntity
        {
            InsertQuery<TEntity> query = QueryExpressionParser.Parse(tableExpression);
            query.Entity = ModelMap.ValueEntity(entity);
            return query;
        }
    }
}
