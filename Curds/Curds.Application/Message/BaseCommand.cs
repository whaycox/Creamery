using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Application.Message
{
    public abstract class BaseCommand<T> : BaseMessage<T> where T : BaseViewModel
    {
        public BaseCommand(T viewModel)
            : base(viewModel)
        { }
    }
}
