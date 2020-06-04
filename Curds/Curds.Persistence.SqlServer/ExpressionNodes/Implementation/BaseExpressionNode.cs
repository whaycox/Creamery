using System.Linq.Expressions;

namespace Curds.Persistence.ExpressionNodes.Implementation
{
    using Persistence.Abstraction;

    internal abstract class BaseExpressionNode<TExpression, TReturn> : IExpressionNode<TReturn>
        where TExpression : Expression
    {
        public TExpression SourceExpression { get; }

        public BaseExpressionNode(IExpressionNodeFactory nodeFactory, TExpression sourceExpression)
        {
            SourceExpression = sourceExpression;
        }

        public abstract TReturn AcceptVisitor(IExpressionVisitor<TReturn> visitor);
    }
}
