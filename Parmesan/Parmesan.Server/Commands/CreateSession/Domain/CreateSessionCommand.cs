using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Parmesan.Server.Commands.CreateSession.Domain
{
    public class CreateSessionCommand : IRequest<string>
    {
    }
}
