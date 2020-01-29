using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace Curds.Persistence.Abstraction
{
    using Domain;

    public interface ISqlQueryBuilder<TModel>
        where TModel : IDataModel
    {
        ISqlQuery Insert<TEntity>(Expression<Func<TModel, ITable<TEntity>>> tableExpression, TEntity entity) where TEntity : BaseEntity;
    }
}
