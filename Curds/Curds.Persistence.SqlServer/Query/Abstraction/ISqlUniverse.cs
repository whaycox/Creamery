﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace Curds.Persistence.Query.Abstraction
{
    using Persistence.Abstraction;
    using Model.Abstraction;

    public interface ISqlUniverse
    {
        IEnumerable<ISqlTable> Tables { get; }
        IEnumerable<ISqlQueryToken> Filters { get; }
    }

    public interface ISqlUniverse<TEntity> : ISqlUniverse
        where TEntity : IEntity
    {
        ISqlQuery<TEntity> ProjectEntity();
        ISqlQuery Delete();

        ISqlUniverse<TEntity> Where(Expression<Func<TEntity, bool>> filterExpression);
    }
}
