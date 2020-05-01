using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Curds.Persistence.Model.Tests
{
    using Abstraction;
    using Implementation;

    [TestClass]
    public class DelegateMapperTest
    {
        private Mock<IValueExpressionBuilder> MockValueExpressionBuilder = new Mock<IValueExpressionBuilder>();
        private Mock<IAssignIdentityExpressionBuilder> MockAssignIdentityExpressionBuilder = new Mock<IAssignIdentityExpressionBuilder>();
        private Mock<IProjectEntityExpressionBuilder> MockProjectEntityExpressionBuilder = new Mock<IProjectEntityExpressionBuilder>();
        private Mock<IEntityModel> MockEntityModel = new Mock<IEntityModel>();
        private Mock<ValueEntityDelegate> MockValueEntityDelegate = new Mock<ValueEntityDelegate>();
        private Mock<AssignIdentityDelegate> MockAssignIdentityDelegate = new Mock<AssignIdentityDelegate>();
        private Mock<ProjectEntityDelegate> MockProjectEntityDelegate = new Mock<ProjectEntityDelegate>();

        private DelegateMapper TestObject = null;

        [TestInitialize]
        public void Init()
        {
            MockValueExpressionBuilder
                .Setup(builder => builder.BuildValueEntityDelegate(It.IsAny<IEntityModel>()))
                .Returns(MockValueEntityDelegate.Object);
            MockAssignIdentityExpressionBuilder
                .Setup(builder => builder.BuildAssignIdentityDelegate(It.IsAny<IEntityModel>()))
                .Returns(MockAssignIdentityDelegate.Object);
            MockProjectEntityExpressionBuilder
                .Setup(builder => builder.BuildProjectEntityDelegate(It.IsAny<IEntityModel>()))
                .Returns(MockProjectEntityDelegate.Object);

            TestObject = new DelegateMapper(
                MockValueExpressionBuilder.Object,
                MockAssignIdentityExpressionBuilder.Object,
                MockProjectEntityExpressionBuilder.Object);
        }

        [TestMethod]
        public void ValueEntityDelegatePassesEntityModelToBuilder()
        {
            TestObject.MapValueEntityDelegate(MockEntityModel.Object);

            MockValueExpressionBuilder.Verify(builder => builder.BuildValueEntityDelegate(MockEntityModel.Object), Times.Once);
        }

        [TestMethod]
        public void ValueEntityDelegateReturnsFromExpressionBuilder()
        {
            ValueEntityDelegate actual = TestObject.MapValueEntityDelegate(MockEntityModel.Object);

            Assert.AreSame(MockValueEntityDelegate.Object, actual);
        }

        [TestMethod]
        public void AssignIdentityDelegatePassesEntityModelToBuilder()
        {
            TestObject.MapAssignIdentityDelegate(MockEntityModel.Object);

            MockAssignIdentityExpressionBuilder.Verify(builder => builder.BuildAssignIdentityDelegate(MockEntityModel.Object), Times.Once);
        }

        [TestMethod]
        public void AssignIdentityDelegateReturnsFromExpressionBuilder()
        {
            AssignIdentityDelegate actual = TestObject.MapAssignIdentityDelegate(MockEntityModel.Object);

            Assert.AreSame(MockAssignIdentityDelegate.Object, actual);
        }

        [TestMethod]
        public void ProjectEntityDelegatePassesEntityModelToBuilder()
        {
            TestObject.MapProjectEntityDelegate(MockEntityModel.Object);

            MockProjectEntityExpressionBuilder.Verify(builder => builder.BuildProjectEntityDelegate(MockEntityModel.Object), Times.Once);
        }

        [TestMethod]
        public void ProjectEntityDelegateReturnsFromExpressionBuilder()
        {
            ProjectEntityDelegate actual = TestObject.MapProjectEntityDelegate(MockEntityModel.Object);

            Assert.AreSame(MockProjectEntityDelegate.Object, actual);
        }
    }
}
