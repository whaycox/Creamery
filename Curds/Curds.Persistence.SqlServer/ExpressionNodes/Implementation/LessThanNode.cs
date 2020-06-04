using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace Curds.Persistence.ExpressionNodes.Implementation
{
    using Persistence.Abstraction;
    using Abstraction;

    internal class LessThanNode<TReturn> : BaseBinaryExpressionNode<TReturn>
    {
        public LessThanNode(IExpressionNodeFactory nodeFactory, BinaryExpression lessThanExpression)
            : base(nodeFactory, lessThanExpression)
        { }

        public override TReturn AcceptVisitor(IExpressionVisitor<TReturn> visitor) => visitor.VisitLessThan(this);
    }
}
