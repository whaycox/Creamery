using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application.Message;

namespace Curds.Application.Message
{
    public abstract class BaseCommandHandler<T, U, V> : BaseMessageHandler<T, U, V>
        where T : CurdsApplication
        where U : BaseCommand<V>
        where V : BaseViewModel
    {
        public BaseCommandHandler(T application)
            : base(application)
        { }
    }
}
