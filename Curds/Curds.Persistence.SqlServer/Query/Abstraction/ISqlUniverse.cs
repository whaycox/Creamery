using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace Curds.Persistence.Query.Abstraction
{
    using Persistence.Abstraction;

    public interface ISqlUniverse<TEntity> 
        where TEntity : IEntity
    {
        ISqlQuery<TEntity> ProjectEntity();
    }
}
