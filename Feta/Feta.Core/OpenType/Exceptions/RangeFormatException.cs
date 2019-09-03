using System;
using System.Collections.Generic;
using System.Text;

namespace Feta.OpenType.Exceptions
{
    public class RangeFormatException : Exception
    {
        public RangeFormatException(string message)
            : base(message)
        { }
    }
}
