using System.Linq.Expressions;

namespace Curds.Expressions.Nodes.Domain
{
    using Expressions.Abstraction;

    public class MemberAccessNode : BaseExpressionNode<MemberExpression>
    {
        public IExpressionNode Expression { get; }

        public MemberAccessNode(IExpressionNodeFactory nodeFactory, MemberExpression memberExpression)
            : base(memberExpression)
        {
            Expression = nodeFactory.Build(memberExpression.Expression);
        }

        public override void AcceptVisitor(IExpressionVisitor visitor) => visitor.VisitMemberAccess(this);
    }
}
