using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace Curds.Persistence.ExpressionNodes.Implementation
{
    using Persistence.Abstraction;

    internal class ConvertNode<TReturn, TContext> : BaseExpressionNode<UnaryExpression, TReturn, TContext>
    {
        public IExpressionNode<TReturn, TContext> Operand { get; }

        public ConvertNode(IExpressionNodeFactory nodeFactory, UnaryExpression unaryExpression)
            : base(nodeFactory, unaryExpression)
        {
            Operand = nodeFactory.Build<TReturn, TContext>(unaryExpression.Operand);
        }

        public override TReturn AcceptVisitor(IExpressionVisitor<TReturn, TContext> visitor, TContext context) => visitor.VisitConvert(context, this);
    }
}
