﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System;
using System.Net;

namespace Parmesan.Server.Controllers.Implementation
{
    using Domain;
    using Commands.SignInUser.Domain;
    using Server.Domain;
    using ViewModels.Domain;

    [AllowAnonymous]
    public class AuthenticationController : Controller
    {
        private IMediator Mediator { get; }
        private ServerOptions ServerOptions { get; }

        public AuthenticationController(
            IMediator mediator,
            IOptions<ServerOptions> serverOptions)
        {
            Mediator = mediator;
            ServerOptions = serverOptions.Value;
        }

        [HttpGet]
        [Route(ServerConstants.LoginRoute)]
        public IActionResult Login(string returnUrl)
        {
            LoginViewModel viewModel = new LoginViewModel
            {
                SiteName = ServerOptions.SiteName,
                ReturnUrl = returnUrl,
            };
            return View(viewModel);
        }

        [HttpPost]
        [Route(ServerConstants.LoginRoute)]
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
