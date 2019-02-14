using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Application.Message
{
    public abstract class BaseQueryHandler<T, U, V> : BaseMessageHandler<T, U, V>
        where T : CurdsApplication
        where U : BaseQuery<V>
        where V : BaseViewModel
    {
        public BaseQueryHandler(T application)
            : base(application)
        { }
    }
}
