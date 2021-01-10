using System.Linq.Expressions;

namespace Curds.Expressions.Nodes.Domain
{
    using Expressions.Abstraction;

    public class ConstantNode : BaseExpressionNode<ConstantExpression>
    {
        public ConstantNode(ConstantExpression constantExpression)
            : base(constantExpression)
        { }

        public override void AcceptVisitor(IExpressionVisitor visitor) => visitor.VisitConstant(this);
    }
}
