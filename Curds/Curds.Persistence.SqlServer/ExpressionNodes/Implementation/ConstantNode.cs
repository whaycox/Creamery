using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace Curds.Persistence.ExpressionNodes.Implementation
{
    using Persistence.Abstraction;

    internal class ConstantNode<TReturn> : BaseExpressionNode<ConstantExpression, TReturn>
    {
        public ConstantNode(IExpressionNodeFactory nodeFactory, ConstantExpression constantExpression)
            : base(nodeFactory, constantExpression)
        { }

        public override TReturn AcceptVisitor(IExpressionVisitor<TReturn> visitor) => visitor.VisitConstant(this);
    }
}
