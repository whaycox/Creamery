using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace Curds.Persistence.Implementation
{
    using Abstraction;
    using Domain;
    using ExpressionNodes.Domain;

    internal class ExpressionNodeFactory : IExpressionNodeFactory
    {
        public IExpressionNode Build(Expression expression)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.Lambda:
                    return new LambdaNode(this, (LambdaExpression)expression);
                case ExpressionType.Equal:
                    return new EqualNode(this, (BinaryExpression)expression);
                case ExpressionType.LessThan:
                    return new LessThanNode(this, (BinaryExpression)expression);
                case ExpressionType.LessThanOrEqual:
                    return new LessThanOrEqualNode(this, (BinaryExpression)expression);
                case ExpressionType.MemberAccess:
                    return new MemberAccessNode(this, (MemberExpression)expression);
                case ExpressionType.Convert:
                    return new ConvertNode(this, (UnaryExpression)expression);
                case ExpressionType.Parameter:
                    return new ParameterNode((ParameterExpression)expression);
                case ExpressionType.Constant:
                    return new ConstantNode((ConstantExpression)expression);
                case ExpressionType.Modulo:
                    return new ModuloNode(this, (BinaryExpression)expression);
                default:
                    throw new InvalidExpressionException(expression, $"Unsupported node: {expression.NodeType}");
            }
        }
    }
}
