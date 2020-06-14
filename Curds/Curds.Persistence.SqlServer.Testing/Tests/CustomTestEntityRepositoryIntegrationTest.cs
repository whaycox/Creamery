using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;
using Whey.Domain;

namespace Curds.Persistence.Tests
{
    using Domain;
    using Implementation;
    using Template;

    [TestClass]
    [TestCategory(nameof(TestType.Integration))]
    public class CustomTestEntityRepositoryIntegrationTest : SqlIntegrationTemplate
    {
        [TestMethod]
        public async Task CanFetchLessThan()
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
