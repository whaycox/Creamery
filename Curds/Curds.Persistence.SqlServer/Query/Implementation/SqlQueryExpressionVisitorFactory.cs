namespace Curds.Persistence.Query.Implementation
{
    using Abstraction;
    using Persistence.Abstraction;

    internal class SqlQueryExpressionVisitorFactory : ISqlQueryExpressionVisitorFactory
    {
        private ISqlQueryTokenFactory TokenFactory { get; }

        public SqlQueryExpressionVisitorFactory(ISqlQueryTokenFactory tokenFactory)
        {
            TokenFactory = tokenFactory;
        }

        public ISqlTableVisitor<TModel> TableVisitor<TModel>(ISqlQueryContext<TModel> queryContext)
            where TModel : IDataModel => new TableExpressionVisitor<TModel>(queryContext);

        public ISqlQueryTokenVisitor<TModel> TokenVisitor<TModel>(ISqlQueryContext<TModel> queryContext)
            where TModel : IDataModel => new TokenExpressionVisitor<TModel>(TokenFactory, queryContext);
    }
}
