using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Abstraction
{
    using Domain;

    public interface IDataModel
    {
        ITable<TEntity> Table<TEntity>() where TEntity : BaseEntity;
    }
}
