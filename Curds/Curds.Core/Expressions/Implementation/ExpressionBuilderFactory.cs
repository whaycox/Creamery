namespace Curds.Expressions.Implementation
{
    using Abstraction;

    internal class ExpressionBuilderFactory : IExpressionBuilderFactory
    {
        public IExpressionBuilder Create() => new ExpressionBuilder();
    }
}
