using System.Linq.Expressions;

namespace Curds.Persistence.ExpressionNodes.Implementation
{
    using Persistence.Abstraction;

    internal class LambdaNode<TReturn, TContext> : BaseExpressionNode<LambdaExpression, TReturn, TContext>
    {
        public IExpressionNode<TReturn, TContext> Body { get; }

        public LambdaNode(IExpressionNodeFactory nodeFactory, LambdaExpression lambdaExpression)
            : base(nodeFactory, lambdaExpression)
        {
            Body = nodeFactory.Build<TReturn, TContext>(SourceExpression.Body);
        }

        public override TReturn AcceptVisitor(IExpressionVisitor<TReturn, TContext> visitor, TContext context) => visitor.VisitLambda(context, this);
    }
}
