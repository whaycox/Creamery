using System;

namespace Curds.Persistence.Domain
{
    public class QueryResultCountException : Exception
    {
        public QueryResultCountException(string message)
            : base(message)
        { }

        public QueryResultCountException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
