namespace Curds.Persistence.Abstraction
{
    public interface IExpressionNode
    {
        TReturn AcceptVisitor<TReturn>(IExpressionVisitor<TReturn> visitor);
    }
}
