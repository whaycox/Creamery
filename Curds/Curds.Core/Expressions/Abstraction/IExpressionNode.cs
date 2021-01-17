namespace Curds.Expressions.Abstraction
{
    public interface IExpressionNode
    {
        void AcceptVisitor(IExpressionVisitor expressionVisitor);
    }
}
