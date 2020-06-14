using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq.Expressions;

namespace Curds.Persistence.ExpressionNodes.Template
{
    using Domain;
    using Persistence.Abstraction;

    public abstract class BaseExpressionNodeTemplate<TExpression>
        where TExpression : Expression
    {
        protected object TestVisitReturn = new object();

        protected Mock<IExpressionNodeFactory> MockNodeFactory = new Mock<IExpressionNodeFactory>();
        protected Mock<IExpressionVisitor<object>> MockVisitor = new Mock<IExpressionVisitor<object>>();

        protected abstract TExpression TestExpression { get; }

        protected abstract BaseExpressionNode<TExpression> BaseExpressionNodeTemplateTestObject { get; }

        [TestMethod]
        public void SetsSourceExpression()
        {
            Assert.AreSame(TestExpression, BaseExpressionNodeTemplateTestObject.SourceExpression);
        }

        [TestMethod]
        public void AcceptsVisitorProperly()
        {
            BaseExpressionNodeTemplateTestObject.AcceptVisitor(MockVisitor.Object);
        }
        protected abstract void VerifyAcceptVisitorWasProper();

        [TestMethod]
        public void ReturnsVisitReturn()
        {
            SetupVisitReturn();

            object actual = BaseExpressionNodeTemplateTestObject.AcceptVisitor(MockVisitor.Object);

            Assert.AreSame(TestVisitReturn, actual);
        }
        protected abstract void SetupVisitReturn();
    }
}
