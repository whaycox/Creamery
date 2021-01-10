using System.Linq.Expressions;

namespace Curds.Expressions.Nodes.Domain
{
    using Expressions.Abstraction;

    public class GreaterThanNode : BaseBinaryExpressionNode
    {
        public GreaterThanNode(IExpressionNodeFactory nodeFactory, BinaryExpression greaterThanExpression)
            : base(nodeFactory, greaterThanExpression)
        { }

        public override void AcceptVisitor(IExpressionVisitor visitor) => visitor.VisitGreaterThan(this);
    }
}
