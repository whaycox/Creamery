using System;
using System.Linq.Expressions;

namespace Curds.Persistence.Abstraction
{
    using Model.Abstraction;

    internal interface IExpressionParser
    {
        Type ParseModelEntitySelection<TModel>(Expression<Func<TModel, IEntityModel>> entitySelectionExpression)
            where TModel : IDataModel;

        string ParseEntityValueSelection<TEntity, TValue>(Expression<Func<TEntity, TValue>> valueSelectionExpression)
            where TEntity : IEntity;
    }
}
