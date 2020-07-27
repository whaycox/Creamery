using System.Linq.Expressions;

namespace Curds.Persistence.ExpressionNodes.Domain
{
    using Persistence.Abstraction;

    public class ParameterNode : BaseExpressionNode<ParameterExpression>
    {
        public ParameterNode(ParameterExpression parameterExpression)
            : base(parameterExpression)
        { }

        public override TReturn AcceptVisitor<TReturn>(IExpressionVisitor<TReturn> visitor) => visitor.VisitParameter(this);
    }
}
