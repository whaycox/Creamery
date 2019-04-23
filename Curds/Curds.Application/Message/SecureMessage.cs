using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain.Security;

namespace Curds.Application.Message
{
    public abstract class SecureMessage<T> : BaseMessage where T : BaseMessage
    {
        public string SessionID { get; }
        public T Message { get; }

        public SecureMessage(string sessionID, T message)
        {
            SessionID = sessionID;
            Message = message;
        }
    }
}
