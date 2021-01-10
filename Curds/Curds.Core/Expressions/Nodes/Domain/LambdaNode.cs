using System.Linq.Expressions;

namespace Curds.Expressions.Nodes.Domain
{
    using Expressions.Abstraction;

    public class LambdaNode : BaseExpressionNode<LambdaExpression>
    {
        public IExpressionNode Body { get; }

        public LambdaNode(IExpressionNodeFactory nodeFactory, LambdaExpression lambdaExpression)
            : base(lambdaExpression)
        {
            Body = nodeFactory.Build(SourceExpression.Body);
        }

        public override void AcceptVisitor(IExpressionVisitor visitor) => visitor.VisitLambda(this);
    }
}
