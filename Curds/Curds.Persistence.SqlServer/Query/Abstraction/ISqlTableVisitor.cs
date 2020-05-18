using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Query.Abstraction
{
    using Persistence.Abstraction;

    internal interface ISqlTableVisitor<TModel> : IExpressionVisitor<ISqlTable, ISqlQueryContext<TModel>>
        where TModel : IDataModel
    {
    }
}
