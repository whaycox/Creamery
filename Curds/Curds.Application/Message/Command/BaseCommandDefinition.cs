using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Curds.Application.Message.Command
{
    public abstract class BaseCommandDefinition<T, U, V> : BaseMessageDefinition<T, U, V>
        where T : CurdsApplication
        where U : BaseCommand
    {
        public BaseCommandDefinition(T application)
            : base(application)
        { }
    }
}
