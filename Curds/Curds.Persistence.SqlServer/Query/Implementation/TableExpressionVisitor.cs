using System.Linq;

namespace Curds.Persistence.Query.Implementation
{
    using Abstraction;
    using Expressions.Nodes.Domain;
    using Persistence.Abstraction;

    internal class TableExpressionVisitor<TModel> : BaseQueryExpressionVisitor<TModel, ISqlTable>, ISqlTableVisitor<TModel>
        where TModel : IDataModel
    {
        public TableExpressionVisitor(ISqlQueryContext<TModel> queryContext)
            : base(queryContext)
        { }

        public override void VisitLambda(LambdaNode lambdaNode) => 
            lambdaNode.Body.AcceptVisitor(this);

        public override void VisitParameter(ParameterNode parameterNode) =>
            Context.Tables.First(table => table.EntityType == parameterNode.SourceExpression.Type);

        public override ISqlTable Build()
        {
            throw new System.NotImplementedException();
        }
    }
}
