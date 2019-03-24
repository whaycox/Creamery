using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Application.Message
{
    public abstract class BaseDispatch<T> : ReferencingObject<T> where T : CurdsApplication
    {
        public BaseDispatch(T application)
            : base(application)
        { }
    }
}
