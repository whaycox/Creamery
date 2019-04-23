using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain.Persistence;
using Curds.Application.Persistence.Persistor;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Persistence.EFCore.Persistor
{
    public abstract class BasePersistorTemplate<T, U, V> : IPersistorTemplate<T, U>
        where T : BasePersistor<V, U>
        where U : BaseEntity
        where V : CurdsContext
    { }
}
