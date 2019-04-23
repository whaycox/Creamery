using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Application.Message.Command
{
    using Query;

    public abstract class SecureCommandDefinition<T, U, V> : BaseMessageDefinition<T, SecureCommand<U>, V>
        where T : CurdsApplication
        where U : BaseCommand
        where V : BaseQuery
    {
        public SecureCommandDefinition(T application)
            : base(application)
        { }
    }
}
