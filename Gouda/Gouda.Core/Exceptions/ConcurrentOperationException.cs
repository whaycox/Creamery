using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Exceptions
{
    public class ConcurrentOperationException : Exception
    {
        public ConcurrentOperationException(string message)
            : base(message)
        { }
    }
}
