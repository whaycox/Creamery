using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Curds.Clone.Tests
{
    using Implementation;
    using Abstraction;
    using Domain;

    [TestClass]
    public class CloneFactoryTest
    {
        private PrimitiveEntity TestEntity = new PrimitiveEntity();

        private Mock<ICloneDefinitionFactory> MockDefinitionFactory = new Mock<ICloneDefinitionFactory>();
        private Mock<ICloneDefinition<PrimitiveEntity>> MockDefinition = new Mock<ICloneDefinition<PrimitiveEntity>>();

        private CloneFactory TestObject = null;

        [TestInitialize]
        public void Init()
        {
            MockDefinitionFactory
                .Setup(factory => factory.Create<PrimitiveEntity>(It.IsAny<ICloneFactory>()))
                .Returns(MockDefinition.Object);

            TestObject = new CloneFactory(MockDefinitionFactory.Object);
        }

        [TestMethod]
        public void CreatesDefinitionForType()
        {
            TestObject.Clone(TestEntity);

            MockDefinitionFactory.Verify(factory => factory.Create<PrimitiveEntity>(TestObject), Times.Once);
        }

        [TestMethod]
        public void OnlyBuildsDefinitionOnce()
        {
            TestObject.Clone(TestEntity);

            TestObject.Clone(TestEntity);

            MockDefinitionFactory.Verify(factory => factory.Create<PrimitiveEntity>(TestObject), Times.Once);
        }

        [DataTestMethod]
        [DataRow(10)]
        [DataRow(50)]
        [DataRow(100)]
        public async Task CanHandleConcurrentRequests(int cloneRequests)
        {
            List<Task> cloneTasks = new List<Task>();
            for (int i = 0; i < cloneRequests; i++)
                cloneTasks.Add(RequestClone());
            Parallel.ForEach(cloneTasks, (task) => task.Start());

            await Task.WhenAll(cloneTasks);

            MockDefinitionFactory.Verify(factory => factory.Create<PrimitiveEntity>(TestObject), Times.Once);
            MockDefinition.Verify(definition => definition.Clone(TestEntity), Times.Exactly(cloneRequests));
        }
        private Task RequestClone() => new Task(() => TestObject.Clone(TestEntity));

    }
}
