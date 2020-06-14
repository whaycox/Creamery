using System.Linq.Expressions;

namespace Curds.Persistence.ExpressionNodes.Domain
{
    using Persistence.Abstraction;

    public class MemberAccessNode : BaseExpressionNode<MemberExpression>
    {
        public IExpressionNode Expression { get; }

        public MemberAccessNode(IExpressionNodeFactory nodeFactory, MemberExpression memberExpression)
            : base(memberExpression)
        {
            Expression = nodeFactory.Build(memberExpression.Expression);
        }

        public override TReturn AcceptVisitor<TReturn>(IExpressionVisitor<TReturn> visitor) => visitor.VisitMemberAccess(this);
    }
}
