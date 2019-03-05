using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain.Persistence;
using Curds.Application.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Persistence.EFCore
{
    public abstract class EFPersistorTemplate<T, U, V> : IPersistorTemplate<T, U> 
        where T : EFPersistor<V, U>
        where U : Entity 
        where V : CurdsContext
    {




    }
}
