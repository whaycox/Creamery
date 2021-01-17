using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Whey.Domain;

namespace Curds.Template
{
    [TestCategory(nameof(TestType.Integration))]
    public abstract class IntegrationTemplate
    {
        protected ServiceCollection TestServiceCollection = new ServiceCollection();
        protected ServiceProvider TestServiceProvider = null;

        protected void BuildServiceProvider()
        {
            TestServiceProvider = TestServiceCollection.BuildServiceProvider();
        }

        protected void RegisterServices()
        {
            TestServiceCollection
                .AddCurdsCore();
        }

        [TestCleanup]
        public void Cleanup()
        {
            TestServiceProvider?.Dispose();
        }
    }
}
