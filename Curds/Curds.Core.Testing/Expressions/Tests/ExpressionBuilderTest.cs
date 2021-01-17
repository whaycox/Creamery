using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Curds.Expressions.Tests
{
    using Abstraction;
    using Curds.Domain;
    using Implementation;

    [TestClass]
    public class ExpressionBuilderTest
    {
        private Expression TestExpression = Expression.Constant(1);
        private ParameterExpression TestParameter = Expression.Parameter(typeof(int), nameof(TestParameter));
        private MethodInfo TestMethodInfo = typeof(object).GetMethod(nameof(object.ToString));
        private ConstructorInfo TestVoidConstructor = typeof(TestEntity).GetConstructor(new Type[0]);
        private ConstructorInfo TestIntConstructor = typeof(TestEntity).GetConstructor(new[] { typeof(int) });
        private PropertyInfo TestIntProperty = typeof(TestEntity).GetProperty(nameof(TestEntity.IntValue));
        private LabelTarget TestLabel = Expression.Label(nameof(TestLabel));
        private LabelTarget TestIntLabel = Expression.Label(typeof(int));
        private Expression TestDelegateExpression = Expression.Constant(2);
        private Expression<Func<int>> TestBuiltDelegate = null;
        private int TestReturnedInt = 100;

        private Mock<IExpressionFactory> MockExpressionFactory = new Mock<IExpressionFactory>();
        private Mock<Func<ParameterExpression, Expression>> MockContentExpressionDelegate = new Mock<Func<ParameterExpression, Expression>>();

        private ExpressionBuilder TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestBuiltDelegate = () => TestReturnedInt;

            MockExpressionFactory
                .Setup(factory => factory.Parameter<int>(It.IsAny<string>()))
                .Returns(TestParameter);
            MockExpressionFactory
                .Setup(factory => factory.Variable<TestEntity>(It.IsAny<string>()))
                .Returns(TestParameter);
            MockExpressionFactory
                .Setup(factory => factory.Variable<int>(It.IsAny<string>()))
                .Returns(TestParameter);
            MockExpressionFactory
                .Setup(factory => factory.New(It.IsAny<ConstructorInfo>(), It.IsAny<Expression[]>()))
                .Returns(TestExpression);
            MockExpressionFactory
                .Setup(factory => factory.Label(It.IsAny<string>()))
                .Returns(TestLabel);
            MockContentExpressionDelegate
                .Setup(del => del(It.IsAny<ParameterExpression>()))
                .Returns(TestDelegateExpression);
            MockExpressionFactory
                .Setup(factory => factory.Label(It.IsAny<Type>()))
                .Returns(TestIntLabel);
            MockExpressionFactory
                .Setup(factory => factory.Lambda<Func<int>>(It.IsAny<Expression>(), It.IsAny<IEnumerable<ParameterExpression>>()))
                .Returns(TestBuiltDelegate);

            TestObject = new ExpressionBuilder(MockExpressionFactory.Object);
        }

        [TestMethod]
        public void AddParameterBuildsExpression()
        {
            TestObject.AddParameter<int>(nameof(AddParameterBuildsExpression));

            MockExpressionFactory.Verify(factory => factory.Parameter<int>(nameof(AddParameterBuildsExpression)), Times.Once);
        }

        [TestMethod]
        public void AddParameterReturnsFactoryExpression()
        {
            ParameterExpression actual = TestObject.AddParameter<int>(nameof(AddParameterReturnsFactoryExpression));

            Assert.AreSame(TestParameter, actual);
        }

        [TestMethod]
        public void AddParameterAddsExpressionToParameters()
        {
            TestObject.AddParameter<int>(nameof(AddParameterAddsExpressionToParameters));

            Assert.AreEqual(1, TestObject.ParameterExpressions.Count);
            Assert.AreSame(TestParameter, TestObject.ParameterExpressions[0]);
        }

        [TestMethod]
        public void CreateObjectBuildsVariable()
        {
            TestObject.CreateObject<TestEntity>(nameof(CreateObjectBuildsVariable));

            MockExpressionFactory.Verify(factory => factory.Variable<TestEntity>(nameof(CreateObjectBuildsVariable)), Times.Once);
        }

        [TestMethod]
        public void CreateObjectAddsVariableToBody()
        {
            TestObject.CreateObject<TestEntity>(nameof(CreateObjectAddsVariableToBody));

            Assert.AreEqual(1, TestObject.BodyVariables.Count);
            Assert.AreSame(TestParameter, TestObject.BodyVariables[0]);
        }

        [TestMethod]
        public void CreateObjectBuildsNewExpression()
        {
            TestObject.CreateObject<TestEntity>(nameof(CreateObjectBuildsNewExpression));

            MockExpressionFactory.Verify(factory => factory.New(TestVoidConstructor, null), Times.Once);
        }

        [TestMethod]
        public void CreateObjectWithArgumentsBuildsNewExpression()
        {
            TestObject.CreateObject<TestEntity>(nameof(CreateObjectBuildsNewExpression), new[] { typeof(int) }, new[] { TestExpression });

            MockExpressionFactory.Verify(factory => factory.New(TestIntConstructor, TestExpression), Times.Once);
        }

        [TestMethod]
        public void CreateObjectBuildsAssignExpression()
        {
            TestObject.CreateObject<TestEntity>(nameof(CreateObjectBuildsAssignExpression));

            MockExpressionFactory.Verify(factory => factory.Assign(TestParameter, TestExpression), Times.Once);
        }

        [TestMethod]
        public void CreateObjectAddsAssignExpressionToBody()
        {
            Expression testAssignExpression = Mock.Of<Expression>();
            MockExpressionFactory
                .Setup(factory => factory.Assign(TestParameter, TestExpression))
                .Returns(testAssignExpression);

            TestObject.CreateObject<TestEntity>(nameof(CreateObjectAddsAssignExpressionToBody));

            Assert.AreEqual(1, TestObject.BodyExpressions.Count);
            Assert.AreSame(testAssignExpression, TestObject.BodyExpressions[0]);
        }

        [TestMethod]
        public void SetPropertyBuildsCallExpression()
        {
            TestObject.SetProperty(TestParameter, TestIntProperty, TestExpression);

            MockExpressionFactory.Verify(factory => factory.Call(TestParameter, TestIntProperty.SetMethod, TestExpression), Times.Once);
        }

        [TestMethod]
        public void SetPropertyAddsBuiltExpressionToBody()
        {
            Expression testCallExpression = Mock.Of<Expression>();
            MockExpressionFactory
                .Setup(factory => factory.Call(TestParameter, TestIntProperty.SetMethod, TestExpression))
                .Returns(testCallExpression);

            TestObject.SetProperty(TestParameter, TestIntProperty, TestExpression);

            Assert.AreEqual(1, TestObject.BodyExpressions.Count);
            Assert.AreSame(testCallExpression, TestObject.BodyExpressions[0]);
        }

        [TestMethod]
        public void ForBuildsIteratorVariable()
        {
            TestObject.For(TestExpression, MockContentExpressionDelegate.Object);

            MockExpressionFactory.Verify(factory => factory.Variable<int>(It.IsNotNull<string>()), Times.Once);
        }

        [TestMethod]
        public void ForBuildsBreakLabel()
        {
            TestObject.For(TestExpression, MockContentExpressionDelegate.Object);

            MockExpressionFactory.Verify(factory => factory.Label(It.IsNotNull<string>()), Times.Once);
        }

        [TestMethod]
        public void ForBuildsACallToCollectionCount()
        {
            TestObject.For(TestExpression, MockContentExpressionDelegate.Object);

            MockExpressionFactory.Verify(factory => factory.Call(TestExpression, TestObject.CollectionCountProperty.GetMethod), Times.Once);
        }

        [TestMethod]
        public void ForBuildsAComparisonOfIteratorAndCount()
        {
            Expression testCallExpression = Mock.Of<Expression>();
            MockExpressionFactory
                .Setup(factory => factory.Call(TestExpression, TestObject.CollectionCountProperty.GetMethod))
                .Returns(testCallExpression);

            TestObject.For(TestExpression, MockContentExpressionDelegate.Object);

            MockExpressionFactory.Verify(factory => factory.LessThan(TestParameter, testCallExpression), Times.Once);
        }

        [TestMethod]
        public void ForInvokesSuppliedDelegate()
        {
            TestObject.For(TestExpression, MockContentExpressionDelegate.Object);

            MockContentExpressionDelegate.Verify(del => del(TestParameter), Times.Once);
        }

        [TestMethod]
        public void ForBuildsPostIncrementAssign()
        {
            TestObject.For(TestExpression, MockContentExpressionDelegate.Object);

            MockExpressionFactory.Verify(factory => factory.PostIncrementAssign(TestParameter), Times.Once);
        }

        [TestMethod]
        public void ForBuildsBlockForLoopBody()
        {
            Expression testPostIncrementExpression = Mock.Of<Expression>();
            MockExpressionFactory
                .Setup(factory => factory.PostIncrementAssign(It.IsAny<Expression>()))
                .Returns(testPostIncrementExpression);

            TestObject.For(TestExpression, MockContentExpressionDelegate.Object);

            MockExpressionFactory.Verify(factory => factory.Block(TestDelegateExpression, testPostIncrementExpression));
        }

        [TestMethod]
        public void ForBuildsBreakExpression()
        {
            TestObject.For(TestExpression, MockContentExpressionDelegate.Object);

            MockExpressionFactory.Verify(factory => factory.Break(TestLabel), Times.Once);
        }

        [TestMethod]
        public void ForBuildsConditionalLoopBody()
        {
            Expression testLessThanExpression = Mock.Of<Expression>();
            MockExpressionFactory
                .Setup(factory => factory.LessThan(TestParameter, null))
                .Returns(testLessThanExpression);
            Expression testLoopBodyExpression = Mock.Of<Expression>();
            MockExpressionFactory
                .Setup(factory => factory.Block(TestDelegateExpression, null))
                .Returns(testLoopBodyExpression);
            Expression testBreakExpression = Mock.Of<Expression>();
            MockExpressionFactory
                .Setup(factory => factory.Break(TestLabel))
                .Returns(testBreakExpression);

            TestObject.For(TestExpression, MockContentExpressionDelegate.Object);

            MockExpressionFactory.Verify(factory => factory.IfThenElse(testLessThanExpression, testLoopBodyExpression, testBreakExpression), Times.Once);
        }

        [TestMethod]
        public void ForBuildsLoopWithConditional()
        {
            Expression testConditionalExpression = Mock.Of<Expression>();
            MockExpressionFactory
                .Setup(factory => factory.IfThenElse(null, null, null))
                .Returns(testConditionalExpression);

            TestObject.For(TestExpression, MockContentExpressionDelegate.Object);

            MockExpressionFactory.Verify(factory => factory.Loop(testConditionalExpression, TestLabel), Times.Once);
        }

        [TestMethod]
        public void ForBuildsBlockWithLoop()
        {
            Expression testLoopExpression = Mock.Of<Expression>();
            MockExpressionFactory
                .Setup(factory => factory.Loop(null, TestLabel))
                .Returns(testLoopExpression);

            TestObject.For(TestExpression, MockContentExpressionDelegate.Object);

            MockExpressionFactory.Verify(factory => factory.Block(new[] { TestParameter }, testLoopExpression), Times.Once);
        }

        [TestMethod]
        public void ForAddsBlockToBody()
        {
            Expression testLoopExpression = Mock.Of<Expression>();
            MockExpressionFactory
                .Setup(factory => factory.Loop(null, TestLabel))
                .Returns(testLoopExpression);
            Expression testLoopBlock = Mock.Of<Expression>();
            MockExpressionFactory
                .Setup(factory => factory.Block(new[] { TestParameter }, testLoopExpression))
                .Returns(testLoopBlock);

            TestObject.For(TestExpression, MockContentExpressionDelegate.Object);

            Assert.AreEqual(1, TestObject.BodyExpressions.Count);
            Assert.AreSame(testLoopBlock, TestObject.BodyExpressions[0]);
        }

        [TestMethod]
        public void ReturnObjectBuildsLabel()
        {
            TestObject.ReturnObject(TestParameter);

            MockExpressionFactory.Verify(factory => factory.Label(typeof(int)), Times.Once);
        }

        [TestMethod]
        public void ReturnObjectBuildsReturn()
        {
            TestObject.ReturnObject(TestParameter);

            MockExpressionFactory.Verify(factory => factory.Return(TestIntLabel, TestParameter), Times.Once);
        }

        [TestMethod]
        public void ReturnObjectAddsReturnToBody()
        {
            Expression testReturnExpression = Mock.Of<Expression>();
            MockExpressionFactory
                .Setup(factory => factory.Return(TestIntLabel, TestParameter))
                .Returns(testReturnExpression);

            TestObject.ReturnObject(TestParameter);

            Assert.AreEqual(2, TestObject.BodyExpressions.Count);
            Assert.AreSame(testReturnExpression, TestObject.BodyExpressions[0]);
        }

        [TestMethod]
        public void ReturnObjectBuildsLabelExpression()
        {
            TestObject.ReturnObject(TestParameter);

            MockExpressionFactory.Verify(factory => factory.Label(TestIntLabel, TestParameter), Times.Once);
        }

        [TestMethod]
        public void ReturnObjectAddsLabelExpressionToBody()
        {
            Expression testLabelExpression = Mock.Of<Expression>();
            MockExpressionFactory
                .Setup(factory => factory.Label(TestIntLabel, TestParameter))
                .Returns(testLabelExpression);

            TestObject.ReturnObject(TestParameter);

            Assert.AreEqual(2, TestObject.BodyExpressions.Count);
            Assert.AreSame(testLabelExpression, TestObject.BodyExpressions[1]);
        }

        [TestMethod]
        public void BuildBuildsBlockWithBody()
        {
            ParameterExpression testBodyVariable = Expression.Parameter(typeof(int), nameof(testBodyVariable));
            TestObject.BodyVariables.Add(testBodyVariable);
            Expression testBodyExpression = Mock.Of<Expression>();
            TestObject.BodyExpressions.Add(testBodyExpression);

            TestObject.Build<Func<int>>();

            MockExpressionFactory.Verify(factory => factory.Block(TestObject.BodyVariables, TestObject.BodyExpressions), Times.Once);
        }

        [TestMethod]
        public void BuildBuildsLambdaWithBlock()
        {
            ParameterExpression testParameterExpression = Expression.Parameter(typeof(int), nameof(testParameterExpression));
            TestObject.ParameterExpressions.Add(testParameterExpression);
            Expression testBuiltBlock = Mock.Of<Expression>();
            MockExpressionFactory
                .Setup(factory => factory.Block(TestObject.BodyVariables, TestObject.BodyExpressions))
                .Returns(testBuiltBlock);

            TestObject.Build<Func<int>>();

            MockExpressionFactory.Verify(factory => factory.Lambda<Func<int>>(testBuiltBlock, TestObject.ParameterExpressions), Times.Once);
        }

        [TestMethod]
        public void BuildReturnsCompiledExpression()
        {
            Func<int> testBuiltDelegate = TestObject.Build<Func<int>>();

            int actual = testBuiltDelegate();

            Assert.AreEqual(TestReturnedInt, actual);
        }
    }
}
