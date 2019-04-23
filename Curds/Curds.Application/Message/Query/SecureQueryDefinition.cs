using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Application.Message.Query
{
    using ViewModel;

    public abstract class SecureQueryDefinition<T, U, V> : BaseMessageDefinition<T, SecureQuery<U>, V>
        where T : CurdsApplication
        where U : BaseQuery
        where V : BaseViewModel
    {
        public SecureQueryDefinition(T application)
            : base(application)
        { }
    }
}
