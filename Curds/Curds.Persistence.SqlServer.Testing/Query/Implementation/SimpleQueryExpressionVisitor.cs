namespace Curds.Persistence.Query.Implementation
{
    using Abstraction;
    using Persistence.Abstraction;

    internal class SimpleQueryExpressionVisitor : BaseQueryExpressionVisitor<ITestDataModel, object>
    {
        public SimpleQueryExpressionVisitor(ISqlQueryContext<ITestDataModel> queryContext)
            : base(queryContext)
        { }
    }
}
