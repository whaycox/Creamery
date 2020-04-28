using System;
using System.Linq.Expressions;

namespace Curds.Persistence.Abstraction
{
    using Model.Abstraction;

    internal interface IExpressionParser
    {
        Type ParseModelEntitySelection<TModel, TEntity>(Expression<Func<TModel, IEntityModel<TEntity>>> entitySelectionExpression)
            where TModel : IDataModel
            where TEntity : IEntity;

        string ParseEntityValueSelection<TEntity, TValue>(Expression<Func<TEntity, TValue>> valueSelectionExpression)
            where TEntity : IEntity;
    }
}
