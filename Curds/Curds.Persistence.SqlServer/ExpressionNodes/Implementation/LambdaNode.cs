using System.Linq.Expressions;

namespace Curds.Persistence.ExpressionNodes.Implementation
{
    using Persistence.Abstraction;

    internal class LambdaNode<TReturn> : BaseExpressionNode<LambdaExpression, TReturn>
    {
        public IExpressionNode<TReturn> Body { get; }

        public LambdaNode(IExpressionNodeFactory nodeFactory, LambdaExpression lambdaExpression)
            : base(nodeFactory, lambdaExpression)
        {
            Body = nodeFactory.Build<TReturn>(SourceExpression.Body);
        }

        public override TReturn AcceptVisitor(IExpressionVisitor<TReturn> visitor) => visitor.VisitLambda(this);
    }
}
