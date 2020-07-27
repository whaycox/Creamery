using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Query.Abstraction
{
    using Persistence.Abstraction;

    internal interface ISqlQueryExpressionVisitorFactory
    {
        ISqlTableVisitor<TModel> TableVisitor<TModel>(ISqlQueryContext<TModel> queryContext)
            where TModel : IDataModel;

        ISqlQueryTokenVisitor<TModel> TokenVisitor<TModel>(ISqlQueryContext<TModel> queryContext) 
            where TModel : IDataModel;
    }
}
