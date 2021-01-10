using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Whey.Domain;

namespace Curds.Template
{
    [TestCategory(nameof(TestType.Integration))]
    public abstract class IntegrationTemplate
    {
        protected IServiceCollection TestServiceCollection = new ServiceCollection();
        protected IServiceProvider TestServiceProvider = null;

        protected void BuildServiceProvider()
        {
            TestServiceProvider = TestServiceCollection.BuildServiceProvider();
        }

        protected void RegisterServices()
        {
            TestServiceCollection
                .AddCurdsCore();
        }
    }
}
