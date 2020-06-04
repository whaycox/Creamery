using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace Curds.Persistence.ExpressionNodes.Implementation
{
    using Persistence.Abstraction;

    internal class ConvertNode<TReturn> : BaseExpressionNode<UnaryExpression, TReturn>
    {
        public IExpressionNode<TReturn> Operand { get; }

        public ConvertNode(IExpressionNodeFactory nodeFactory, UnaryExpression unaryExpression)
            : base(nodeFactory, unaryExpression)
        {
            Operand = nodeFactory.Build<TReturn>(unaryExpression.Operand);
        }

        public override TReturn AcceptVisitor(IExpressionVisitor<TReturn> visitor) => visitor.VisitConvert(this);
    }
}
