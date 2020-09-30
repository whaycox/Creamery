using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Parmesan.Server.Controllers.Implementation
{
    using Domain;
    using Server.Abstraction;
    using Parmesan.Domain;

    public class OAuthController : Controller
    {
        private IAuthorizationRequestParser AuthorizationRequestParser { get; }

        public OAuthController(IAuthorizationRequestParser authorizationRequestParser)
        {
            AuthorizationRequestParser = authorizationRequestParser;
        }

        public const string AuthorizeRoute = "oauth/authorize";
        [HttpGet]
        [Route(AuthorizeRoute)]
        public IActionResult Authorize(WebAuthorizationRequest webAuthorizationRequest)
        {
            AuthorizationRequest parsedRequest = AuthorizationRequestParser.Parse(webAuthorizationRequest);


            return View();
        }
    }
}
