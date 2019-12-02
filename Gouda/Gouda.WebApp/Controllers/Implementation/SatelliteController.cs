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

    public class SatelliteController : Controller
    {
        public const string Name = nameof(Satellite);

        private IMediator Mediator { get; }

        public SatelliteController(IMediator mediator)
        {
            Mediator = mediator;
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
            DisplaySatelliteResult result = await Mediator.Send(query, default);

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddSatellite(AddSatelliteCommand command)
        {
            AddSatelliteResult result = await Mediator.Send(command);
            return ViewComponent(result.NewSatellite.ViewConcept, result.NewSatellite);
        }

        [HttpGet]
        public async Task<IActionResult> GetAddCheck()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> AddCheck()
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCheck()
        {
            throw new NotImplementedException();
        }

    }
}
