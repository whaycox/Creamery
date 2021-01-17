using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Curds.Persistence.Tests
{
    using Abstraction;
    using Domain;
    using Expressions.Abstraction;
    using Implementation;

    [TestClass]
    public class EntityUpdateDelegateFactoryTest
    {
        private TestSimpleEntity TestEntity = new TestSimpleEntity();
        private PropertyInfo TestIDProperty = typeof(ISimpleEntity).GetProperty(nameof(ISimpleEntity.ID));
        private int TestIDValue = 7;
        private ParameterExpression TestEntityParameter = Expression.Parameter(typeof(TestSimpleEntity));
        private ParameterExpression TestValueParameter = Expression.Parameter(typeof(int));

        private Mock<IExpressionBuilderFactory> MockExpressionBuilderFactory = new Mock<IExpressionBuilderFactory>();
        private Mock<IExpressionBuilder> MockExpressionBuilder = new Mock<IExpressionBuilder>();
        private Mock<Action<TestSimpleEntity, int>> MockUpdateDelegate = new Mock<Action<TestSimpleEntity, int>>();

        private EntityUpdateDelegateFactory TestObject = null;

        [TestInitialize]
        public void Init()
        {
            MockExpressionBuilderFactory
                .Setup(factory => factory.Create())
                .Returns(MockExpressionBuilder.Object);
            MockExpressionBuilder
                .Setup(builder => builder.AddParameter<TestSimpleEntity>(It.IsAny<string>()))
                .Returns(TestEntityParameter);
            MockExpressionBuilder
                .Setup(builder => builder.AddParameter<int>(It.IsAny<string>()))
                .Returns(TestValueParameter);
            MockExpressionBuilder
                .Setup(builder => builder.Build<Action<TestSimpleEntity, int>>())
                .Returns(MockUpdateDelegate.Object);

            TestObject = new EntityUpdateDelegateFactory(MockExpressionBuilderFactory.Object);
        }

        private Action<TestSimpleEntity> CreateTestDelegate(int testValue)
        {
            return TestObject.Create<TestSimpleEntity, int>(TestIDProperty, testValue);
        }

        [TestMethod]
        public void CreatingNewTypeAndValueBuildsExpressionBuilder()
        {
            CreateTestDelegate(TestIDValue);

            MockExpressionBuilderFactory.Verify(factory => factory.Create(), Times.Once);
        }

        [TestMethod]
        public void AddsEntityParameterToExpression()
        {
            CreateTestDelegate(TestIDValue);

            MockExpressionBuilder.Verify(builder => builder.AddParameter<TestSimpleEntity>(It.IsNotNull<string>()), Times.Once);
        }

        [TestMethod]
        public void AddsValueParameterToExpression()
        {
            CreateTestDelegate(TestIDValue);

            MockExpressionBuilder.Verify(builder => builder.AddParameter<int>(It.IsNotNull<string>()), Times.Once);
        }

        [TestMethod]
        public void SetsEntityPropertyToValue()
        {
            CreateTestDelegate(TestIDValue);

            MockExpressionBuilder.Verify(builder => builder.SetProperty(
                TestEntityParameter,
                TestIDProperty,
                TestValueParameter), Times.Once);
        }

        [TestMethod]
        public void BuildsDelegateFromBuilder()
        {
            CreateTestDelegate(TestIDValue);

            MockExpressionBuilder.Verify(builder => builder.Build<Action<TestSimpleEntity, int>>(), Times.Once);
        }

        [TestMethod]
        public void CreatedDelegateInvokesBuiltExpression()
        {
            Action<TestSimpleEntity> actual = CreateTestDelegate(TestIDValue);
            actual(TestEntity);

            MockUpdateDelegate.Verify(updateDelegate => updateDelegate(TestEntity, TestIDValue));
        }

        [TestMethod]
        public void CanBuildDifferentDelegatesWithDifferentValues()
        {
            Action<TestSimpleEntity> firstDelegate = CreateTestDelegate(TestIDValue);
            Action<TestSimpleEntity> secondDelegate = CreateTestDelegate(int.MinValue);

            firstDelegate(TestEntity);
            secondDelegate(TestEntity);

            MockUpdateDelegate.Verify(updateDelegate => updateDelegate(TestEntity, TestIDValue), Times.Once);
            MockUpdateDelegate.Verify(updateDelegate => updateDelegate(TestEntity, int.MinValue), Times.Once);
        }

        [TestMethod]
        public void CreatingSamePropertyDifferentValueDoesntBuildExpression()
        {
            CreateTestDelegate(TestIDValue);
            CreateTestDelegate(int.MinValue);

            MockExpressionBuilderFactory.Verify(factory => factory.Create(), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CreatingForMismatchingPropertyThrows()
        {
            PropertyInfo invalidProperty = typeof(EntityUpdateDelegateFactoryTest).GetProperty(nameof(InvalidProperty));

            TestObject.Create<TestSimpleEntity, int>(invalidProperty, TestIDValue);
        }
        public int InvalidProperty { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        [DataTestMethod]
        [DataRow(10)]
        [DataRow(50)]
        [DataRow(100)]
        public async Task CanHandleConcurrentRequests(int delegateRequests)
        {
            List<Task> delegateTasks = new List<Task>();
            for (int i = 0; i < delegateRequests; i++)
                delegateTasks.Add(RequestDelegate(i));
            Parallel.ForEach(delegateTasks, (task) => task.Start());

            await Task.WhenAll(delegateTasks);

            MockExpressionBuilderFactory.Verify(factory => factory.Create(), Times.Once);
        }
        private Task RequestDelegate(int index) => new Task(() => CreateTestDelegate(index));
    }
}
