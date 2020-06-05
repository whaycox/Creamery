using System.Collections.Generic;
using System.Threading.Tasks;

namespace Curds.Persistence.Query.Queries.Implementation
{
    using Persistence.Abstraction;
    using Query.Abstraction;
    using Query.Domain;
    using System.Data.SqlClient;

    internal abstract class BaseSqlQuery<TModel> : ISqlQuery
        where TModel : IDataModel
    {
        private ISqlQueryContext<TModel> QueryContext { get; }

        private ISqlQueryFormatter Formatter => QueryContext.Formatter;
        protected ISqlQueryTokenFactory TokenFactory => QueryContext.TokenFactory;
        protected ISqlQueryParameterBuilder ParameterBuilder => QueryContext.ParameterBuilder;

        public BaseSqlQuery(ISqlQueryContext<TModel> queryContext)
        {
            QueryContext = queryContext;
        }

        public SqlCommand GenerateCommand()
        {
            SqlCommand command = new SqlCommand(Formatter.FormatTokens(GenerateTokens()));
            command.Parameters.AddRange(ParameterBuilder.Flush());
            return command;
        }
        protected abstract IEnumerable<ISqlQueryToken> GenerateTokens();

        protected ISqlQueryToken SelectColumnsToken(IEnumerable<ISqlColumn> columns) => TokenFactory.Phrase(
            TokenFactory.Keyword(SqlQueryKeyword.SELECT),
            TokenFactory.SelectList(columns));

        protected ISqlQueryToken DeleteTableToken(ISqlTable table) => TokenFactory.Phrase(
            TokenFactory.Keyword(SqlQueryKeyword.DELETE),
            TokenFactory.QualifiedObjectName(table));

        protected IEnumerable<ISqlQueryToken> FromUniverseTokens(ISqlUniverse universe)
        {
            foreach (ISqlTable table in universe.Tables)
                yield return TokenFactory.Phrase(
                    TokenFactory.Keyword(SqlQueryKeyword.FROM),
                    TokenFactory.QualifiedObjectName(table));

            int filterIndex = 0;
            foreach (ISqlQueryToken filter in universe.Filters)
                yield return TokenFactory.Phrase(
                    TokenFactory.Keyword(filterIndex++ > 0 ? SqlQueryKeyword.AND : SqlQueryKeyword.WHERE),
                    filter);
        }

        public abstract Task ProcessResult(ISqlQueryReader queryReader);
    }

    internal abstract class BaseSqlQuery<TModel, TEntity> : BaseSqlQuery<TModel>, ISqlQuery<TEntity>
        where TModel : IDataModel
        where TEntity : IEntity
    {
        public BaseSqlQuery(ISqlQueryContext<TModel> queryContext)
            : base(queryContext)
        { }

        public IList<TEntity> Results { get; } = new List<TEntity>();
    }
}
