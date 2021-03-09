using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Parmesan.Server.Controllers.Implementation
{
    using Application.Commands.IssueAccessToken.Domain;
    using Parmesan.Domain;
    using Server.Domain;
    using ViewModels.Domain;
    using Server.Abstraction;
    using Application.Commands.CreateAuthorizationTicket.Domain;
    using Application.Commands.RedeemAuthorizationTicket.Domain;

    [Authorize(ServerConstants.LoginAuthorizationPolicy)]
    public class OAuthController : Controller
    {
        private IOAuthRequestFactory OAuthRequestFactory { get; }
        private IMediator Mediator { get; }

        public OAuthController(
            IOAuthRequestFactory oAuthRequestFactory,
            IMediator mediator)
        {
            OAuthRequestFactory = oAuthRequestFactory;
            Mediator = mediator;
        }

        [HttpGet]
        [Route(ServerConstants.AuthorizeRoute)]
        public async Task<IActionResult> Authorize(WebAuthorizationRequest authorizationRequest)
        {
            AuthorizationRequest request = OAuthRequestFactory.Authorization(authorizationRequest);
            CreateAuthorizationTicketCommand command = new CreateAuthorizationTicketCommand
            {
                Request = request,
            };

            return View(
                await Mediator.Send(command));
        }

        [HttpPost]
        [Route(ServerConstants.AuthorizeRoute)]
        public async Task<IActionResult> ApproveAuthorization(RedeemAuthorizationTicketCommand command)
        {
            command.UserID = HttpContext.User.LoggedInUserID();
            string redirectUri = await Mediator.Send(command);

            return new RedirectResult(redirectUri);
        }

        [HttpPost]
        [Route(ServerConstants.TokenRoute)]
        public async Task<AccessTokenResponse> AccessToken(WebAccessTokenRequest accessTokenRequest)
        {
            AccessTokenRequest request = OAuthRequestFactory.AccessToken(accessTokenRequest);
            IssueAccessTokenCommand command = new IssueAccessTokenCommand
            {
                Request = request,
            };
            return await Mediator.Send(command);
        }
    }
}
