using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace Curds.Persistence.ExpressionNodes.Implementation
{
    using Persistence.Abstraction;
    using Abstraction;

    internal class LessThanNode<TReturn, TContext> : BaseBinaryExpressionNode<TReturn, TContext>
    {
        public LessThanNode(IExpressionNodeFactory nodeFactory, BinaryExpression lessThanExpression)
            : base(nodeFactory, lessThanExpression)
        { }

        public override TReturn AcceptVisitor(IExpressionVisitor<TReturn, TContext> visitor, TContext context) => visitor.VisitLessThan(context, this);
    }
}
