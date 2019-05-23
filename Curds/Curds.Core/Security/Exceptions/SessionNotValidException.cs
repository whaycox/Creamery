using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Domain.Exceptions
{
    public class SessionNotValidException : Exception
    {
        public string SessionIdentifier { get; }
        public string DeviceIdentifier { get; }

        public SessionNotValidException(string sessionIdentifier, string deviceIdentifier)
        {
            SessionIdentifier = sessionIdentifier;
            DeviceIdentifier = deviceIdentifier;
        }
    }
}
