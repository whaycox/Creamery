using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Curds.Persistence.Tests
{
    using Abstraction;
    using Domain;
    using Template;

    [TestClass]
    public class SqlRepositoryIntegrationTest : SqlIntegrationTemplate
    {
        [TestMethod]
        public async Task CanDeleteSingleEntity()
        {
            RegisterServices();
            ConfigureCustomTestEntity();
            BuildServiceProvider();
            IRepository<ITestDataModel, TestEntity> testRepository = TestServiceProvider.GetRequiredService<IRepository<ITestDataModel, TestEntity>>();
            TestEntity.Name = nameof(CanDeleteSingleEntity);
            await testRepository.Insert(TestEntity);

            await testRepository.Delete(TestEntity.ID);

            try
            {
                await testRepository.Fetch(TestEntity.ID);
                Assert.Fail();
            }
            catch (KeyNotFoundException)
            { }
        }

        [TestMethod]
        public async Task CanUpdateWithConstantValues()
        {
            RegisterServices();
            ConfigureCustomTestEntity();
            BuildServiceProvider();
            IRepository<ITestDataModel, TestEntity> testRepository = TestServiceProvider.GetRequiredService<IRepository<ITestDataModel, TestEntity>>();
            await testRepository.Insert(TestEntity);

            await testRepository
                .Update(TestEntity.ID)
                .Set(entity => entity.Name, nameof(CanUpdateWithConstantValues))
                .Execute();

            TestEntity actual = await testRepository.Fetch(TestEntity.ID);
            Assert.AreEqual(TestEntity.ID, actual.ID);
            Assert.AreEqual(nameof(CanUpdateWithConstantValues), actual.Name);
            Assert.AreNotSame(TestEntity, actual);
        }

        [TestMethod]
        public async Task CanProjectEntityFromJoin()
        {
            RegisterServices();
            BuildServiceProvider();
            IRepository<ITestDataModel, Parent> parentRepository = TestServiceProvider.GetRequiredService<IRepository<ITestDataModel, Parent>>();
            IChildRepository childRepository = TestServiceProvider.GetRequiredService<IChildRepository>();
            Parent testParent = new Parent { Name = nameof(testParent) };
            await parentRepository.Insert(testParent);
            List<Child> testChildren = new List<Child>();
            for (int i = 0; i < 3; i++)
            {
                Child testChild = new Child
                {
                    ParentID = testParent.ID,
                    Name = $"{nameof(testChild)}{i}",
                };
                testChildren.Add(testChild);
            }
            await childRepository.Insert(testChildren);

            List<Child> actual = await childRepository.ChildrenByParent(testParent.ID);

            CollectionAssert.AreEqual(testChildren, actual);
        }

        [TestMethod]
        public async Task CanJoinOnMultipleCriteria()
        {
            RegisterServices();
            BuildServiceProvider();
            IRepository<ITestDataModel, Parent> parentRepository = TestServiceProvider.GetRequiredService<IRepository<ITestDataModel, Parent>>();
            IChildRepository childRepository = TestServiceProvider.GetRequiredService<IChildRepository>();
            Parent testParent = new Parent { Name = nameof(testParent) };
            await parentRepository.Insert(testParent);
            List<Child> testChildren = new List<Child>();
            for (int i = 0; i < 3; i++)
            {
                Child testChild = new Child
                {
                    ParentID = testParent.ID,
                    Name = $"{nameof(testChild)}{i}",
                };
                testChildren.Add(testChild);
            }
            await childRepository.Insert(testChildren);

            List<Child> actual = await childRepository.OddChildrenByParent(testParent.ID);

            List<Child> oddChildren = testChildren
                .Where(child => child.ID % 2 != 0)
                .ToList();
            CollectionAssert.AreEqual(oddChildren, actual);
        }

        [TestMethod]
        public async Task CanFilterJoinedUniverse()
        {
            RegisterServices();
            BuildServiceProvider();
            IRepository<ITestDataModel, Parent> parentRepository = TestServiceProvider.GetRequiredService<IRepository<ITestDataModel, Parent>>();
            IChildRepository childRepository = TestServiceProvider.GetRequiredService<IChildRepository>();
            Parent testParent = new Parent { Name = nameof(testParent) };
            await parentRepository.Insert(testParent);
            List<Child> testChildren = new List<Child>();
            for (int i = 0; i < 3; i++)
            {
                Child testChild = new Child
                {
                    ParentID = testParent.ID,
                    Name = $"{nameof(testChild)}{i}",
                };
                testChildren.Add(testChild);
            }
            await childRepository.Insert(testChildren);

            List<Child> actual = await childRepository.EvenChildrenByParent(testParent.ID);

            List<Child> evenChildren = testChildren
                .Where(child => child.ID % 2 == 0)
                .ToList();
            CollectionAssert.AreEqual(evenChildren, actual);
        }
    }
}
