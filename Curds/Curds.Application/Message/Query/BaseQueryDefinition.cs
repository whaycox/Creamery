using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Curds.Application.Message.Query
{
    public abstract class BaseQueryDefinition<T, U, V, W> : BaseMessageDefinition<T, U, V, W>
        where T : CurdsApplication
        where U : BaseViewModel
        where V : BaseQuery
        where W : BaseViewModel
    {
        public BaseQueryDefinition(T application)
            : base(application)
        { }
    }
}
