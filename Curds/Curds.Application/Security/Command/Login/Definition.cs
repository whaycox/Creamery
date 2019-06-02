using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Curds.Application.Security.Command.Login
{
    using Application.Command.Domain;
    using Curds.Security.Domain;
    using Security.Domain;

    public class Definition : BaseCommandDefinition<SecureApplication, Command, Authentication>
    {
        public Definition(SecureApplication application)
            : base(application)
        { }

        public override Task<Authentication> Execute(Command message)
        {
            throw new NotImplementedException();
        }
    }
}
