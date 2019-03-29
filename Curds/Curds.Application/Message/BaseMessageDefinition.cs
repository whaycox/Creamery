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
        where U : BaseMessage<W>
        where V : BaseMessageHandler<T, U, W>
        where W : BaseViewModel
    {
        protected abstract V Handler { get; }

        public BaseMessageDefinition(T application)
            : base(application)
        { }

        public abstract Task<BaseViewModel> ViewModel();
        protected abstract U BuildMessage(W viewModel);
    }
}
