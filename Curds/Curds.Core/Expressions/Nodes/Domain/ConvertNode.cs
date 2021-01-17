using System.Linq.Expressions;

namespace Curds.Expressions.Nodes.Domain
{
    using Expressions.Abstraction;

    public class ConvertNode : BaseExpressionNode<UnaryExpression>
    {
        public IExpressionNode Operand { get; }

        public ConvertNode(IExpressionNodeFactory nodeFactory, UnaryExpression unaryExpression)
            : base(unaryExpression)
        {
            Operand = nodeFactory.Build(unaryExpression.Operand);
        }

        public override void AcceptVisitor(IExpressionVisitor visitor) => visitor.VisitConvert(this);
    }
}
