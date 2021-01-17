using System.Linq.Expressions;

namespace Curds.Expressions.Nodes.Domain
{
    using Expressions.Abstraction;

    public class ParameterNode : BaseExpressionNode<ParameterExpression>
    {
        public ParameterNode(ParameterExpression parameterExpression)
            : base(parameterExpression)
        { }

        public override void AcceptVisitor(IExpressionVisitor visitor) => visitor.VisitParameter(this);
    }
}
