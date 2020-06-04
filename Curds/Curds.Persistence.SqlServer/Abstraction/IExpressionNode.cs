namespace Curds.Persistence.Abstraction
{
    internal interface IExpressionNode<TReturn>
    {
        TReturn AcceptVisitor(IExpressionVisitor<TReturn> visitor);
    }
}
