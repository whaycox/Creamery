using System;

namespace Curds.CLI.Operations.Exceptions
{
    public class DuplicateArgumentException : Exception
    {
        public DuplicateArgumentException(string argumentName)
            : base($"Cannot supply duplicate argument {argumentName}")
        { }
    }
}
