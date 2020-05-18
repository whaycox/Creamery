namespace Curds.Persistence.Abstraction
{
    internal interface IExpressionNode<TReturn, TContext>
    {
        TReturn AcceptVisitor(IExpressionVisitor<TReturn, TContext> visitor, TContext context);
    }
}
