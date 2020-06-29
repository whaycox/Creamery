using System.Linq;

namespace Curds.Persistence.Query.Implementation
{
    using Abstraction;
    using ExpressionNodes.Domain;
    using Persistence.Abstraction;

    internal class TableExpressionVisitor<TModel> : BaseQueryExpressionVisitor<TModel, ISqlTable>, ISqlTableVisitor<TModel>
        where TModel : IDataModel
    {
        public TableExpressionVisitor(ISqlQueryContext<TModel> queryContext)
            : base(queryContext)
        { }

        public override ISqlTable VisitParameter(ParameterNode parameterNode) =>
            Context.Tables.First(table => table.EntityType == parameterNode.SourceExpression.Type);
    }
}
