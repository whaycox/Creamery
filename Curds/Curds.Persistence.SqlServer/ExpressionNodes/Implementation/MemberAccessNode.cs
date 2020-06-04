using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace Curds.Persistence.ExpressionNodes.Implementation
{
    using Persistence.Abstraction;

    internal class MemberAccessNode<TReturn> : BaseExpressionNode<MemberExpression, TReturn>
    {
        public IExpressionNode<TReturn> Expression { get; }

        public MemberAccessNode(IExpressionNodeFactory nodeFactory, MemberExpression memberExpression)
            : base(nodeFactory, memberExpression)
        {
            Expression = nodeFactory.Build<TReturn>(memberExpression.Expression);
        }

        public override TReturn AcceptVisitor(IExpressionVisitor<TReturn> visitor) => visitor.VisitMemberAccess(this);
    }
}
