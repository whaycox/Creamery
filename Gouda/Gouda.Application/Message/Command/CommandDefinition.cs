using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application.Message;

namespace Gouda.Application.Message.Command
{
    public abstract class CommandDefinition<T, U, V> : MessageDefinition<T, U, V>
        where T : BaseCommand<V>
        where U : BaseCommandHandler<GoudaApplication, T, V>
        where V : BaseViewModel
    {
        public CommandDefinition(GoudaApplication application)
            : base(application)
        { }
    }
}
