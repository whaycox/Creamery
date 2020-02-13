using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace Curds.Persistence.Query.Abstraction
{
    using Persistence.Abstraction;
    using Implementation;
    using Persistence.Domain;

    internal interface ISqlQueryExpressionParser<TModel>
        where TModel : IDataModel
    {
        InsertQuery<TEntity> Parse<TEntity>(Expression<Func<TModel, ITable<TEntity>>> expression)
            where TEntity : BaseEntity;
    }
}
