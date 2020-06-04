using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace Curds.Persistence.ExpressionNodes.Implementation
{
    using Persistence.Abstraction;

    internal class LessThanOrEqualNode<TReturn> : BaseBinaryExpressionNode<TReturn>
    {
        public LessThanOrEqualNode(IExpressionNodeFactory nodeFactory, BinaryExpression lessThanExpression)
            : base(nodeFactory, lessThanExpression)
        { }

        public override TReturn AcceptVisitor(IExpressionVisitor<TReturn> visitor) => visitor.VisitLessThanOrEqual(this);
    }
}
