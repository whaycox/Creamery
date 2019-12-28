using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

namespace Gouda.WebApp.Controllers.Implementation
{
    using Application.Commands.AddSatellite.Domain;
    using Application.Queries.DisplaySatellite.Domain;
    using Application.Queries.ListSatellites.Domain;
    using Gouda.Domain;
    using ViewComponents.Implementation;
    using ViewModels.Domain;
    using Application.ViewModels.Satellite.Domain;
    using ViewModels.Abstraction;
    using Application.Queries.GetAddCheck.Domain;
    using Application.Commands.AddCheck.Domain;
    using ViewModels;

    public class SatelliteController : Controller
    {
        public const string Name = nameof(Satellite);

        private IMediator Mediator { get; }
        private IWebAppViewModelWrapper ViewModelWrapper { get; }

        public SatelliteController(IMediator mediator, IWebAppViewModelWrapper viewModelWrapper)
        {
            Mediator = mediator;
            ViewModelWrapper = viewModelWrapper;
        }

        [HttpGet]
        public IActionResult Index() => RedirectToAction(nameof(List));

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> List()
        {
            ListSatellitesQuery query = new ListSatellitesQuery();
            ListSatellitesResult result = await Mediator.Send(query);

            return View(result);
        }

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Display(int id)
        {
            DisplaySatelliteQuery query = new DisplaySatelliteQuery { SatelliteID = id };
            SatelliteViewModel viewModel = await Mediator.Send(query, default);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddSatellite(AddSatelliteCommand command)
        {
            SatelliteSummaryViewModel viewModel = await Mediator.Send(command);
            return this.ReturnViewComponent(ViewModelWrapper, viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetAddCheck()
        {
            NewCheckViewModel viewModel = await Mediator.Send(new GetAddCheckQuery());
            return this.ReturnViewComponent(ViewModelWrapper, viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddCheck(AddCheckCommand command)
        {
            CheckViewModel viewModel = await Mediator.Send(command);
            return this.ReturnViewComponent(ViewModelWrapper, viewModel);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCheck()
        {
            throw new NotImplementedException();
        }

    }
}
