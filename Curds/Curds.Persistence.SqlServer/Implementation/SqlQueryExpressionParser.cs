using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace Curds.Persistence.Implementation
{
    using Abstraction;
    using Domain;
    using Query.Domain;
    using Query.Implementation;
    using Model.Abstraction;

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
            switch (expression.NodeType)
            {
                case ExpressionType.MemberAccess:
                    MemberExpression memberExpression = (MemberExpression)expression;
                    return ModelMap.Table(memberExpression.Member.Name);
                case ExpressionType.Call:
                    MethodCallExpression methodCallExpression = (MethodCallExpression)expression;
                    if (methodCallExpression.Method.Name != nameof(IDataModel.Table))
                        throw new FormatException($"Unsupported method name: {methodCallExpression.Method.Name}");
                    return ModelMap.Table(ParseTableEntityType(methodCallExpression.Method.ReturnType));
                default:
                    throw new InvalidOperationException($"Unsupported {nameof(expression.NodeType)}: {expression.NodeType}");
            }
        }
        private Type ParseTableEntityType(Type tableType) => tableType.GenericTypeArguments[0];
    }
}
