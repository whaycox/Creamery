using System.Linq.Expressions;

namespace Curds.Expressions.Nodes.Domain
{
    using Expressions.Abstraction;

    public abstract class BaseExpressionNode<TExpression> : IExpressionNode
        where TExpression : Expression
    {
        public TExpression SourceExpression { get; }

        public BaseExpressionNode(TExpression sourceExpression)
        {
            SourceExpression = sourceExpression;
        }

        public abstract void AcceptVisitor(IExpressionVisitor visitor);
    }
}
