using System;
using System.Linq.Expressions;

namespace Curds.Persistence.Abstraction
{
    using Domain;

    internal interface IExpressionParser
    {
        Type ParseModelEntitySelection<TModel, TEntity>(Expression<Func<TModel, ITable<TEntity>>> entitySelectionExpression)
            where TModel : IDataModel
            where TEntity : BaseEntity;

        string ParseEntityValueSelection<TEntity, TValue>(Expression<Func<TEntity, TValue>> valueSelectionExpression)
            where TEntity : BaseEntity;
    }
}
