using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Application.Message
{
    public abstract class BaseMessageDefinition<T, U, V, W> : ReferencingObject<T>
        where T : CurdsApplication 
        where U : BaseMessage<W>
        where V : BaseMessageHandler<T, U, W>
        where W : BaseViewModel
    {
        public abstract BaseViewModel ViewModel { get; }

        protected abstract V Handler { get; }

        public BaseMessageDefinition(T application)
            : base(application)
        { }

        protected abstract U BuildMessage(W viewModel);
    }
}
