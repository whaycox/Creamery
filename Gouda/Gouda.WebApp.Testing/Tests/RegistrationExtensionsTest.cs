using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using Whey.Template;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Gouda.WebApp.Tests
{
    using Implementation;
    using DeferredValues.Implementation;
    using DeferredValues.Abstraction;
    using ViewModels.Abstraction;
    using ViewModels.Implementation;

    [TestClass]
    public class RegistrationExtensionsTest : RegistrationExtensionsTemplate
    {
        private void TestAddGoudaWebApp() => TestServiceCollection.AddGoudaWebApp();

        [TestMethod]
        public void GoudaWebAppAddsActionContextAccessor()
        {
            TestAddGoudaWebApp();

            VerifyServiceWasRegistered(typeof(IActionContextAccessor), typeof(ActionContextAccessor), ServiceLifetime.Singleton);
        }

        [TestMethod]
        public void GoudaWebAppAddsWebAppViewModelWrapper()
        {
            TestAddGoudaWebApp();

            VerifyServiceWasRegistered(typeof(IWebAppViewModelWrapper), typeof(WebAppViewModelWrapper), ServiceLifetime.Singleton);
        }

        [TestMethod]
        public void GoudaWebAppAddsCheckExecutionService()
        {
            TestAddGoudaWebApp();

            VerifyServiceWasRegistered(typeof(IHostedService), typeof(CheckExecutionService), ServiceLifetime.Singleton);
        }

        [TestMethod]
        public void GoudaWebAppAddsDestinationDeferredValue()
        {
            TestAddGoudaWebApp();

            VerifyServiceWasRegistered(typeof(IDestinationDeferredValue), typeof(DestinationDeferredValue), ServiceLifetime.Singleton);
        }

    }
}
