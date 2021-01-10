using System.Linq.Expressions;

namespace Curds.Expressions.Nodes.Domain
{
    using Expressions.Abstraction;

    public abstract class BaseBinaryExpressionNode : BaseExpressionNode<BinaryExpression>
    {
        public IExpressionNode Left { get; }
        public IExpressionNode Right { get; }

        public BaseBinaryExpressionNode(IExpressionNodeFactory nodeFactory, BinaryExpression binaryExpression)
            : base(binaryExpression)
        {
            Left = nodeFactory.Build(binaryExpression.Left);
            Right = nodeFactory.Build(binaryExpression.Right);
        }
    }
}
