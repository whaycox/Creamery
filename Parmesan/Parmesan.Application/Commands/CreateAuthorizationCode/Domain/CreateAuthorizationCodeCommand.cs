using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Parmesan.Application.Commands.CreateAuthorizationCode.Domain
{
    public class CreateAuthorizationCodeCommand : IRequest<string>
    {
        public int ClientID { get; set; }
        public int UserID { get; set; }
        public string Scope { get; set; }
        public string CodeChallenge { get; set; }
    }
}
