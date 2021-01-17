using System.Linq.Expressions;

namespace Curds.Expressions.Nodes.Domain
{
    using Expressions.Abstraction;

    public class LessThanOrEqualNode : BaseBinaryExpressionNode
    {
        public LessThanOrEqualNode(IExpressionNodeFactory nodeFactory, BinaryExpression lessThanOrEqualExpression)
            : base(nodeFactory, lessThanOrEqualExpression)
        { }

        public override void AcceptVisitor(IExpressionVisitor visitor) => visitor.VisitLessThanOrEqual(this);
    }
}
