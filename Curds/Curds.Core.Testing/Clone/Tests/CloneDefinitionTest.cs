using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Curds.Clone.Tests
{
    using Abstraction;
    using Domain;
    using Implementation;

    [TestClass]
    public class CloneDefinitionTest
    {
        private PrimitiveEntity TestEntity = new PrimitiveEntity();
        private PrimitiveEntity TestClonedEntity = new PrimitiveEntity();

        private Mock<ICloneFactory> MockCloneFactory = new Mock<ICloneFactory>();
        private Mock<CloneDelegate<PrimitiveEntity>> MockCloneDelegate = new Mock<CloneDelegate<PrimitiveEntity>>();

        private CloneDefinition<PrimitiveEntity> TestObject = null;

        [TestInitialize]
        public void Init()
        {
            MockCloneDelegate
                .Setup(cloneDelegate => cloneDelegate(It.IsAny<PrimitiveEntity>(), It.IsAny<ICloneFactory>()))
                .Returns(TestClonedEntity);

            TestObject = new CloneDefinition<PrimitiveEntity>(
                MockCloneFactory.Object,
                MockCloneDelegate.Object);
        }

        [TestMethod]
        public void CloneInvokesDelegate()
        {
            TestObject.Clone(TestEntity);

            MockCloneDelegate.Verify(cloneDelegate => cloneDelegate(TestEntity, MockCloneFactory.Object), Times.Once);
        }

        [TestMethod]
        public void CloneReturnsDelegateResult()
        {
            PrimitiveEntity actual = TestObject.Clone(TestEntity);

            Assert.AreSame(TestClonedEntity, actual);
        }
    }
}
