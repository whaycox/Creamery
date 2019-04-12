using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Application.Message.Command
{
    public abstract class BaseCommandDefinition<T, U, V, W> : BaseMessageDefinition<T, U, W>
        where T : CurdsApplication
        where U : BaseCommandHandler<T, V>
        where V : BaseCommand
        where W : BaseViewModel
    {
        public BaseCommandDefinition(T application)
            : base(application)
        { }
    }
}
