using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace Curds.Persistence.Implementation
{
    using Abstraction;
    using Domain;
    using ExpressionNodes.Implementation;

    internal class ExpressionNodeFactory : IExpressionNodeFactory
    {
        public IExpressionNode<TReturn, TContext> Build<TReturn, TContext>(Expression expression)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.Lambda:
                    return new LambdaNode<TReturn, TContext>(this, (LambdaExpression)expression);
                case ExpressionType.Equal:
                    return new EqualNode<TReturn, TContext>(this, (BinaryExpression)expression);
                case ExpressionType.LessThan:
                    return new LessThanNode<TReturn, TContext>(this, (BinaryExpression)expression);
                case ExpressionType.LessThanOrEqual:
                    return new LessThanOrEqualNode<TReturn, TContext>(this, (BinaryExpression)expression);
                case ExpressionType.MemberAccess:
                    return new MemberAccessNode<TReturn, TContext>(this, (MemberExpression)expression);
                case ExpressionType.Convert:
                    return new ConvertNode<TReturn, TContext>(this, (UnaryExpression)expression);
                case ExpressionType.Parameter:
                    return new ParameterNode<TReturn, TContext>(this, (ParameterExpression)expression);
                case ExpressionType.Constant:
                    return new ConstantNode<TReturn, TContext>(this, (ConstantExpression)expression);
                case ExpressionType.Modulo:
                    return new ModuloNode<TReturn, TContext>(this, (BinaryExpression)expression);
                default:
                    throw new InvalidExpressionException(expression, $"Unsupported node: {expression.NodeType}");
            }
        }
    }
}
