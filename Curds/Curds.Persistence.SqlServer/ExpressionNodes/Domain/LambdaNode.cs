using System.Linq.Expressions;

namespace Curds.Persistence.ExpressionNodes.Domain
{
    using Persistence.Abstraction;

    public class LambdaNode : BaseExpressionNode<LambdaExpression>
    {
        public IExpressionNode Body { get; }

        public LambdaNode(IExpressionNodeFactory nodeFactory, LambdaExpression lambdaExpression)
            : base(lambdaExpression)
        {
            Body = nodeFactory.Build(SourceExpression.Body);
        }

        public override TReturn AcceptVisitor<TReturn>(IExpressionVisitor<TReturn> visitor) => visitor.VisitLambda(this);
    }
}
