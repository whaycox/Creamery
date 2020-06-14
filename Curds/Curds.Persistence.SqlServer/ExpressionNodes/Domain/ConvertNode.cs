using System.Linq.Expressions;

namespace Curds.Persistence.ExpressionNodes.Domain
{
    using Persistence.Abstraction;

    public class ConvertNode : BaseExpressionNode<UnaryExpression>
    {
        public IExpressionNode Operand { get; }

        public ConvertNode(IExpressionNodeFactory nodeFactory, UnaryExpression unaryExpression)
            : base(unaryExpression)
        {
            Operand = nodeFactory.Build(unaryExpression.Operand);
        }

        public override TReturn AcceptVisitor<TReturn>(IExpressionVisitor<TReturn> visitor) => visitor.VisitConvert(this);
    }
}
