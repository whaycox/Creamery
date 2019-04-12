using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Application.Message.Command
{
    public abstract class BaseCommandHandler<T, U> : BaseMessageHandler<T> 
        where T : CurdsApplication
        where U : BaseCommand
    {
        public BaseCommandHandler(T application)
            : base(application)
        { }
    }
}
