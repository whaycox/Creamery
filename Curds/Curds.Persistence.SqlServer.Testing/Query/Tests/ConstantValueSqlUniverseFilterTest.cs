using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Curds.Persistence.Query.Tests
{
    using Abstraction;
    using Domain;
    using Implementation;

    [TestClass]
    public class ConstantValueSqlUniverseFilterTest
    {
        private SqlBooleanOperation TestOperation = SqlBooleanOperation.Equals;
        private object TestValue = new object();

        private Mock<ISqlQueryTokenFactory> MockTokenFactory = new Mock<ISqlQueryTokenFactory>();
        private Mock<ISqlColumn> MockColumn = new Mock<ISqlColumn>();

        private ConstantValueSqlUniverseFilter TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestObject = new ConstantValueSqlUniverseFilter(TestOperation, MockColumn.Object, TestValue);
        }

        [TestMethod]
        public void LeftBuildsColumnNameToken()
        {
            TestObject.Left(MockTokenFactory.Object);

            MockTokenFactory.Verify(factory => factory.QualifiedObjectName(MockColumn.Object), Times.Once);
        }

        [TestMethod]
        public void RightBuildsParameterForColumnName()
        {
            MockColumn
                .Setup(column => column.Name)
                .Returns(nameof(RightBuildsParameterForColumnName));
            TestObject.Right(MockTokenFactory.Object);

            MockTokenFactory.Verify(factory => factory.Parameter(nameof(RightBuildsParameterForColumnName), TestValue), Times.Once);
        }
    }
}
