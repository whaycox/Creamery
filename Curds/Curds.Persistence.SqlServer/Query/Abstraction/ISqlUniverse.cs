using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace Curds.Persistence.Query.Abstraction
{
    using Persistence.Domain;

    public interface ISqlUniverse<TEntity> 
        where TEntity : BaseEntity
    {
        ISqlQuery<TEntity> ProjectEntity();
    }
}
