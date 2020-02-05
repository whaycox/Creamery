using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace Curds.Persistence.Abstraction
{
    using Query.Domain;
    using Domain;
    using Query.Implementation;

    internal interface ISqlQueryExpressionParser<TModel>
        where TModel : IDataModel
    {
        InsertQuery<TEntity> Parse<TEntity>(Expression<Func<TModel, ITable<TEntity>>> expression)
            where TEntity : BaseEntity;
    }
}
