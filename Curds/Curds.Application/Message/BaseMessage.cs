using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Application.Message
{
    public abstract class BaseMessage<T> where T : BaseViewModel
    {
        public BaseMessage(T viewModel)
        { }
    }
}
