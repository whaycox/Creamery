using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain.Persistence;
using Curds.Application.Persistence.Persistor;

namespace Curds.Persistence.EFCore.Persistor
{
    public abstract class BaseEntityPersistorTemplate<T, U, V> : IEntityPersistorTemplate<T, U>
        where T : BaseEntityPersistor<V, U>
        where U : Entity
        where V : CurdsContext
    { }
}
