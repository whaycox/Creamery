using System.Linq.Expressions;

namespace Curds.Persistence.ExpressionNodes.Implementation
{
    using Abstraction;

    internal abstract class BaseBinaryExpressionNode<TReturn, TContext> : BaseExpressionNode<BinaryExpression, TReturn, TContext>
    {
        public IExpressionNode<TReturn, TContext> Left { get; }
        public IExpressionNode<TReturn, TContext> Right { get; }

        public BaseBinaryExpressionNode(IExpressionNodeFactory nodeFactory, BinaryExpression binaryExpression)
            : base(nodeFactory, binaryExpression)
        {
            Left = nodeFactory.Build<TReturn, TContext>(binaryExpression.Left);
            Right = nodeFactory.Build<TReturn, TContext>(binaryExpression.Right);
        }
    }
}
