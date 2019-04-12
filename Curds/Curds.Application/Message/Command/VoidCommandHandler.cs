using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Curds.Application.Message.Command
{
    public abstract class VoidCommandHandler<T, U> : BaseCommandHandler<T, U>
        where T : CurdsApplication
        where U : BaseCommand
    {
        public VoidCommandHandler(T application)
            : base(application)
        { }

        public abstract Task Execute(U command);
    }
}
