using Moq;
using System;
using System.Linq.Expressions;

namespace Curds.Persistence.Query.Tokens.Template
{
    using Query.Abstraction;

    public abstract class BaseSqlQueryTokenTemplate
    {
        protected Mock<ISqlQueryTokenFactory> MockTokenFactory = new Mock<ISqlQueryTokenFactory>();
        protected Mock<ISqlQueryToken> MockToken = new Mock<ISqlQueryToken>();
        protected Mock<ISqlQueryFormatVisitor> MockFormatVisitor = new Mock<ISqlQueryFormatVisitor>();

        protected void SetupTokenFactory(Expression<Func<ISqlQueryTokenFactory, ISqlQueryToken>> setupExpression)
        {
            MockTokenFactory
                .Setup(setupExpression)
                .Returns(MockToken.Object);
        }
    }
}
