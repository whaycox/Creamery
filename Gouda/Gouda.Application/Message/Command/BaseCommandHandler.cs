using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Application.Message.Command
{
    public abstract class BaseCommandHandler<T> : BaseMessageHandler<T> where T : BaseCommand
    {
        public BaseCommandHandler(Gouda application)
            : base(application)
        { }
    }
}
