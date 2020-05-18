using System.Linq.Expressions;

namespace Curds.Persistence.ExpressionNodes.Implementation
{
    using Persistence.Abstraction;

    internal abstract class BaseExpressionNode<TExpression, TReturn, TContext> : IExpressionNode<TReturn, TContext>
        where TExpression : Expression
    {
        public TExpression SourceExpression { get; }

        public BaseExpressionNode(IExpressionNodeFactory nodeFactory, TExpression sourceExpression)
        {
            SourceExpression = sourceExpression;
        }

        public abstract TReturn AcceptVisitor(IExpressionVisitor<TReturn, TContext> visitor, TContext context);
    }
}
