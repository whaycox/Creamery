using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Model.Abstraction
{
    using Query.Domain;
    using Persistence.Domain;
    using Persistence.Abstraction;

    public delegate ValueEntity ValueEntityDelegate(BaseEntity entity);

    public interface IDelegateMapper
    {
        ValueEntityDelegate MapValueEntityDelegate<TModel>(Type entityType)
            where TModel : IDataModel;
    }
}
