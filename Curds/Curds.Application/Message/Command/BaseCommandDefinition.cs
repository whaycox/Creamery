using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Curds.Application.Message.Command
{
    using Query;

    public abstract class BaseCommandDefinition<T, U, V, W> : BaseMessageDefinition<T, U, V, W>
        where T : CurdsApplication
        where U : BaseViewModel
        where V : BaseCommand
        where W : BaseQuery
    {
        public BaseCommandDefinition(T application)
            : base(application)
        { }
    }
}
