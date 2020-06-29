using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace Curds.Persistence.Query.Queries.Template
{
    using Abstraction;
    using Persistence.Abstraction;
    using Query.Abstraction;

    public abstract class BaseSqlQueryTemplate
    {
        protected List<SqlParameter> TestFlushedParameters = new List<SqlParameter>();

        protected Mock<ISqlQueryContext<ITestDataModel>> MockQueryContext = new Mock<ISqlQueryContext<ITestDataModel>>();
        protected Mock<ISqlQueryParameterBuilder> MockParameterBuilder = new Mock<ISqlQueryParameterBuilder>();
        protected Mock<ISqlQueryReader> MockQueryReader = new Mock<ISqlQueryReader>();
        protected Mock<ISqlQueryPhraseBuilder> MockPhraseBuilder = new Mock<ISqlQueryPhraseBuilder>();
        protected Mock<ISqlUniverse> MockSource = new Mock<ISqlUniverse>();

        protected List<ISqlQueryToken> FormattedTokens = null;

        [TestInitialize]
        public void SetupBaseSqlQueryTemplate()
        {
            MockQueryContext
                .Setup(context => context.ParameterBuilder)
                .Returns(MockParameterBuilder.Object);
            MockQueryContext
                .Setup(context => context.Formatter.FormatTokens(It.IsAny<IEnumerable<ISqlQueryToken>>()))
                .Callback<IEnumerable<ISqlQueryToken>>(tokens => FormattedTokens = tokens.ToList());
            MockParameterBuilder
                .Setup(builder => builder.Flush())
                .Returns(() => TestFlushedParameters.ToArray());
        }

        protected ISqlQueryToken SetupPhraseBuilder(Expression<Func<ISqlQueryPhraseBuilder, ISqlQueryToken>> phraseExpression)
        {
            ISqlQueryToken testToken = Mock.Of<ISqlQueryToken>();
            MockPhraseBuilder
                .Setup(phraseExpression)
                .Returns(testToken);
            return testToken;
        }

        protected List<ISqlQueryToken> SetupPhraseBuilder(Expression<Func<ISqlQueryPhraseBuilder, IEnumerable<ISqlQueryToken>>> phraseExpression, int numberOfTokens)
        {
            List<ISqlQueryToken> testTokens = new List<ISqlQueryToken>();
            for (int i = 0; i < numberOfTokens; i++)
                testTokens.Add(Mock.Of<ISqlQueryToken>());
            MockPhraseBuilder
                .Setup(phraseExpression)
                .Returns(testTokens);
            return testTokens;
        }
    }
}
