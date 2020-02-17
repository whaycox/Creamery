using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace Curds.Persistence.Query.Abstraction
{
    using Persistence.Domain;
    using Persistence.Abstraction;

    public interface ISqlQueryBuilder<TModel>
        where TModel : IDataModel
    {
        ISqlQuery Insert<TEntity>(Expression<Func<TModel, ITable<TEntity>>> tableExpression, IEnumerable<TEntity> entities)
            where TEntity : BaseEntity;
    }
}
