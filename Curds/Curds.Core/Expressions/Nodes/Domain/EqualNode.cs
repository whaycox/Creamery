using System.Linq.Expressions;

namespace Curds.Expressions.Nodes.Domain
{
    using Expressions.Abstraction;

    public class EqualNode : BaseBinaryExpressionNode
    {
        public EqualNode(IExpressionNodeFactory nodeFactory, BinaryExpression binaryExpression)
            : base(nodeFactory, binaryExpression)
        { }

        public override void AcceptVisitor(IExpressionVisitor visitor) => visitor.VisitEqual(this);
    }
}
