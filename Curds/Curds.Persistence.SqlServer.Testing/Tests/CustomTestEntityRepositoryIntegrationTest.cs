using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Curds.Persistence.Tests
{
    using Implementation;
    using Template;
    using Domain;

    [TestClass]
    public class CustomTestEntityRepositoryIntegrationTest : SqlIntegrationTemplate
    {
        [TestMethod]
        public async Task CanInsertTestEntity()
        {
            RegisterServices();
            TestServiceCollection.AddTransient<CustomTestEntityRepository>();
            BuildServiceProvider();

            using (IServiceScope testScope = TestServiceProvider.CreateScope())
            {
                CustomTestEntityRepository testRepository = testScope.ServiceProvider.GetRequiredService<CustomTestEntityRepository>();
                IList<TestEntity> allEntities = await testRepository.FetchAll();
                int maxExpected = 100;
                if (allEntities.Count < maxExpected)
                    Assert.Fail();
                IList<TestEntity> actual = await testRepository.FetchEvensLessThan(maxExpected);
                Assert.AreEqual(maxExpected / 2, actual.Count);
            }
        }
    }
}
