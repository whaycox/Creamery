﻿using System;
using System.Linq.Expressions;

namespace Curds.Persistence.Implementation
{
    using Abstraction;
    using Curds.Persistence.Domain;
    using Domain;
    using Model.Abstraction;

    internal class ExpressionParser : IExpressionParser
    {
        public Type ParseModelEntitySelection<TModel>(Expression<Func<TModel, IEntity>> modelEntitySelectionExpression)
            where TModel : IDataModel
        {
            if (modelEntitySelectionExpression.NodeType != ExpressionType.Lambda)
                throw InvalidModelEntitySelectionExpression(modelEntitySelectionExpression);

            Expression lambdaBody = modelEntitySelectionExpression.Body;
            switch (lambdaBody.NodeType)
            {
                case ExpressionType.MemberAccess:
                    MemberExpression memberExpression = (MemberExpression)lambdaBody;
                    return memberExpression.Type;
                case ExpressionType.Call:
                    MethodCallExpression methodCallExpression = (MethodCallExpression)lambdaBody;
                    if (methodCallExpression.Method.Name != nameof(IDataModel.Entity))
                        throw new FormatException($"Unsupported method name: {methodCallExpression.Method.Name}");
                    return methodCallExpression.Method.ReturnType;
                default:
                    throw InvalidModelEntitySelectionExpression(modelEntitySelectionExpression);
            }
        }
        private InvalidExpressionException InvalidModelEntitySelectionExpression<TModel>(Expression<Func<TModel, IEntity>> modelEntitySelectionExpression)
            where TModel : IDataModel => new InvalidExpressionException(modelEntitySelectionExpression);

        public string ParseEntityValueSelection<TEntity, TValue>(Expression<Func<TEntity, TValue>> entityValueSelectionExpression)
            where TEntity : IEntity
        {
            if (entityValueSelectionExpression.NodeType != ExpressionType.Lambda ||
                entityValueSelectionExpression.Body.NodeType != ExpressionType.MemberAccess)
                throw InvalidEntityValueSelectionExpression(entityValueSelectionExpression);

            MemberExpression memberAccessExpression = (MemberExpression)entityValueSelectionExpression.Body;
            if (memberAccessExpression.Expression.NodeType != ExpressionType.Parameter)
                throw InvalidEntityValueSelectionExpression(entityValueSelectionExpression);
            return memberAccessExpression.Member.Name;
        }
        private FormatException InvalidEntityValueSelectionExpression<TEntity, TValue>(Expression<Func<TEntity, TValue>> entityValueSelectionExpression)
            where TEntity : IEntity => new FormatException($"Invalid entity value selection: {entityValueSelectionExpression}");
    }
}
