using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace Curds.Persistence.ExpressionNodes.Implementation
{
    using Persistence.Abstraction;

    internal class MemberAccessNode<TReturn, TContext> : BaseExpressionNode<MemberExpression, TReturn, TContext>
    {
        public IExpressionNode<TReturn, TContext> Expression { get; }

        public MemberAccessNode(IExpressionNodeFactory nodeFactory, MemberExpression memberExpression)
            : base(nodeFactory, memberExpression)
        {
            Expression = nodeFactory.Build<TReturn, TContext>(memberExpression.Expression);
        }

        public override TReturn AcceptVisitor(IExpressionVisitor<TReturn, TContext> visitor, TContext context) => visitor.VisitMemberAccess(context, this);
    }
}
