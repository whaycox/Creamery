using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain.Security;

namespace Curds.Application.Message.Command
{
    public class SecureCommand<T> : SecureMessage<T> where T : BaseCommand
    {
        public SecureCommand(string sessionID, T command)
            : base(sessionID, command)
        { }
    }
}
