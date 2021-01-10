using System.Linq.Expressions;

namespace Curds.Expressions.Nodes.Domain
{
    using Expressions.Abstraction;

    public class NotEqualNode : BaseBinaryExpressionNode
    {
        public NotEqualNode(IExpressionNodeFactory nodeFactory, BinaryExpression binaryExpression)
            : base(nodeFactory, binaryExpression)
        { }

        public override void AcceptVisitor(IExpressionVisitor visitor) => visitor.VisitNotEqual(this);
    }
}
