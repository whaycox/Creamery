using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Parmesan.Server.Controllers.Implementation
{
    using Commands.ApproveAuthorizationRequest.Domain;
    using Commands.ProcessAuthorizationRequest.Domain;
    using Domain;
    using Server.Domain;
    using ViewModels.Domain;

    [Authorize(ServerConstants.LoginAuthorizationPolicy)]
    public class OAuthController : Controller
    {
        private IMediator Mediator { get; }

        public OAuthController(IMediator mediator)
        {
            Mediator = mediator;
        }

        [HttpGet]
        [Route(ServerConstants.AuthorizeRoute)]
        public async Task<IActionResult> Authorize(WebAuthorizationRequest webAuthorizationRequest)
        {
            ProcessAuthorizationRequestCommand command = new ProcessAuthorizationRequestCommand
            {
                Request = webAuthorizationRequest,
            };
            AuthorizationRequestViewModel viewModel = await Mediator.Send(command);
            return View(viewModel);
        }

        [HttpPost]
        [Route(ServerConstants.AuthorizeRoute)]
        public async Task<IActionResult> ApproveAuthorization(string ticketNumber)
        {
            string redirectUri = await Mediator.Send(new ApproveAuthorizationRequestCommand
            {
                UserID = HttpContext.User.LoggedInUserID(),
                TicketNumber = ticketNumber,
            });
            return new RedirectResult(redirectUri);
        }
    }
}
