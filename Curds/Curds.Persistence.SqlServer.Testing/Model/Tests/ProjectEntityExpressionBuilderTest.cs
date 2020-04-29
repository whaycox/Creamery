using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using System.Reflection;

namespace Curds.Persistence.Model.Tests
{
    using Implementation;
    using Persistence.Domain;
    using Persistence.Abstraction;
    using Abstraction;
    using Query.Abstraction;

    [TestClass]
    public class ProjectEntityExpressionBuilderTest
    {
        private Type TestType = typeof(TestEntity);
        private PropertyInfo[] TestProperties = null;
        private int TestReadInt = 15;
        private string TestReadString = nameof(TestReadString);

        private Mock<ISqlQueryReader> MockQueryReader = new Mock<ISqlQueryReader>();

        private ProjectEntityExpressionBuilder TestObject = new ProjectEntityExpressionBuilder();

        [TestInitialize]
        public void Init()
        {
            TestProperties = new PropertyInfo[]
            {
                TestType.GetProperty(nameof(TestEntity.ID)),
                TestType.GetProperty(nameof(TestEntity.Name)),
            };

            MockQueryReader
                .Setup(reader => reader.ReadInt(It.IsAny<string>()))
                .Returns(TestReadInt);
            MockQueryReader
                .Setup(reader => reader.ReadString(It.IsAny<string>()))
                .Returns(TestReadString);
        }

        [TestMethod]
        public void CanBuildDelegate()
        {
            TestObject.BuildProjectEntityDelegate(TestType, TestProperties);
        }

        [TestMethod]
        public void BuiltDelegateReadsFromReader()
        {
            ProjectEntityDelegate<IEntity> projection = TestObject.BuildProjectEntityDelegate(TestType, TestProperties);

            projection(MockQueryReader.Object);

            MockQueryReader.Verify(reader => reader.ReadInt(nameof(TestEntity.ID)), Times.Once);
            MockQueryReader.Verify(reader => reader.ReadString(nameof(TestEntity.Name)), Times.Once);
        }

        [TestMethod]
        public void BuiltDelegateReturnsExpectedType()
        {
            ProjectEntityDelegate<IEntity> projection = TestObject.BuildProjectEntityDelegate(TestType, TestProperties);
            
            IEntity actual = projection(MockQueryReader.Object);

            Assert.IsInstanceOfType(actual, typeof(TestEntity));
        }

        [TestMethod]
        public void BuiltDelegateCanBeBoxedToType()
        {
            ProjectEntityDelegate<IEntity> projection = TestObject.BuildProjectEntityDelegate(TestType, TestProperties);
            ProjectEntityDelegate<TestEntity> boxed = projection as ProjectEntityDelegate<TestEntity>;

            TestEntity actual = boxed(MockQueryReader.Object);

            Assert.AreEqual(TestReadInt, actual.ID);
            Assert.AreEqual(TestReadString, actual.Name);
        }
    }
}
