using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace Curds.Persistence.Abstraction
{
    using Domain;

    public delegate TValue EntityValueSelectionDelegate<TEntity, TValue>(TEntity entity);

    public interface IExpressionParser
    {
        string ParseEntityValueSelection<TEntity, TValue>(Expression<Func<TEntity, TValue>> valueSelectionExpression) 
            where TEntity : BaseEntity;
    }
}
