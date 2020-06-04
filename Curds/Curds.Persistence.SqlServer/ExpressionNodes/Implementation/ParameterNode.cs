using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace Curds.Persistence.ExpressionNodes.Implementation
{
    using Persistence.Abstraction;

    internal class ParameterNode<TReturn> : BaseExpressionNode<ParameterExpression, TReturn>
    {
        public ParameterNode(IExpressionNodeFactory nodeFactory, ParameterExpression parameterExpression)
            : base(nodeFactory, parameterExpression)
        { }

        public override TReturn AcceptVisitor(IExpressionVisitor<TReturn> visitor) => visitor.VisitParameter(this);
    }
}
