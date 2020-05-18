using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace Curds.Persistence.ExpressionNodes.Implementation
{
    using Persistence.Abstraction;

    internal class ConstantNode<TReturn, TContext> : BaseExpressionNode<ConstantExpression, TReturn, TContext>
    {
        public ConstantNode(IExpressionNodeFactory nodeFactory, ConstantExpression constantExpression)
            : base(nodeFactory, constantExpression)
        { }

        public override TReturn AcceptVisitor(IExpressionVisitor<TReturn, TContext> visitor, TContext context) => visitor.VisitConstant(context, this);
    }
}
