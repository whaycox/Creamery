using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application.Message;

namespace Queso.Application.Message
{
    public abstract class MessageDefinition<T, U, V> : BaseMessageDefinition<QuesoApplication, T, U, V>
        where T : BaseMessage<V>
        where U : BaseMessageHandler<QuesoApplication, T, V>
        where V : BaseViewModel
    {
        public MessageDefinition(QuesoApplication application)
            : base(application)
        { }
    }
}
