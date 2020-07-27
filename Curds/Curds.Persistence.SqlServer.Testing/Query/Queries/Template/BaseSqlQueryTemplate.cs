using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace Curds.Persistence.Query.Queries.Template
{
    using Persistence.Abstraction;
    using Query.Abstraction;

    public abstract class BaseSqlQueryTemplate
    {
        protected List<SqlParameter> TestFlushedParameters = new List<SqlParameter>();

        protected Mock<ISqlQueryContext<ITestDataModel>> MockQueryContext = new Mock<ISqlQueryContext<ITestDataModel>>();
        protected Mock<ISqlQueryParameterBuilder> MockParameterBuilder = new Mock<ISqlQueryParameterBuilder>();
        protected Mock<ISqlQueryReader> MockQueryReader = new Mock<ISqlQueryReader>();
        protected Mock<ISqlQueryTokenFactory> MockTokenFactory = new Mock<ISqlQueryTokenFactory>();
        protected Mock<ISqlQueryPhraseBuilder> MockPhraseBuilder = new Mock<ISqlQueryPhraseBuilder>();
        protected Mock<ISqlUniverse<ITestDataModel>> MockSource = new Mock<ISqlUniverse<ITestDataModel>>();

        protected List<ISqlQueryToken> FormattedTokens = null;

        [TestInitialize]
        public void SetupBaseSqlQueryTemplate()
        {
            MockQueryContext
                .Setup(context => context.ParameterBuilder)
                .Returns(MockParameterBuilder.Object);
            MockQueryContext
                .Setup(context => context.TokenFactory)
                .Returns(MockTokenFactory.Object);
            MockQueryContext
                .Setup(context => context.PhraseBuilder)
                .Returns(MockPhraseBuilder.Object);
            MockQueryContext
                .Setup(context => context.Formatter.FormatTokens(It.IsAny<IEnumerable<ISqlQueryToken>>()))
                .Callback<IEnumerable<ISqlQueryToken>>(tokens => FormattedTokens = tokens.ToList());
            MockParameterBuilder
                .Setup(builder => builder.Flush())
                .Returns(() => TestFlushedParameters.ToArray());
        }
    }
}
