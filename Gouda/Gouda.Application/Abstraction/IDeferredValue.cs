using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Application.Abstraction
{
    public interface IDeferredValue<TDeferredKey, TDeferredValue> where TDeferredKey : Enum
    {
        TDeferredValue this[TDeferredKey key] { get; }
    }
}
