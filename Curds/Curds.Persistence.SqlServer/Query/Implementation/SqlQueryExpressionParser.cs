using System;
using System.Linq.Expressions;

namespace Curds.Persistence.Query.Implementation
{
    using Abstraction;
    using Domain;
    using Model.Abstraction;
    using Persistence.Abstraction;
    using Persistence.Domain;
    using Model.Domain;

    internal class SqlQueryExpressionParser<TModel> : ISqlQueryExpressionParser<TModel>
        where TModel : IDataModel
    {
        private IModelMap<TModel> ModelMap { get; }

        public SqlQueryExpressionParser(IModelMap<TModel> modelMap)
        {
            ModelMap = modelMap;
        }

        public InsertQuery<TEntity> Parse<TEntity>(Expression<Func<TModel, ITable<TEntity>>> expression)
            where TEntity : BaseEntity
        {
            Table table = ParseTableExpression(expression.Body);
            return new InsertQuery<TEntity> { Table = table, };
        }
        private Table ParseTableExpression(Expression expression)
        {
            Type entityType = null;
            switch (expression.NodeType)
            {
                case ExpressionType.MemberAccess:
                    MemberExpression memberExpression = (MemberExpression)expression;
                    entityType = ParseTableEntityType(memberExpression.Type);
                    break;
                case ExpressionType.Call:
                    MethodCallExpression methodCallExpression = (MethodCallExpression)expression;
                    if (methodCallExpression.Method.Name != nameof(IDataModel.Table))
                        throw new FormatException($"Unsupported method name: {methodCallExpression.Method.Name}");
                    entityType = ParseTableEntityType(methodCallExpression.Method.ReturnType);
                    break;
                default:
                    throw new InvalidOperationException($"Unsupported {nameof(expression.NodeType)}: {expression.NodeType}");
            }
            return ModelMap.Table(entityType);
        }
        private Type ParseTableEntityType(Type tableType) => tableType.GenericTypeArguments[0];
    }
}
