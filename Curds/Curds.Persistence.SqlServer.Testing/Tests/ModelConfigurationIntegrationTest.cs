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

namespace Curds.Persistence.Tests
{
    using Curds.Persistence.Abstraction;
    using Template;
    using Domain;
    using Model.Domain;

    [TestClass]
    public class ModelConfigurationIntegrationTest : SqlIntegrationTemplate
    {
        [TestMethod]
        [ExpectedException(typeof(ModelException))]
        public void ConfiguringModelWithoutKeyThrows()
        {
            RegisterServices();
            BuildServiceProvider();

            TestServiceProvider.GetRequiredService<IRepository<IKeylessDataModel, GenericToken>>();
        }
    }
}
