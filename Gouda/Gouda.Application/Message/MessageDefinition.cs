using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application.Message;

namespace Gouda.Application.Message
{
    public abstract class MessageDefinition<T, U, V> : BaseMessageDefinition<GoudaApplication, T, U, V>
        where T : BaseMessage<V>
        where U : BaseMessageHandler<GoudaApplication, T, V>
        where V : BaseViewModel
    {
        public MessageDefinition(GoudaApplication application)
            : base(application)
        { }
    }
}
