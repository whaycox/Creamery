using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application.Message;

namespace Gouda.Application.Message.Command
{
    public abstract class CommandHandler<T, U> : BaseCommandHandler<GoudaApplication, T, U>
        where T : BaseCommand<U>
        where U : BaseViewModel
    {
        public CommandHandler(GoudaApplication application)
            : base(application)
        { }
    }
}
