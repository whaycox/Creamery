using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq.Expressions;

namespace Curds.Expressions.Nodes.Template
{
    using Domain;
    using Expressions.Abstraction;

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
    }
}
