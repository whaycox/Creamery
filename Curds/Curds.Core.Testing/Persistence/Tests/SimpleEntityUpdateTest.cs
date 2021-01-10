using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
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
    public class SimpleEntityUpdateTest
    {
        private TestSimpleEntity TestEntity = new TestSimpleEntity();
        private PropertyInfo TestParsedProperty = typeof(TestSimpleEntity).GetProperty(nameof(TestSimpleEntity.ID));
        private Expression<Func<TestSimpleEntity, int>> TestSetExpression = (entity) => entity.ID;
        private int TestSetValue = 10;

        private Mock<IExpressionParser> MockExpressionParser = new Mock<IExpressionParser>();
        private Mock<IEntityUpdateDelegateFactory> MockUpdateDelegateFactory = new Mock<IEntityUpdateDelegateFactory>();
        private Mock<Action<TestSimpleEntity>> MockUpdateDelegate = new Mock<Action<TestSimpleEntity>>();

        private SimpleEntityUpdate<TestSimpleEntity> TestObject = null;

        [TestInitialize]
        public void Init()
        {
            MockExpressionParser
                .Setup(parser => parser.ParsePropertyExpression(It.IsAny<Expression<Func<TestSimpleEntity, int>>>()))
                .Returns(TestParsedProperty);
            MockUpdateDelegateFactory
                .Setup(factory => factory.Create<TestSimpleEntity, int>(It.IsAny<PropertyInfo>(), It.IsAny<int>()))
                .Returns(MockUpdateDelegate.Object);

            TestObject = new SimpleEntityUpdate<TestSimpleEntity>(
                TestEntity,
                MockExpressionParser.Object,
                MockUpdateDelegateFactory.Object);
        }

        [TestMethod]
        public void SetParsesExpression()
        {
            TestObject.Set(TestSetExpression, TestSetValue);

            MockExpressionParser.Verify(parser => parser.ParsePropertyExpression(TestSetExpression), Times.Once);
        }

        [TestMethod]
        public void SetBuildsUpdateDelegate()
        {
            TestObject.Set(TestSetExpression, TestSetValue);

            MockUpdateDelegateFactory.Verify(factory => factory.Create<TestSimpleEntity, int>(TestParsedProperty, TestSetValue), Times.Once);
        }

        [TestMethod]
        public void SetReturnsItself()
        {
            IEntityUpdate<TestSimpleEntity> actual = TestObject.Set(TestSetExpression, TestSetValue);

            Assert.AreSame(TestObject, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ParsingAReadOnlyPropertyThrows()
        {
            PropertyInfo testProperty = typeof(TestSimpleEntity).GetProperty(nameof(TestSimpleEntity.ReadOnlyProperty));
            MockExpressionParser
                .Setup(parser => parser.ParsePropertyExpression(It.IsAny<Expression<Func<TestSimpleEntity, int>>>()))
                .Returns(testProperty);

            TestObject.Set(TestSetExpression, TestSetValue);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(10)]
        public async Task ExecuteInvokesUpdateDelegates(int sets)
        {
            for (int i = 0; i < sets; i++)
                TestObject.Set(TestSetExpression, TestSetValue);

            await TestObject.Execute();

            MockUpdateDelegate.Verify(updateDelegate => updateDelegate(TestEntity), Times.Exactly(sets));
        }
    }
}
