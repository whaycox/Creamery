using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Curds.Persistence.Query.Implementation
{
    using Persistence.Abstraction;
    using Abstraction;
    using ExpressionNodes.Domain;
    using Persistence.Implementation;

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
