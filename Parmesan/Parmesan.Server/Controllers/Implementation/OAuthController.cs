using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Parmesan.Server.Controllers.Implementation
{
    using Application.Queries.VerifyAuthorizationRequest.Domain;
    using Domain;
    using Parmesan.Domain;
    using Server.Abstraction;
    using ViewModels.Domain;
    using Queries.CheckForSession.Domain;
    using Commands.BindAuthorizationToSession.Domain;

    [Authorize]
    public class OAuthController : Controller
    {
        private IAuthorizationRequestParser AuthorizationRequestParser { get; }
        private IMediator Mediator { get; }

        public OAuthController(
            IAuthorizationRequestParser authorizationRequestParser,
            IMediator mediator)
        {
            AuthorizationRequestParser = authorizationRequestParser;
            Mediator = mediator;
        }

        public const string AuthorizeRoute = "oauth/authorize";
        [HttpGet]
        [Route(AuthorizeRoute)]
        public async Task<IActionResult> Authorize(WebAuthorizationRequest webAuthorizationRequest)
        {
            AuthorizationRequest parsedRequest = AuthorizationRequestParser.Parse(webAuthorizationRequest);
            AuthorizationRequestViewModel viewModel = await Mediator.Send(new VerifyAuthorizationRequestQuery
            {
                AuthorizationRequest = parsedRequest,
            });
            return View(viewModel);
        }
    }
}
