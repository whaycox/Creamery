using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Query.Abstraction
{
    using Persistence.Abstraction;
    using Expressions.Abstraction;

    internal interface ISqlTableVisitor<TModel> : IExpressionVisitor<ISqlTable>
        where TModel : IDataModel
    {
    }
}
