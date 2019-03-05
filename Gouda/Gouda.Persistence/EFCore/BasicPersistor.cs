using System;
using System.Collections.Generic;
using System.Text;
using Curds.Persistence.EFCore;
using Curds.Domain.Persistence;

namespace Gouda.Persistence.EFCore
{
    public abstract class BasicPersistor<T> : EFPersistor<GoudaContext, T> where T : Entity
    {
        public BasicPersistor(EFProvider<GoudaContext> provider)
            : base(provider)
        { }
    }
}
