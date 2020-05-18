using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Curds.Persistence.Query.Implementation
{
    using Persistence.Abstraction;
    using Abstraction;
    using ExpressionNodes.Implementation;
    using Persistence.Implementation;

    internal class TableExpressionVisitor<TModel> : BaseExpressionVisitor<ISqlTable, ISqlQueryContext<TModel>>, ISqlTableVisitor<TModel>
        where TModel : IDataModel
    {
        public override ISqlTable VisitParameter(ISqlQueryContext<TModel> context, ParameterNode<ISqlTable, ISqlQueryContext<TModel>> parameterNode) =>
            context.Tables.First(table => table.EntityType == parameterNode.SourceExpression.Type);
    }
}
