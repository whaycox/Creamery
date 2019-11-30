using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Gouda.WebApp.Adapters.Tests
{
    using Application.DeferredValues.Domain;
    using DeferredValues.Abstraction;
    using DeferredValues.Domain;
    using Implementation;

    [TestClass]
    public class DestinationAdapterTest
    {
        private ActionContext TestActionContext = new ActionContext();
        private DestinationDeferredKey TestDeferredKey = DestinationDeferredKey.None;
        private DestinationItem TestDestinationItem = new DestinationItem();
        private string TestController = nameof(TestController);
        private string TestAction = nameof(TestAction);
        private string TestUrl = nameof(TestUrl);

        private Mock<IUrlHelperFactory> MockUrlHelperFactory = new Mock<IUrlHelperFactory>();
        private Mock<IUrlHelper> MockUrlHelper = new Mock<IUrlHelper>();
        private Mock<IActionContextAccessor> MockActionContextAccessor = new Mock<IActionContextAccessor>();
        private Mock<IDestinationDeferredValue> MockDestinationDeferredValue = new Mock<IDestinationDeferredValue>();

        private UrlActionContext SuppliedUrlContext = null;

        private DestinationAdapter TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestDestinationItem.Controller = TestController;
            TestDestinationItem.Action = TestAction;

            MockActionContextAccessor
                .Setup(accessor => accessor.ActionContext)
                .Returns(TestActionContext);
            MockUrlHelperFactory
                .Setup(url => url.GetUrlHelper(It.IsAny<ActionContext>()))
                .Returns(MockUrlHelper.Object);
            MockUrlHelper
                .Setup(url => url.Action(It.IsAny<UrlActionContext>()))
                .Callback<UrlActionContext>(context => SuppliedUrlContext = context)
                .Returns(TestUrl);
            MockDestinationDeferredValue
                .Setup(value => value[It.IsAny<DestinationDeferredKey>()])
                .Returns(TestDestinationItem);
        }

        private void BuildObject()
        {
            TestObject = new DestinationAdapter(
                MockUrlHelperFactory.Object,
                MockActionContextAccessor.Object,
                MockDestinationDeferredValue.Object);
        }

        [TestMethod]
        public void BuildsUrlHelperFromDependencies()
        {
            BuildObject();

            MockActionContextAccessor.Verify(accessor => accessor.ActionContext, Times.Once);
            MockUrlHelperFactory.Verify(url => url.GetUrlHelper(TestActionContext), Times.Once);
        }

        [TestMethod]
        public void AdaptGetsDeferredValueForKey()
        {
            BuildObject();

            TestObject.Adapt(TestDeferredKey);

            MockDestinationDeferredValue.Verify(value => value[TestDeferredKey], Times.Once);
        }

        [TestMethod]
        public void AdaptGetsUrlFromHelperWithItem()
        {
            BuildObject();

            TestObject.Adapt(TestDeferredKey);

            Assert.AreEqual(TestAction, SuppliedUrlContext.Action);
            Assert.AreEqual(TestController, SuppliedUrlContext.Controller);
            Assert.IsNull(SuppliedUrlContext.Host);
            Assert.IsNull(SuppliedUrlContext.Values);
            Assert.IsNull(SuppliedUrlContext.Protocol);
            Assert.IsNull(SuppliedUrlContext.Fragment);
        }

        [TestMethod]
        public void AdaptReturnsBuiltUrl()
        {
            BuildObject();

            string actual = TestObject.Adapt(TestDeferredKey);

            Assert.AreEqual(TestUrl, actual);
        }
    }
}
