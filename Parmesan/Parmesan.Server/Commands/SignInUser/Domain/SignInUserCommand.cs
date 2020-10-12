using MediatR;
using Microsoft.AspNetCore.Http;

namespace Parmesan.Server.Commands.SignInUser.Domain
{
    using Security.Abstraction;

    public class SignInUserCommand : IRequest
    {
        public HttpContext Context { get; set; }
        public IAuthenticationData Authentication { get; set; }
    }
}
