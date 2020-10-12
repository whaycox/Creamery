using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Parmesan.Server.Controllers.Implementation
{
    using Domain;
    using Commands.SignInUser.Domain;

    public class AuthenticationController : Controller
    {
        private IMediator Mediator { get; }

        public AuthenticationController(IMediator mediator)
        {
            Mediator = mediator;
        }

        [HttpGet]
        [Route(AuthenticationRoutes.LoginRoute)]
        public IActionResult Login(string returnUrl)
        {
            return View(model: returnUrl);
        }

        [HttpPost]
        [Route(AuthenticationRoutes.LoginRoute)]
        public async Task<IActionResult> Authenticate(PasswordAuthenticationSubmission authenticationSubmission)
        {
            SignInUserCommand signInCommand = new SignInUserCommand
            {
                Context = HttpContext,
                Authentication = authenticationSubmission,
            };
            await Mediator.Send(signInCommand);
            return new RedirectResult(authenticationSubmission.ReturnUrl);
        }
    }
}
