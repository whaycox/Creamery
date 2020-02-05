using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Curds.Persistence.Model.Tests
{
    using Implementation;
    using Persistence.Abstraction;
    using Abstraction;

    [TestClass]
    public class ModelMapFactoryTest
    {
        private Mock<IModelBuilder> MockModelBuilder = new Mock<IModelBuilder>();

        private ModelMapFactory TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestObject = new ModelMapFactory(MockModelBuilder.Object);
        }

        [TestMethod]
        public void ModelIsExpectedType()
        {
            IModelMap<ITestDataModel> actual = TestObject.Build<ITestDataModel>();

            Assert.IsInstanceOfType(actual, typeof(ModelMap<ITestDataModel>));
        }

        [DataTestMethod]
        [DataRow(2)]
        [DataRow(5)]
        [DataRow(7)]
        [DataRow(15)]
        public async Task CanRequestManyOfSameModel(int numberOfRequests)
        {
            List<Task<IModelMap<ITestDataModel>>> requestTasks = new List<Task<IModelMap<ITestDataModel>>>();
            for (int i = 0; i < numberOfRequests; i++)
                requestTasks.Add(Task.Run(() => TestObject.Build<ITestDataModel>()));

            await Task.WhenAll(requestTasks);

            List<IModelMap<ITestDataModel>> models = requestTasks
                .Select(task => task.Result)
                .ToList();
            Assert.IsTrue(models.All(model => model == models.First()));
        }
    }
}
