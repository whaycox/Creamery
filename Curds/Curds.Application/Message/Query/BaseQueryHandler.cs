using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Curds.Application.Message.Query
{
    public abstract class BaseQueryHandler<T, U, V> : BaseMessageHandler<T>
        where T : CurdsApplication
        where U : BaseQuery
        where V : BaseViewModel
    {
        public BaseQueryHandler(T application)
            : base(application)
        { }

        public abstract Task<V> Execute(U query);
    }
}
