using System;
using System.Collections.Generic;
using System.Text;
using Curds.Persistence.EFCore;
using Curds.Persistence.EFCore.Persistor;
using Curds.Domain.Persistence;

namespace Gouda.Persistence.EFCore.Persistor
{
    public abstract class BasicPersistor<T> : BaseEntityPersistor<GoudaContext, T> where T : Entity
    {
        public BasicPersistor(EFProvider<GoudaContext> provider)
            : base(provider)
        { }
    }
}
