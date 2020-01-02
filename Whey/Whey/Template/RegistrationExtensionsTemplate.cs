using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Whey.Template
{
    public abstract class RegistrationExtensionsTemplate
    {
        protected IServiceCollection TestServiceCollection = new ServiceCollection();
        protected IServiceProvider TestServiceProvider => TestServiceCollection.BuildServiceProvider();

        protected void VerifyServiceWasRegistered(Type abstractionType, Type implementationType, ServiceLifetime lifetime)
        {
            Assert.IsTrue(TestServiceCollection.Any(service =>
            service.ServiceType == abstractionType &&
            service.ImplementationType == implementationType &&
            service.Lifetime == lifetime));
        }
    }
}
