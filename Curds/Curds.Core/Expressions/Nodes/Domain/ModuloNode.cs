using System.Linq.Expressions;

namespace Curds.Expressions.Nodes.Domain
{
    using Expressions.Abstraction;

    public class ModuloNode : BaseBinaryExpressionNode
    {
        public ModuloNode(IExpressionNodeFactory nodeFactory, BinaryExpression moduloExpression)
            : base(nodeFactory, moduloExpression)
        { }

        public override void AcceptVisitor(IExpressionVisitor visitor) => visitor.VisitModulo(this);
    }
}
