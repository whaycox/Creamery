using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Curds.Expressions.Tests
{
    using Implementation;

    [TestClass]
    public class ExpressionFactoryTest
    {
        private Expression TestBooleanExpression = Expression.Constant(true);
        private Expression TestExpressionOne = Expression.Constant(1);
        private Expression TestExpressionTwo = Expression.Constant(2);
        private Expression TestExpressionThree = Expression.Constant(3);
        private List<Expression> TestExpressions = new List<Expression>();
        private ParameterExpression TestParameterOne = Expression.Parameter(typeof(int), nameof(TestParameterOne));
        private ParameterExpression TestParameterTwo = Expression.Parameter(typeof(int), nameof(TestParameterTwo));
        private List<ParameterExpression> TestParameters = new List<ParameterExpression>();
        private ConstructorInfo TestConstructor = typeof(List<Expression>).GetConstructor(new[] { typeof(int) });
        private MethodInfo TestMethod = typeof(object).GetMethod(nameof(object.ToString));
        private LabelTarget TestLabel = Expression.Label(typeof(int), nameof(TestLabel));
        private LabelTarget TestVoidLabel = Expression.Label(nameof(TestVoidLabel));

        private ExpressionFactory TestObject = new ExpressionFactory();

        [TestInitialize]
        public void Init()
        {
            TestExpressions.Add(TestExpressionOne);
            TestExpressions.Add(TestExpressionTwo);
            TestExpressions.Add(TestExpressionThree);
            TestParameters.Add(TestParameterOne);
            TestParameters.Add(TestParameterTwo);
        }

        [TestMethod]
        public void ParameterIsExpected()
        {
            ParameterExpression actual = TestObject.Parameter<object>(nameof(ParameterIsExpected));

            Assert.AreEqual(nameof(ParameterIsExpected), actual.Name);
            Assert.AreEqual(typeof(object), actual.Type);
        }

        [TestMethod]
        public void VariableIsExpected()
        {
            ParameterExpression actual = TestObject.Variable<int>(nameof(VariableIsExpected));

            Assert.AreEqual(nameof(VariableIsExpected), actual.Name);
            Assert.AreEqual(typeof(int), actual.Type);
        }

        [TestMethod]
        public void BlockWithParamsIsExpected()
        {
            Expression actual = TestObject.Block(
                TestExpressionOne,
                TestExpressionTwo,
                TestExpressionThree);

            Assert.IsInstanceOfType(actual, typeof(BlockExpression));
            BlockExpression actualExpression = (BlockExpression)actual;
            CollectionAssert.AreEqual(TestExpressions, actualExpression.Expressions);
        }

        [TestMethod]
        public void BlockCollectionAndParamsIsExpected()
        {
            Expression actual = TestObject.Block(
                TestParameters,
                TestExpressionOne,
                TestExpressionTwo,
                TestExpressionThree);

            Assert.IsInstanceOfType(actual, typeof(BlockExpression));
            BlockExpression actualExpression = (BlockExpression)actual;
            CollectionAssert.AreEqual(TestParameters, actualExpression.Variables);
            CollectionAssert.AreEqual(TestExpressions, actualExpression.Expressions);
        }

        [TestMethod]
        public void BlockCollectionAndCollectionIsExpected()
        {
            Expression actual = TestObject.Block(TestParameters, TestExpressions);

            Assert.IsInstanceOfType(actual, typeof(BlockExpression));
            BlockExpression actualExpression = (BlockExpression)actual;
            CollectionAssert.AreEqual(TestParameters, actualExpression.Variables);
            CollectionAssert.AreEqual(TestExpressions, actualExpression.Expressions);
        }

        [TestMethod]
        public void NewIsExpected()
        {
            Expression actual = TestObject.New(TestConstructor, TestExpressionOne);

            Assert.IsInstanceOfType(actual, typeof(NewExpression));
            NewExpression actualExpression = (NewExpression)actual;
            Assert.AreEqual(TestConstructor, actualExpression.Constructor);
            CollectionAssert.AreEqual(new[] { TestExpressionOne }, actualExpression.Arguments);
        }

        [TestMethod]
        public void AssignIsExpected()
        {
            Expression actual = TestObject.Assign(TestParameterOne, TestExpressionOne);

            Assert.IsInstanceOfType(actual, typeof(BinaryExpression));
            BinaryExpression actualExpression = (BinaryExpression)actual;
            Assert.AreEqual(TestParameterOne, actualExpression.Left);
            Assert.AreEqual(TestExpressionOne, actualExpression.Right);
        }

        [TestMethod]
        public void ConvertIsExpected()
        {
            Expression actual = TestObject.Convert<long>(TestExpressionOne);

            Assert.IsInstanceOfType(actual, typeof(UnaryExpression));
            UnaryExpression actualExpression = (UnaryExpression)actual;
            Assert.AreEqual(typeof(long), actualExpression.Type);
            Assert.AreEqual(TestExpressionOne, actualExpression.Operand);
        }

        [TestMethod]
        public void CallIsExpected()
        {
            Expression actual = TestObject.Call(TestParameterOne, TestMethod);

            Assert.IsInstanceOfType(actual, typeof(MethodCallExpression));
            MethodCallExpression actualExpression = (MethodCallExpression)actual;
            Assert.AreEqual(TestParameterOne, actualExpression.Object);
            Assert.AreEqual(TestMethod, actualExpression.Method);
            Assert.AreEqual(0, actualExpression.Arguments.Count);
        }

        [TestMethod]
        public void LoopIsExpected()
        {
            Expression actual = TestObject.Loop(TestExpressionOne, TestLabel);

            Assert.IsInstanceOfType(actual, typeof(LoopExpression));
            LoopExpression actualExpression = (LoopExpression)actual;
            Assert.AreEqual(TestExpressionOne, actualExpression.Body);
            Assert.AreEqual(TestLabel, actualExpression.BreakLabel);
        }

        [TestMethod]
        public void LambdaIsExpected()
        {
            Expression actual = TestObject.Lambda<Func<int, int, int>>(TestExpressionOne, TestParameters);

            Assert.IsInstanceOfType(actual, typeof(LambdaExpression));
            LambdaExpression actualExpression = (LambdaExpression)actual;
            Assert.AreEqual(TestExpressionOne, actualExpression.Body);
            CollectionAssert.AreEqual(TestParameters, actualExpression.Parameters);
        }

        [TestMethod]
        public void IfThenElseIsExpected()
        {
            Expression actual = TestObject.IfThenElse(TestBooleanExpression, TestExpressionTwo, TestExpressionThree);

            Assert.IsInstanceOfType(actual, typeof(ConditionalExpression));
            ConditionalExpression actualExpression = (ConditionalExpression)actual;
            Assert.AreEqual(TestBooleanExpression, actualExpression.Test);
            Assert.AreEqual(TestExpressionTwo, actualExpression.IfTrue);
            Assert.AreEqual(TestExpressionThree, actualExpression.IfFalse);
        }

        [TestMethod]
        public void LessThanIsExpected()
        {
            Expression actual = TestObject.LessThan(TestExpressionOne, TestExpressionTwo);

            Assert.IsInstanceOfType(actual, typeof(BinaryExpression));
            BinaryExpression actualExpression = (BinaryExpression)actual;
            Assert.AreEqual(TestExpressionOne, actualExpression.Left);
            Assert.AreEqual(TestExpressionTwo, actualExpression.Right);
        }

        [TestMethod]
        public void PostIncrementAssignIsExpected()
        {
            Expression actual = TestObject.PostIncrementAssign(TestParameterOne);

            Assert.IsInstanceOfType(actual, typeof(UnaryExpression));
            UnaryExpression actualExpression = (UnaryExpression)actual;
            Assert.AreEqual(TestParameterOne, actualExpression.Operand);
        }

        [TestMethod]
        public void LabelNameIsExpected()
        {
            LabelTarget actual = TestObject.Label(nameof(LabelNameIsExpected));

            Assert.AreEqual(nameof(LabelNameIsExpected), actual.Name);
        }

        [TestMethod]
        public void LabelTypeIsExpected()
        {
            LabelTarget actual = TestObject.Label(typeof(int));

            Assert.AreEqual(typeof(int), actual.Type);
        }

        [TestMethod]
        public void LabelExpressionIsExpected()
        {
            Expression actual = TestObject.Label(TestLabel, TestExpressionOne);

            Assert.IsInstanceOfType(actual, typeof(LabelExpression));
            LabelExpression actualExpression = (LabelExpression)actual;
            Assert.AreEqual(TestLabel, actualExpression.Target);
            Assert.AreEqual(TestExpressionOne, actualExpression.DefaultValue);
        }

        [TestMethod]
        public void ReturnIsExpected()
        {
            Expression actual = TestObject.Return(TestLabel, TestExpressionOne);

            Assert.IsInstanceOfType(actual, typeof(GotoExpression));
            GotoExpression actualExpression = (GotoExpression)actual;
            Assert.AreEqual(TestLabel, actualExpression.Target);
            Assert.AreEqual(TestExpressionOne, actualExpression.Value);
        }

        [TestMethod]
        public void BreakIsExpected()
        {
            Expression actual = TestObject.Break(TestVoidLabel);

            Assert.IsInstanceOfType(actual, typeof(GotoExpression));
            GotoExpression actualExpression = (GotoExpression)actual;
            Assert.AreEqual(TestVoidLabel, actualExpression.Target);
            Assert.IsNull(actualExpression.Value);
        }
    }
}
