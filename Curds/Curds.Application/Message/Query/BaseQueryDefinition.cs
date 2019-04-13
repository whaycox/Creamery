using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Curds.Application.Message.Query
{
    public abstract class BaseQueryDefinition<T, U, V> : BaseMessageDefinition<T, U, V>
        where T : CurdsApplication
        where U : BaseQuery
        where V : BaseViewModel
    {
        public BaseQueryDefinition(T application)
            : base(application)
        { }
    }
}
