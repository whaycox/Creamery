using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain.Security;

namespace Curds.Application.Message.Query
{
    public class SecureQuery<T> : SecureMessage<T> where T : BaseQuery
    {
        public SecureQuery(string sessionID, T query)
            : base(sessionID, query)
        { }
    }
}
