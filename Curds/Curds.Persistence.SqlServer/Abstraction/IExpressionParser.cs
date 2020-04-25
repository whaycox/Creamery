using System;
using System.Linq.Expressions;

namespace Curds.Persistence.Abstraction
{
    internal interface IExpressionParser
    {
        Type ParseModelEntitySelection<TModel, TEntity>(Expression<Func<TModel, ITable<TEntity>>> entitySelectionExpression)
            where TModel : IDataModel
            where TEntity : IEntity;

        string ParseEntityValueSelection<TEntity, TValue>(Expression<Func<TEntity, TValue>> valueSelectionExpression)
            where TEntity : IEntity;
    }
}
