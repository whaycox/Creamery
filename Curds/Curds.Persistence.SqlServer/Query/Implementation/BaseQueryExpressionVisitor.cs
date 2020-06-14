using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Query.Implementation
{
    using Persistence.Abstraction;
    using Persistence.Implementation;
    using Abstraction;

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
