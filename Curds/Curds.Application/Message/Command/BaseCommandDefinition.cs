using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Curds.Application.Message.Command
{
    using Query;

    public abstract class BaseCommandDefinition<T, U, V> : BaseMessageDefinition<T, U, V>
        where T : CurdsApplication
        where U : BaseCommand
        where V : BaseQuery
    {
        public BaseCommandDefinition(T application)
            : base(application)
        { }
    }
}
