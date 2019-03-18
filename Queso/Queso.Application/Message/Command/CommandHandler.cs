using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application.Message;

namespace Queso.Application.Message.Command
{
    public abstract class CommandHandler<T, U> : BaseCommandHandler<QuesoApplication, T, U>
        where T : BaseCommand<U>
        where U : BaseViewModel
    {
        public CommandHandler(QuesoApplication application)
            : base(application)
        { }
    }
}
