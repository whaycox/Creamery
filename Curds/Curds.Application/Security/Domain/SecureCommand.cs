using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Application.Security.Domain
{
    using Application.Command.Domain;

    public class SecureCommand<T> : SecureMessage<T> where T : BaseCommand
    {
        public SecureCommand(string sessionID, T command)
            : base(sessionID, command)
        { }
    }
}
