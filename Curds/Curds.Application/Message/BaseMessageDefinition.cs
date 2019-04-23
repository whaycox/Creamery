using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Curds.Application.Message
{
    public abstract class BaseMessageDefinition<T> : ReferencingObject<T> where T : CurdsApplication
    {
        public BaseMessageDefinition(T application)
            : base(application)
        { }
    }

    public abstract class BaseMessageDefinition<T, U, V> : BaseMessageDefinition<T>
        where T : CurdsApplication
        where U : BaseMessage
    {

        public BaseMessageDefinition(T application)
            : base(application)
        { }

        public abstract Task<V> Execute(U message);
    }
}
