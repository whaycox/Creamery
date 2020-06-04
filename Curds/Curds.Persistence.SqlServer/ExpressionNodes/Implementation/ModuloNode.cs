using System.Linq.Expressions;

namespace Curds.Persistence.ExpressionNodes.Implementation
{
    using Persistence.Abstraction;

    internal class ModuloNode<TReturn> : BaseBinaryExpressionNode<TReturn>
    {
        public ModuloNode(IExpressionNodeFactory nodeFactory, BinaryExpression moduloExpression)
            : base(nodeFactory, moduloExpression)
        { }

        public override TReturn AcceptVisitor(IExpressionVisitor<TReturn> visitor) => visitor.VisitModulo(this);
    }
}
