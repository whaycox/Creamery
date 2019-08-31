using System;

namespace Feta.OpenType.Exceptions
{
    public class IncompleteOffsetException : Exception
    {
        public IncompleteOffsetException(string message)
            : base(message)
        { }
    }
}
