using System.Linq.Expressions;

namespace Curds.Expressions.Nodes.Domain
{
    using Expressions.Abstraction;

    public class GreaterThanOrEqualNode : BaseBinaryExpressionNode
    {
        public GreaterThanOrEqualNode(IExpressionNodeFactory nodeFactory, BinaryExpression greaterThanOrEqualExpression)
            : base(nodeFactory, greaterThanOrEqualExpression)
        { }

        public override void AcceptVisitor(IExpressionVisitor visitor) => visitor.VisitGreaterThanOrEqual(this);
    }
}
