﻿using System;
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
        ISqlQuery Insert<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : IEntity;

        ISqlUniverse<TEntity> From<TEntity>()
            where TEntity : IEntity;
    }
}