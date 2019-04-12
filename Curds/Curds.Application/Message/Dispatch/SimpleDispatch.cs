using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Application.Message.Dispatch
{
    public abstract class SimpleDispatch<T> : BaseDispatch<T> where T : CurdsApplication
    {
        public SimpleDispatch(T application)
            : base(application)
        { }

        public U Request<U>() where U : BaseMessageDefinition<T> => Lookup<U>();
    }
}
