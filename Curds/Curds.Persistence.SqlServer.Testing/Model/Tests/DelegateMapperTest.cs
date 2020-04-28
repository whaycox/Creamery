using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Curds.Persistence.Model.Tests
{
    using Abstraction;
    using Configuration.Abstraction;
    using Implementation;
    using Persistence.Abstraction;
    using Persistence.Domain;
    using Configuration.Domain;

    [TestClass]
    public class DelegateMapperTest
    {
        private TestEntity TestInputEntity = new TestEntity();
        private int TestID = 124;
        private string TestName = nameof(TestName);
        private List<PropertyInfo> TestValueTypes = new List<PropertyInfo>();
        private PropertyInfo TestIDProperty = typeof(TestEntity).GetProperty(nameof(TestEntity.ID));
        private PropertyInfo TestNameProperty = typeof(TestEntity).GetProperty(nameof(TestEntity.Name));
        private CompiledConfiguration<ITestDataModel> TestCompiledConfiguration = new CompiledConfiguration<ITestDataModel>(typeof(TestEntity));
        private CompiledColumnConfiguration<ITestDataModel> TestIdentityColumnConfiguration = null;

        private Mock<IValueExpressionBuilder> MockValueExpressionBuilder = new Mock<IValueExpressionBuilder>();
        private Mock<IAssignIdentityExpressionBuilder> MockAssignIdentityExpressionBuilder = new Mock<IAssignIdentityExpressionBuilder>();
        private Mock<IProjectEntityExpressionBuilder> MockProjectEntityExpressionBuilder = new Mock<IProjectEntityExpressionBuilder>();
        private Mock<ITypeMapper> MockTypeMapper = new Mock<ITypeMapper>();
        private Mock<IModelConfigurationFactory> MockConfigurationFactory = new Mock<IModelConfigurationFactory>();
        private Mock<ValueEntityDelegate> MockValueEntityDelegate = new Mock<ValueEntityDelegate>();
        private Mock<AssignIdentityDelegate> MockAssignIdentityDelegate = new Mock<AssignIdentityDelegate>();

        private DelegateMapper TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestInputEntity.ID = TestID;
            TestInputEntity.Name = TestName;
            TestValueTypes.Add(TestIDProperty);
            TestValueTypes.Add(TestNameProperty);
            TestIdentityColumnConfiguration = new CompiledColumnConfiguration<ITestDataModel>(TestIDProperty.Name) { IsIdentity = true };

            MockTypeMapper
                .Setup(mapper => mapper.ValueTypes(It.IsAny<Type>()))
                .Returns(TestValueTypes);
            MockConfigurationFactory
                .Setup(factory => factory.Build<ITestDataModel>(It.IsAny<Type>()))
                .Returns(TestCompiledConfiguration);
            MockValueExpressionBuilder
                .Setup(builder => builder.BuildValueEntityDelegate(It.IsAny<Type>(), It.IsAny<IEnumerable<PropertyInfo>>()))
                .Returns(MockValueEntityDelegate.Object);
            MockAssignIdentityExpressionBuilder
                .Setup(builder => builder.BuildAssignIdentityDelegate(It.IsAny<Type>(), It.IsAny<PropertyInfo>()))
                .Returns(MockAssignIdentityDelegate.Object);

            TestObject = new DelegateMapper(
                MockValueExpressionBuilder.Object,
                MockAssignIdentityExpressionBuilder.Object,
                MockProjectEntityExpressionBuilder.Object,
                MockTypeMapper.Object,
                MockConfigurationFactory.Object);
        }

        [TestMethod]
        public void ValueEntityDelegateRetrievesConfigForEntity()
        {
            TestObject.MapValueEntityDelegate<ITestDataModel>(typeof(TestEntity));

            MockConfigurationFactory.Verify(factory => factory.Build<ITestDataModel>(typeof(TestEntity)), Times.Once);
        }

        [TestMethod]
        public void ValueEntityDelegateRetrievesValueTypesFromMapper()
        {
            TestObject.MapValueEntityDelegate<ITestDataModel>(typeof(TestEntity));

            MockTypeMapper.Verify(factory => factory.ValueTypes(typeof(TestEntity)), Times.Once);
        }

        [TestMethod]
        public void ValueEntityDelegateGetsFromExpressionBuilder()
        {
            TestObject.MapValueEntityDelegate<ITestDataModel>(typeof(TestEntity));

            MockValueExpressionBuilder.Verify(builder => builder.BuildValueEntityDelegate(typeof(TestEntity), TestValueTypes), Times.Once);
        }

        [TestMethod]
        public void ValueEntityDelegateDoesntIncludeIdentityValue()
        {
            CompiledColumnConfiguration<ITestDataModel> identityColumn = new CompiledColumnConfiguration<ITestDataModel>(nameof(TestEntity.ID)) { IsIdentity = true };
            TestCompiledConfiguration.Columns.Add(nameof(TestEntity.ID), identityColumn);

            TestObject.MapValueEntityDelegate<ITestDataModel>(typeof(TestEntity));

            MockValueExpressionBuilder
                .Verify(builder => builder.BuildValueEntityDelegate(typeof(TestEntity), It.Is<IEnumerable<PropertyInfo>>(properties => properties.Count() == 1)));
        }

        [TestMethod]
        public void ValueEntityDelegateReturnsFromExpressionBuilder()
        {
            ValueEntityDelegate actual = TestObject.MapValueEntityDelegate<ITestDataModel>(typeof(TestEntity));

            Assert.AreSame(MockValueEntityDelegate.Object, actual);
        }

        [TestMethod]
        public void AssignIdentityDelegateRetrievesConfigForEntity()
        {
            TestCompiledConfiguration.Columns.Add(TestIdentityColumnConfiguration.ValueName, TestIdentityColumnConfiguration);

            TestObject.MapAssignIdentityDelegate<ITestDataModel>(typeof(TestEntity));

            MockConfigurationFactory.Verify(factory => factory.Build<ITestDataModel>(typeof(TestEntity)), Times.Once);
        }

        [TestMethod]
        public void AssignIdentityDelegatePassesIdentityPropertyToBuilder()
        {
            TestCompiledConfiguration.Columns.Add(TestIdentityColumnConfiguration.ValueName, TestIdentityColumnConfiguration);

            TestObject.MapAssignIdentityDelegate<ITestDataModel>(typeof(TestEntity));

            MockAssignIdentityExpressionBuilder.Verify(builder => builder.BuildAssignIdentityDelegate(typeof(TestEntity), TestIDProperty), Times.Once);
        }

        [TestMethod]
        public void AssignIdentityDelegateReturnsFromExpressionBuilder()
        {
            TestCompiledConfiguration.Columns.Add(TestIdentityColumnConfiguration.ValueName, TestIdentityColumnConfiguration);

            AssignIdentityDelegate actual = TestObject.MapAssignIdentityDelegate<ITestDataModel>(typeof(TestEntity));

            Assert.AreSame(MockAssignIdentityDelegate.Object, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AssignIdentityDelegateThrowsWhenNone()
        {
            TestObject.MapAssignIdentityDelegate<ITestDataModel>(typeof(TestEntity));
        }
    }
}
