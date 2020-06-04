using System.Linq.Expressions;

namespace Curds.Persistence.ExpressionNodes.Implementation
{
    using Abstraction;

    internal abstract class BaseBinaryExpressionNode<TReturn> : BaseExpressionNode<BinaryExpression, TReturn>
    {
        public IExpressionNode<TReturn> Left { get; }
        public IExpressionNode<TReturn> Right { get; }

        public BaseBinaryExpressionNode(IExpressionNodeFactory nodeFactory, BinaryExpression binaryExpression)
            : base(nodeFactory, binaryExpression)
        {
            Left = nodeFactory.Build<TReturn>(binaryExpression.Left);
            Right = nodeFactory.Build<TReturn>(binaryExpression.Right);
        }
    }
}
