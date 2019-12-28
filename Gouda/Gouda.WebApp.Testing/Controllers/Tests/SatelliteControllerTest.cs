using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Gouda.WebApp.Controllers.Tests
{
    using Application.Abstraction;
    using Application.Commands.AddCheck.Domain;
    using Application.Commands.AddSatellite.Domain;
    using Application.Queries.DisplaySatellite.Domain;
    using Application.Queries.ListSatellites.Domain;
    using Application.ViewModels.Satellite.Domain;
    using Implementation;
    using ViewModels.Abstraction;

    [TestClass]
    public class SatelliteControllerTest
    {
        private ListSatellitesResult TestListSatellitesResult = new ListSatellitesResult();
        private AddSatelliteCommand TestAddSatelliteCommand = new AddSatelliteCommand();
        private SatelliteSummaryViewModel TestSummaryViewModel = new SatelliteSummaryViewModel();
        private SatelliteViewModel TestSatelliteViewModel = new SatelliteViewModel();
        private AddCheckCommand TestAddCheckCommand = new AddCheckCommand();
        private CheckViewModel TestCheckViewModel = new CheckViewModel();

        private Mock<IMediator> MockMediator = new Mock<IMediator>();
        private Mock<IWebAppViewModelWrapper> MockViewModelWrapper = new Mock<IWebAppViewModelWrapper>();
        private Mock<IWebAppViewModel> MockWebAppViewModel = new Mock<IWebAppViewModel>();

        private SatelliteController TestObject = null;

        [TestInitialize]
        public void Init()
        {
            MockMediator
                .Setup(mediator => mediator.Send(It.IsAny<AddSatelliteCommand>(), default))
                .ReturnsAsync(TestSummaryViewModel);
            MockMediator
                .Setup(mediator => mediator.Send(It.IsAny<DisplaySatelliteQuery>(), default))
                .ReturnsAsync(TestSatelliteViewModel);
            MockMediator
                .Setup(mediator => mediator.Send(It.IsAny<AddCheckCommand>(), default))
                .ReturnsAsync(TestCheckViewModel);
            MockViewModelWrapper
                .Setup(wrapper => wrapper.Wrap(It.IsAny<IViewModel>()))
                .Returns(MockWebAppViewModel.Object);

            TestObject = new SatelliteController(MockMediator.Object, MockViewModelWrapper.Object);
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
            Assert.AreSame(TestSatelliteViewModel, view.Model);
        }

        [TestMethod]
        public async Task AddSatelliteSendsCommand()
        {
            await TestObject.AddSatellite(TestAddSatelliteCommand);

            MockMediator.Verify(mediator => mediator.Send(TestAddSatelliteCommand, default), Times.Once);
        }

        [TestMethod]
        public async Task AddSatelliteWrapsResult()
        {
            await TestObject.AddSatellite(TestAddSatelliteCommand);

            MockViewModelWrapper.Verify(wrapper => wrapper.Wrap(TestSummaryViewModel), Times.Once);
        }

        [TestMethod]
        public async Task AddSatelliteReturnsWrappedViewComponent()
        {
            IActionResult result = await TestObject.AddSatellite(TestAddSatelliteCommand);

            Assert.IsInstanceOfType(result, typeof(ViewComponentResult));
            ViewComponentResult view = (ViewComponentResult)result;
            Assert.AreEqual(MockWebAppViewModel.Object, view.Arguments);
        }

        [TestMethod]
        public async Task AddCheckSendsCommand()
        {
            await TestObject.AddCheck(TestAddCheckCommand);

            MockMediator.Verify(mediator => mediator.Send(TestAddCheckCommand, default), Times.Once);
        }

        [TestMethod]
        public async Task AddCheckWrapsResult()
        {
            await TestObject.AddCheck(TestAddCheckCommand);

            MockViewModelWrapper.Verify(wrapper => wrapper.Wrap(TestCheckViewModel), Times.Once);
        }

        [TestMethod]
        public async Task AddCheckReturnsWrappedViewComponent()
        {
            IActionResult result = await TestObject.AddCheck(TestAddCheckCommand);

            Assert.IsInstanceOfType(result, typeof(ViewComponentResult));
            ViewComponentResult view = (ViewComponentResult)result;
            Assert.AreEqual(MockWebAppViewModel.Object, view.Arguments);
        }
    }
}
