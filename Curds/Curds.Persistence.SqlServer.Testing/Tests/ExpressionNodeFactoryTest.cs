using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;
using System.Reflection;
using Whey;

namespace Curds.Persistence.Tests
{
    using Abstraction;
    using Domain;
    using ExpressionNodes.Domain;
    using Implementation;

    [TestClass]
    public class ExpressionNodeFactoryTest
    {
        private Expression TestConstantZero = Expression.Constant(0);
        private Expression TestConstantOne = Expression.Constant(1);
        private ParameterExpression TestParameterExpression = Expression.Parameter(typeof(string), nameof(TestParameterExpression));
        private PropertyInfo TestLengthProperty = typeof(string).GetProperty(nameof(string.Length));

        private ExpressionNodeFactory TestObject = new ExpressionNodeFactory();

        [TestMethod]
        [ExpectedException(typeof(InvalidExpressionException))]
        public void UnsupportedExpressionThrows()
        {
            Expression testExpression = Expression.Throw(null);

            TestObject.Build(testExpression);
        }

        [TestMethod]
        public void ConstantExpressionIsExpected()
        {
            IExpressionNode actual = TestObject.Build(TestConstantOne);

            ConstantNode actualNode = actual.VerifyIsActually<ConstantNode>();
            Assert.AreSame(TestConstantOne, actualNode.SourceExpression);
        }

        [TestMethod]
        public void ParameterExpressionIsExpected()
        {
            IExpressionNode actual = TestObject.Build(TestParameterExpression);

            ParameterNode actualNode = actual.VerifyIsActually<ParameterNode>();
            Assert.AreSame(TestParameterExpression, actualNode.SourceExpression);
        }

        private Expression TestConvertExpression => Expression.Convert(TestConstantOne, typeof(object));

        [TestMethod]
        public void ConvertExpressionIsExpected()
        {
            Expression testExpression = TestConvertExpression;

            IExpressionNode actual = TestObject.Build(testExpression);

            ConvertNode actualNode = actual.VerifyIsActually<ConvertNode>();
            Assert.AreSame(testExpression, actualNode.SourceExpression);
        }

        [TestMethod]
        public void ConvertExpressionHasExpectedOperand()
        {
            IExpressionNode actual = TestObject.Build(TestConvertExpression);

            ConvertNode actualNode = actual.VerifyIsActually<ConvertNode>();
            ConstantNode operandNode = actualNode.Operand.VerifyIsActually<ConstantNode>();
            Assert.AreSame(TestConstantOne, operandNode.SourceExpression);
        }

        private Expression TestLambdaExpression => Expression.Lambda(TestConstantZero, TestParameterExpression);

        [TestMethod]
        public void LambdaExpressionIsExpected()
        {
            Expression testExpression = TestLambdaExpression;

            IExpressionNode actual = TestObject.Build(testExpression);

            LambdaNode actualNode = actual.VerifyIsActually<LambdaNode>();
            Assert.AreSame(testExpression, actualNode.SourceExpression);
        }

        [TestMethod]
        public void LambdaExpressionBodyIsExpected()
        {
            IExpressionNode actual = TestObject.Build(TestLambdaExpression);

            LambdaNode actualNode = actual.VerifyIsActually<LambdaNode>();
            ConstantNode bodyNode = actualNode.Body.VerifyIsActually<ConstantNode>();
            Assert.AreSame(TestConstantZero, bodyNode.SourceExpression);
        }

        private Expression TestMemberAccessExpression => Expression.MakeMemberAccess(TestParameterExpression, TestLengthProperty);

        [TestMethod]
        public void MemberAccessExpressionIsExpected()
        {
            Expression testExpression = TestMemberAccessExpression;

            IExpressionNode actual = TestObject.Build(testExpression);

            MemberAccessNode actualNode = actual.VerifyIsActually<MemberAccessNode>();
            Assert.AreSame(testExpression, actualNode.SourceExpression);
        }

        [TestMethod]
        public void MemberAccessExpressionHasExpectedExpression()
        {
            IExpressionNode actual = TestObject.Build(TestMemberAccessExpression);

            MemberAccessNode actualNode = actual.VerifyIsActually<MemberAccessNode>();
            ParameterNode expressionNode = actualNode.Expression.VerifyIsActually<ParameterNode>();
            Assert.AreSame(TestParameterExpression, expressionNode.SourceExpression);
        }

        private Expression TestEqualExpression => Expression.Equal(TestConstantZero, TestConstantOne);

        [TestMethod]
        public void EqualExpressionIsExpected()
        {
            Expression testExpression = TestEqualExpression;

            IExpressionNode actual = TestObject.Build(testExpression);

            EqualNode actualNode = actual.VerifyIsActually<EqualNode>();
            Assert.AreSame(testExpression, actualNode.SourceExpression);
        }

        [TestMethod]
        public void EqualExpressionHasExpectedLeft()
        {
            IExpressionNode actual = TestObject.Build(TestEqualExpression);

            EqualNode actualNode = actual.VerifyIsActually<EqualNode>();
            ConstantNode leftNode = actualNode.Left.VerifyIsActually<ConstantNode>();
            Assert.AreSame(TestConstantZero, leftNode.SourceExpression);
        }

        [TestMethod]
        public void EqualExpressionHasExpectedRight()
        {
            IExpressionNode actual = TestObject.Build(TestEqualExpression);

            EqualNode actualNode = actual.VerifyIsActually<EqualNode>();
            ConstantNode rightNode = actualNode.Right.VerifyIsActually<ConstantNode>();
            Assert.AreSame(TestConstantOne, rightNode.SourceExpression);
        }

        private Expression TestNotEqualExpression => Expression.NotEqual(TestConstantZero, TestConstantOne);

        [TestMethod]
        public void NotEqualExpressionIsExpected()
        {
            Expression testExpression = TestNotEqualExpression;

            IExpressionNode actual = TestObject.Build(testExpression);

            NotEqualNode actualNode = actual.VerifyIsActually<NotEqualNode>();
            Assert.AreSame(testExpression, actualNode.SourceExpression);
        }

        [TestMethod]
        public void NotEqualExpressionHasExpectedLeft()
        {
            IExpressionNode actual = TestObject.Build(TestNotEqualExpression);

            NotEqualNode actualNode = actual.VerifyIsActually<NotEqualNode>();
            ConstantNode leftNode = actualNode.Left.VerifyIsActually<ConstantNode>();
            Assert.AreSame(TestConstantZero, leftNode.SourceExpression);
        }

        [TestMethod]
        public void NotEqualExpressionHasExpectedRight()
        {
            IExpressionNode actual = TestObject.Build(TestNotEqualExpression);

            NotEqualNode actualNode = actual.VerifyIsActually<NotEqualNode>();
            ConstantNode rightNode = actualNode.Right.VerifyIsActually<ConstantNode>();
            Assert.AreSame(TestConstantOne, rightNode.SourceExpression);
        }

        private Expression TestLessThanExpression => Expression.LessThan(TestConstantZero, TestConstantOne);

        [TestMethod]
        public void LessThanExpressionIsExpected()
        {
            Expression testExpression = TestLessThanExpression;

            IExpressionNode actual = TestObject.Build(testExpression);

            LessThanNode actualNode = actual.VerifyIsActually<LessThanNode>();
            Assert.AreSame(testExpression, actualNode.SourceExpression);
        }

        [TestMethod]
        public void LessThanExpressionHasExpectedLeft()
        {
            IExpressionNode actual = TestObject.Build(TestLessThanExpression);

            LessThanNode actualNode = actual.VerifyIsActually<LessThanNode>();
            ConstantNode leftNode = actualNode.Left.VerifyIsActually<ConstantNode>();
            Assert.AreSame(TestConstantZero, leftNode.SourceExpression);
        }

        [TestMethod]
        public void LessThanExpressionHasExpectedRight()
        {
            IExpressionNode actual = TestObject.Build(TestLessThanExpression);

            LessThanNode actualNode = actual.VerifyIsActually<LessThanNode>();
            ConstantNode rightNode = actualNode.Right.VerifyIsActually<ConstantNode>();
            Assert.AreSame(TestConstantOne, rightNode.SourceExpression);
        }

        private Expression TestLessThanOrEqualExpression => Expression.LessThanOrEqual(TestConstantZero, TestConstantOne);

        [TestMethod]
        public void LessThanOrEqualExpressionIsExpected()
        {
            Expression testExpression = TestLessThanOrEqualExpression;

            IExpressionNode actual = TestObject.Build(testExpression);

            LessThanOrEqualNode actualNode = actual.VerifyIsActually<LessThanOrEqualNode>();
            Assert.AreSame(testExpression, actualNode.SourceExpression);
        }

        [TestMethod]
        public void LessThanOrEqualExpressionHasExpectedLeft()
        {
            IExpressionNode actual = TestObject.Build(TestLessThanOrEqualExpression);

            LessThanOrEqualNode actualNode = actual.VerifyIsActually<LessThanOrEqualNode>();
            ConstantNode leftNode = actualNode.Left.VerifyIsActually<ConstantNode>();
            Assert.AreSame(TestConstantZero, leftNode.SourceExpression);
        }

        [TestMethod]
        public void LessThanOrEqualExpressionHasExpectedRight()
        {
            IExpressionNode actual = TestObject.Build(TestLessThanOrEqualExpression);

            LessThanOrEqualNode actualNode = actual.VerifyIsActually<LessThanOrEqualNode>();
            ConstantNode rightNode = actualNode.Right.VerifyIsActually<ConstantNode>();
            Assert.AreSame(TestConstantOne, rightNode.SourceExpression);
        }

        private Expression TestModuloExpression => Expression.Modulo(TestConstantZero, TestConstantOne);

        [TestMethod]
        public void ModuloExpressionIsExpected()
        {
            Expression testExpression = TestModuloExpression;

            IExpressionNode actual = TestObject.Build(testExpression);

            ModuloNode actualNode = actual.VerifyIsActually<ModuloNode>();
            Assert.AreSame(testExpression, actualNode.SourceExpression);
        }

        [TestMethod]
        public void ModuloExpressionHasExpectedLeft()
        {
            IExpressionNode actual = TestObject.Build(TestModuloExpression);

            ModuloNode actualNode = actual.VerifyIsActually<ModuloNode>();
            ConstantNode leftNode = actualNode.Left.VerifyIsActually<ConstantNode>();
            Assert.AreSame(TestConstantZero, leftNode.SourceExpression);
        }

        [TestMethod]
        public void ModuloExpressionHasExpectedRight()
        {
            IExpressionNode actual = TestObject.Build(TestModuloExpression);

            ModuloNode actualNode = actual.VerifyIsActually<ModuloNode>();
            ConstantNode rightNode = actualNode.Right.VerifyIsActually<ConstantNode>();
            Assert.AreSame(TestConstantOne, rightNode.SourceExpression);
        }
    }
}
