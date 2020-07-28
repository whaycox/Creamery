using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace Cheddar.Server.Controllers.Implementation
{
    using ViewModel.Domain;
    using Application.Commands.AddOrganization.Domain;

    [ApiController]
    [Route("[controller]/[action]")]
    public class OrganizationController : ControllerBase
    {
        private IMediator Mediator { get; }

        public OrganizationController(IMediator mediator)
        {
            Mediator = mediator;
        }

        public async Task<int> Add(OrganizationViewModel viewModel)
        {
            AddOrganizationCommand command = new AddOrganizationCommand
            {
                Name = viewModel.Name,
            };
            return await Mediator.Send(command);
        }
    }
}
