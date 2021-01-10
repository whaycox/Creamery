using System.Linq.Expressions;

namespace Curds.Expressions.Nodes.Domain
{
    using Expressions.Abstraction;

    public class LessThanNode : BaseBinaryExpressionNode
    {
        public LessThanNode(IExpressionNodeFactory nodeFactory, BinaryExpression lessThanExpression)
            : base(nodeFactory, lessThanExpression)
        { }

        public override void AcceptVisitor(IExpressionVisitor visitor) => visitor.VisitLessThan(this);
    }
}
