using System;

namespace Curds.Clone.Domain
{
    public class InvalidCloneDefinitionException : Exception
    {
        public InvalidCloneDefinitionException(string message)
            : base(message)
        { }

        public InvalidCloneDefinitionException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
