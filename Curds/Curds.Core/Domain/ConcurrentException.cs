using System;

namespace Curds.Domain
{
    public class ConcurrentException : Exception
    {
        public ConcurrentException(string message)
            : base(message)
        { }

        public ConcurrentException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
