namespace Curds.Persistence.Query.Implementation
{
    using Abstraction;
    using Persistence.Abstraction;
    using Persistence.Implementation;
    using Expressions.Implementation;

    internal abstract class BaseQueryExpressionVisitor<TModel, TReturn> : BaseExpressionVisitor<TReturn>
        where TModel : IDataModel
    {
        public ISqlQueryContext<TModel> Context { get; }

        public BaseQueryExpressionVisitor(ISqlQueryContext<TModel> context)
        {
            Context = context;
        }
    }
}
