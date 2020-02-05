using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Abstraction
{
    using Domain;

    public interface ISqlQuery
    {
        void Write(ISqlQueryWriter queryWriter);
    }

    public interface ISqlQuery<TEntity> : ISqlQuery
        where TEntity : BaseEntity
    { }
}
