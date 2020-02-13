using System;
using System.Linq.Expressions;

namespace Curds.Persistence.Abstraction
{
    using Domain;

    internal interface IExpressionParser
    {
        string ParseEntityValueSelection<TEntity, TValue>(Expression<Func<TEntity, TValue>> valueSelectionExpression)
            where TEntity : BaseEntity;
    }
}
