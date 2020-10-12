using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Parmesan.Server.Controllers.Implementation
{
    using Application.Queries.VerifyAuthorizationRequest.Domain;
    using Parmesan.Domain;
    using Server.Abstraction;
    using Server.Domain;
    using ViewModels.Domain;

    [Authorize(ServerConstants.LoginAuthorizationPolicy)]
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

        [HttpGet]
        [Route(ServerConstants.AuthorizeRoute)]
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
