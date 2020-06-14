using System.Linq.Expressions;

namespace Curds.Persistence.ExpressionNodes.Domain
{
    using Persistence.Abstraction;

    public abstract class BaseExpressionNode<TExpression> : IExpressionNode
        where TExpression : Expression
    {
        public TExpression SourceExpression { get; }

        public BaseExpressionNode(TExpression sourceExpression)
        {
            SourceExpression = sourceExpression;
        }

        public abstract TReturn AcceptVisitor<TReturn>(IExpressionVisitor<TReturn> visitor);
    }
}
