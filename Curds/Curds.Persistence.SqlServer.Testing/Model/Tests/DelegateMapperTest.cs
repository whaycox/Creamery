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
    using Abstraction;
    using Persistence.Abstraction;
    using Query.Domain;
    using Configuration.Abstraction;

    [TestClass]
    public class DelegateMapperTest
    {
        private TestEntity TestInputEntity = new TestEntity();
        private int TestID = 124;
        private string TestName = nameof(TestName);
        private List<PropertyInfo> TestValueTypes = new List<PropertyInfo>();
        private PropertyInfo TestIDProperty = typeof(TestEntity).GetProperty(nameof(TestEntity.ID));
        private PropertyInfo TestNameProperty = typeof(TestEntity).GetProperty(nameof(TestEntity.Name));

        private Mock<IValueExpressionBuilder> MockValueExpressionBuilder = new Mock<IValueExpressionBuilder>();
        private Mock<ITypeMapper> MockTypeMapper = new Mock<ITypeMapper>();
        private Mock<IModelConfigurationFactory> MockConfigurationFactory = new Mock<IModelConfigurationFactory>();
        private Mock<IModelEntityConfiguration> MockConfiguration = new Mock<IModelEntityConfiguration>();

        private DelegateMapper TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestInputEntity.ID = TestID;
            TestInputEntity.Name = TestName;
            TestValueTypes.Add(TestIDProperty);
            TestValueTypes.Add(TestNameProperty);

            MockTypeMapper
                .Setup(mapper => mapper.ValueTypes(It.IsAny<Type>()))
                .Returns(TestValueTypes);
            MockConfigurationFactory
                .Setup(factory => factory.Build<ITestDataModel>(It.IsAny<Type>()))
                .Returns(MockConfiguration.Object);

            TestObject = new DelegateMapper(
                MockValueExpressionBuilder.Object,
                MockTypeMapper.Object,
                MockConfigurationFactory.Object);
        }

        [TestMethod]
        public void CanMapValueEntityDelegate()
        {
            TestObject.MapValueEntityDelegate<ITestDataModel>(typeof(TestEntity));
        }

        [TestMethod]
        public void ValueEntityDelegateWorksCorrectly()
        {
            ValueEntityDelegate valueEntityDelegate = TestObject.MapValueEntityDelegate<ITestDataModel>(typeof(TestEntity));

            ValueEntity actual = valueEntityDelegate(TestInputEntity);

            Assert.AreEqual(2, actual.Values.Count);
            Assert.IsInstanceOfType(actual.Values[0], typeof(IntValue));
            Assert.AreEqual(TestIDProperty.Name, actual.Values[0].Name);
            Assert.AreEqual(TestID, actual.Values[0].Content);
            Assert.IsInstanceOfType(actual.Values[1], typeof(StringValue));
            Assert.AreEqual(TestNameProperty.Name, actual.Values[1].Name);
            Assert.AreEqual(TestName, actual.Values[1].Content);
        }

        [TestMethod]
        public void ValueEntityDelegateDoesntIncludeIdentity()
        {
            MockConfiguration
                .Setup(config => config.Identity)
                .Returns(TestIDProperty.Name);
            ValueEntityDelegate valueEntityDelegate = TestObject.MapValueEntityDelegate<ITestDataModel>(typeof(TestEntity));

            ValueEntity actual = valueEntityDelegate(TestInputEntity);

            Assert.AreEqual(1, actual.Values.Count);
            Assert.IsInstanceOfType(actual.Values[0], typeof(StringValue));
            Assert.AreEqual(TestNameProperty.Name, actual.Values[0].Name);
            Assert.AreEqual(TestName, actual.Values[0].Content);
        }

    }
}
