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
        public IExpressionNode<TReturn> Build<TReturn>(Expression expression)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.Lambda:
                    return new LambdaNode<TReturn>(this, (LambdaExpression)expression);
                case ExpressionType.Equal:
                    return new EqualNode<TReturn>(this, (BinaryExpression)expression);
                case ExpressionType.LessThan:
                    return new LessThanNode<TReturn>(this, (BinaryExpression)expression);
                case ExpressionType.LessThanOrEqual:
                    return new LessThanOrEqualNode<TReturn>(this, (BinaryExpression)expression);
                case ExpressionType.MemberAccess:
                    return new MemberAccessNode<TReturn>(this, (MemberExpression)expression);
                case ExpressionType.Convert:
                    return new ConvertNode<TReturn>(this, (UnaryExpression)expression);
                case ExpressionType.Parameter:
                    return new ParameterNode<TReturn>(this, (ParameterExpression)expression);
                case ExpressionType.Constant:
                    return new ConstantNode<TReturn>(this, (ConstantExpression)expression);
                case ExpressionType.Modulo:
                    return new ModuloNode<TReturn>(this, (BinaryExpression)expression);
                default:
                    throw new InvalidExpressionException(expression, $"Unsupported node: {expression.NodeType}");
            }
        }
    }
}
