using System.Linq.Expressions;

namespace Curds.Persistence.ExpressionNodes.Implementation
{
    using Persistence.Abstraction;

    internal class ModuloNode<TReturn, TContext> : BaseBinaryExpressionNode<TReturn, TContext>
    {
        public ModuloNode(IExpressionNodeFactory nodeFactory, BinaryExpression moduloExpression)
            : base(nodeFactory, moduloExpression)
        { }

        public override TReturn AcceptVisitor(IExpressionVisitor<TReturn, TContext> visitor, TContext context) => visitor.VisitModulo(context, this);
    }
}
