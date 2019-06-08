using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Curds.Application.Security.Command.ValidateSession
{
    using Application.Command.Domain;
    using Security.Domain;

    public class Definition : BaseCommandDefinition<SecureApplication, Command, bool>
    {
        public Definition(SecureApplication application)
            : base(application)
        { }

        public override Task<bool> Execute(Command message)
        {
            throw new NotImplementedException();
        }
    }
}
