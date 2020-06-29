using System.Collections.Generic;

namespace Curds.Persistence.Query.Queries.Implementation
{
    using Persistence.Abstraction;
    using Query.Abstraction;

    internal class SimpleSqlQuery : BaseSqlQuery<ITestDataModel>
    {
        public ISqlQueryParameterBuilder ExposedParameterBuilder => ParameterBuilder;

        public List<ISqlQueryToken> TokensToGenerate { get; } = new List<ISqlQueryToken>();

        public SimpleSqlQuery(ISqlQueryContext<ITestDataModel> queryContext)
            : base(queryContext)
        { }

        protected override IEnumerable<ISqlQueryToken> GenerateTokens() => TokensToGenerate;
    }
}
