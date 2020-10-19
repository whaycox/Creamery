using MediatR;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Parmesan.Server.Commands.SignInUser.Implementation
{
    using Application.Queries.VerifyAuthentication.Domain;
    using Domain;
    using Server.Domain;

    internal class SignInUserHandler : AsyncRequestHandler<SignInUserCommand>
    {
        private IMediator Mediator { get; }

        public SignInUserHandler(IMediator mediator)
        {
            Mediator = mediator;
        }

        protected async override Task Handle(SignInUserCommand request, CancellationToken cancellationToken)
        {
            ClaimsPrincipal authenticatedUser = await Mediator.Send(new VerifyAuthenticationQuery
            {
                Authentication = request.Authentication,
            });
            AuthenticationProperties authProperties = new AuthenticationProperties { IsPersistent = request.Authentication.RememberMe };
            await request.Context.SignInAsync(
                ServerConstants.LoginAuthenticationScheme, 
                authenticatedUser, 
                authProperties);
        }
    }
}
