using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Curds.Application.Message.Command
{
    public abstract class QueryingCommandHandler<T, U, V> : BaseCommandHandler<T, U>
        where T : CurdsApplication
        where U : BaseCommand
        where V : BaseViewModel
    {
        public QueryingCommandHandler(T application)
            : base(application)
        { }

        public abstract Task<V> Execute(U command);
    }
}
