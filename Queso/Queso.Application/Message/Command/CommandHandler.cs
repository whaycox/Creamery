using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application.Message;

namespace Queso.Application.Message.Command
{
    public abstract class CommandHandler<T, U> : BaseMessageHandler<QuesoApplication, T, U>
        where T : BaseMessage<U>
        where U : BaseViewModel
    {
        public CommandHandler(QuesoApplication application)
            : base(application)
        { }
    }
}
