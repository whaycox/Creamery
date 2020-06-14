using System.Linq.Expressions;

namespace Curds.Persistence.ExpressionNodes.Domain
{
    using Persistence.Abstraction;

    public class ModuloNode : BaseBinaryExpressionNode
    {
        public ModuloNode(IExpressionNodeFactory nodeFactory, BinaryExpression moduloExpression)
            : base(nodeFactory, moduloExpression)
        { }

        public override TReturn AcceptVisitor<TReturn>(IExpressionVisitor<TReturn> visitor) => visitor.VisitModulo(this);
    }
}
