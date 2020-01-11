using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using MediatR;
using Whey.Template;

namespace Gouda.Application.Tests
{
    using ViewModels.Satellite.Abstraction;
    using ViewModels.Satellite.Implementation;
    using ViewModels.Navigation.Implementation;
    using ViewModels.Navigation.Abstraction;
    using DeferredValues.Abstraction;
    using DeferredValues.Implementation;

    [TestClass]
    public class RegistrationExtensionsTest : RegistrationExtensionsTemplate
    {
        private void TestAddGoudaApplication() => TestServiceCollection.AddGoudaApplication();

        [TestMethod]
        public void GoudaApplicationAddsNavigationTreeBuilder()
        {
            TestAddGoudaApplication();

            VerifyServiceWasRegistered(typeof(INavigationTreeBuilder), typeof(NavigationTreeBuilder), ServiceLifetime.Scoped);
        }

        [TestMethod]
        public void GoudaApplicationAddsLabelDeferredValue()
        {
            TestAddGoudaApplication();

            VerifyServiceWasRegistered(typeof(ILabelDeferredValue), typeof(LabelDeferredValue), ServiceLifetime.Singleton);
        }

        [TestMethod]
        public void GoudaApplicationAddsViewModelMappers()
        {
            TestAddGoudaApplication();

            VerifyServiceWasRegistered(typeof(ISatelliteMapper), typeof(SatelliteMapper), ServiceLifetime.Transient);
            VerifyServiceWasRegistered(typeof(ISatelliteSummaryMapper), typeof(SatelliteSummaryMapper), ServiceLifetime.Transient);
            VerifyServiceWasRegistered(typeof(ICheckDefinitionMapper), typeof(CheckDefinitionMapper), ServiceLifetime.Transient);
        }
    }
}
