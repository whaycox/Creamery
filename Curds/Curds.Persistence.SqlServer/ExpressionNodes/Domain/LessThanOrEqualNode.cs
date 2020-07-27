using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace Curds.Persistence.ExpressionNodes.Domain
{
    using Persistence.Abstraction;

    public class LessThanOrEqualNode : BaseBinaryExpressionNode
    {
        public LessThanOrEqualNode(IExpressionNodeFactory nodeFactory, BinaryExpression lessThanExpression)
            : base(nodeFactory, lessThanExpression)
        { }

        public override TReturn AcceptVisitor<TReturn>(IExpressionVisitor<TReturn> visitor) => visitor.VisitLessThanOrEqual(this);
    }
}
