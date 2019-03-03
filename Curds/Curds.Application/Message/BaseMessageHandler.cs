using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Application.Message
{
    public abstract class BaseMessageHandler<T, U, V> : ReferencingObject<T> 
        where T : CurdsApplication
        where U : BaseMessage<V>
        where V : BaseViewModel
    {
        public BaseMessageHandler(T application)
            : base(application)
        { }
    }
}
