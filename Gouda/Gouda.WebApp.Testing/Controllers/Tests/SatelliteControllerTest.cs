using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Gouda.WebApp.Controllers.Tests
{
    using Application.Commands.AddSatellite.Domain;
    using Application.Queries.ListSatellites.Domain;
    using Application.ViewModels.Satellite.Domain;
    using Implementation;
    using ViewComponents.Implementation;
    using Application.Queries.DisplaySatellite.Domain;

    [TestClass]
    public class SatelliteControllerTest
    {
        private ListSatellitesResult TestListSatellitesResult = new ListSatellitesResult();
        private AddSatelliteCommand TestAddSatelliteCommand = new AddSatelliteCommand();
        private AddSatelliteResult TestAddSatelliteResult = new AddSatelliteResult();
        private SatelliteSummaryViewModel TestSummaryViewModel = new SatelliteSummaryViewModel();
        private DisplaySatelliteResult TestDisplaySatelliteResult = new DisplaySatelliteResult();

        private Mock<IMediator> MockMediator = new Mock<IMediator>();

        private SatelliteController TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestAddSatelliteResult.NewSatellite = TestSummaryViewModel;

            MockMediator
                .Setup(mediator => mediator.Send(It.IsAny<AddSatelliteCommand>(), default))
                .ReturnsAsync(TestAddSatelliteResult);
            MockMediator
                .Setup(mediator => mediator.Send(It.IsAny<DisplaySatelliteQuery>(), default))
                .ReturnsAsync(TestDisplaySatelliteResult);

            TestObject = new SatelliteController(MockMediator.Object);
        }

        [TestMethod]
        public void IndexRedirectsToList()
        {
            IActionResult result = TestObject.Index();

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            RedirectToActionResult redirect = (RedirectToActionResult)result;
            Assert.AreEqual(nameof(TestObject.List), redirect.ActionName);
        }

        [TestMethod]
        public async Task ListQueriesForSatellites()
        {
            IActionResult result = await TestObject.List();

            MockMediator.Verify(mediator => mediator.Send(It.IsAny<ListSatellitesQuery>(), default), Times.Once);
        }

        [TestMethod]
        public async Task ListReturnsViewWithResult()
        {
            MockMediator
                .Setup(mediator => mediator.Send(It.IsAny<ListSatellitesQuery>(), default))
                .ReturnsAsync(TestListSatellitesResult);

            IActionResult result = await TestObject.List();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult view = (ViewResult)result;
            Assert.AreSame(TestListSatellitesResult, view.Model);
        }

        [DataTestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        [DataRow(100)]
        public async Task DisplaySendsQueryWithID(int testID)
        {
            await TestObject.Display(testID);

            MockMediator.Verify(mediator => mediator.Send(It.Is<DisplaySatelliteQuery>(query => query.SatelliteID == testID), default), Times.Once);
        }

        [TestMethod]
        public async Task DisplayReturnsResultInView()
        {
            IActionResult result = await TestObject.Display(1);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult view = (ViewResult)result;
            Assert.AreSame(TestDisplaySatelliteResult, view.Model);
        }

        [TestMethod]
        public async Task AddSendsCommand()
        {
            await TestObject.AddSatellite(TestAddSatelliteCommand);

            MockMediator.Verify(mediator => mediator.Send(TestAddSatelliteCommand, default), Times.Once);
        }

        [TestMethod]
        public async Task AddReturnsViewComponentWithSummary()
        {
            IActionResult result = await TestObject.AddSatellite(TestAddSatelliteCommand);

            Assert.IsInstanceOfType(result, typeof(ViewComponentResult));
            ViewComponentResult view = (ViewComponentResult)result;
            Assert.AreEqual(TestSummaryViewModel.ViewConcept, view.ViewComponentName);
            Assert.AreSame(TestSummaryViewModel, view.Arguments);
        }
    }
}
