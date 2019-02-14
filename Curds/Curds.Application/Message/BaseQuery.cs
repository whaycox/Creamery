using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Application.Message
{
    public abstract class BaseQuery<T> : BaseMessage<T> where T : BaseViewModel
    {
        public BaseQuery(T viewModel)
        : base(viewModel)
        { }
    }
}
