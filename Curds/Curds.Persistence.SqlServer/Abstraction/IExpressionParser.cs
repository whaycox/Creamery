using System;
using System.Linq.Expressions;

namespace Curds.Persistence.Abstraction
{
    internal interface IExpressionParser
    {
        Type ParseModelEntitySelection<TModel>(Expression<Func<TModel, IEntity>> entitySelectionExpression)
            where TModel : IDataModel;

        string ParseEntityValueSelection<TEntity, TValue>(Expression<Func<TEntity, TValue>> valueSelectionExpression)
            where TEntity : IEntity;
    }
}
