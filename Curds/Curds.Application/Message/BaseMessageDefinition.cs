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
        where U : BaseMessageHandler<T>
        where V : BaseViewModel
    {
        public abstract V ViewModel { get; }

        public BaseMessageDefinition(T application)
            : base(application)
        { }

        public abstract U Handler();
    }
}
