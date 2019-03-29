using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application.Message;

namespace Queso.Application.Message.Command
{
    public abstract class CommandDefinition<T, U, V> : MessageDefinition<T, U, V>
        where T : BaseMessage<V>
        where U : CommandHandler<T, V>
        where V : BaseViewModel
    {
        public CommandDefinition(QuesoApplication application)
            : base(application)
        { }
    }
}
