using System.Linq.Expressions;

namespace Curds.Persistence.ExpressionNodes.Implementation
{
    using Persistence.Abstraction;

    internal class EqualNode<TReturn> : BaseBinaryExpressionNode<TReturn>
    {
        public EqualNode(IExpressionNodeFactory nodeFactory, BinaryExpression binaryExpression)
            : base(nodeFactory, binaryExpression)
        { }

        public override TReturn AcceptVisitor(IExpressionVisitor<TReturn> visitor) => visitor.VisitEqual(this);
    }
}
