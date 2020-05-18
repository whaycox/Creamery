using System.Linq.Expressions;

namespace Curds.Persistence.ExpressionNodes.Implementation
{
    using Persistence.Abstraction;

    internal class EqualNode<TReturn, TContext> : BaseBinaryExpressionNode<TReturn, TContext>
    {
        public EqualNode(IExpressionNodeFactory nodeFactory, BinaryExpression binaryExpression)
            : base(nodeFactory, binaryExpression)
        { }

        public override TReturn AcceptVisitor(IExpressionVisitor<TReturn, TContext> visitor, TContext context) => visitor.VisitEqual(context, this);
    }
}
