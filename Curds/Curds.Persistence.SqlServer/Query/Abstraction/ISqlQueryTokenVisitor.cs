namespace Curds.Persistence.Query.Abstraction
{
    using Expressions.Abstraction;
    using Persistence.Abstraction;

    internal interface ISqlQueryTokenVisitor<TModel> : IExpressionVisitor<ISqlQueryToken>
        where TModel : IDataModel
    { }
}
