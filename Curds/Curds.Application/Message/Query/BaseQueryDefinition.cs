using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Curds.Application.Message.Query
{
    public abstract class BaseQueryDefinition<T, U, V, W, X> : BaseMessageDefinition<T, U, X>
        where T : CurdsApplication
        where U : BaseQueryHandler<T, V, W>
        where V : BaseQuery
        where W : BaseViewModel
        where X : BaseViewModel
    {
        public BaseQueryDefinition(T application)
            : base(application)
        { }
    }
}
