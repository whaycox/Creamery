using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;

namespace Curds.Persistence.Query.Template
{
    using Abstraction;
    using Domain;

    public class SqlQueryWriterTemplate : SqlQueryTemplate
    {
        protected Mock<ISqlQueryTokenFactory> MockTokenFactory = new Mock<ISqlQueryTokenFactory>();
        protected Mock<ISqlQueryToken> MockPhraseToken = new Mock<ISqlQueryToken>();

        protected List<ISqlQueryToken[]> SuppliedPhraseTokens = new List<ISqlQueryToken[]>();

        [TestInitialize]
        public void SetupSqlQueryWriterTemplate()
        {
            MockTokenFactory
                .Setup(factory => factory.Phrase(It.IsAny<ISqlQueryToken[]>()))
                .Callback<ISqlQueryToken[]>(tokens => SuppliedPhraseTokens.Add(tokens))
                .Returns(MockPhraseToken.Object);
        }

        protected ISqlQueryToken SetupFactory(Expression<Func<ISqlQueryTokenFactory, ISqlQueryToken>> setupExpression)
        {
            Mock<ISqlQueryToken> mockToken = new Mock<ISqlQueryToken>();
            MockTokenFactory
                .Setup(setupExpression)
                .Returns(mockToken.Object);

            return mockToken.Object;
        }

        protected ISqlQueryToken SetupFactoryForKeyword(SqlQueryKeyword keyword) => SetupFactory(factory => factory.Keyword(keyword));

    }
}
