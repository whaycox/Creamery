using System.Linq.Expressions;

namespace Curds.Persistence.ExpressionNodes.Domain
{
    using Persistence.Abstraction;

    public class ConstantNode : BaseExpressionNode<ConstantExpression>
    {
        public ConstantNode(ConstantExpression constantExpression)
            : base(constantExpression)
        { }

        public override TReturn AcceptVisitor<TReturn>(IExpressionVisitor<TReturn> visitor) => visitor.VisitConstant(this);
    }
}
