using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Query.Abstraction
{
    using Persistence.Abstraction;

    internal interface ISqlQueryTokenVisitor<TModel> : IExpressionVisitor<ISqlQueryToken>
        where TModel : IDataModel
    {
    }
}
