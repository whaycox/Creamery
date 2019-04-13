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

    public abstract class BaseMessageDefinition<T, U, V, W> : BaseMessageDefinition<T>
        where T : CurdsApplication
        where U : BaseViewModel
        where V : BaseMessage
    {
        public abstract U ViewModel { get; }

        public BaseMessageDefinition(T application)
            : base(application)
        { }

        public abstract Task<W> Execute(V message);
    }
}
