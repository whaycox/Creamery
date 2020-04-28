using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Model.Abstraction
{
    using Persistence.Abstraction;

    public interface IValueModel<TEntity>
        where TEntity : IEntity
    {
    }
}
