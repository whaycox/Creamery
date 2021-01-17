namespace Curds.Expressions.Implementation
{
    using Abstraction;

    internal class ExpressionBuilderFactory : IExpressionBuilderFactory
    {
        private IExpressionFactory ExpressionFactory { get; }

        public ExpressionBuilderFactory(IExpressionFactory expressionFactory)
        {
            ExpressionFactory = expressionFactory;
        }

        public IExpressionBuilder Create() => new ExpressionBuilder(ExpressionFactory);
    }
}
