using System.Collections.Generic;
using System.Threading.Tasks;

namespace Curds.Persistence.Query.Queries.Implementation
{
    using Persistence.Abstraction;
    using Query.Abstraction;
    using System.Data.SqlClient;

    internal abstract class BaseSqlQuery<TModel> : ISqlQuery
        where TModel : IDataModel
    {
        private ISqlQueryContext<TModel> QueryContext { get; }

        private ISqlQueryFormatter Formatter => QueryContext.Formatter;
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

        public virtual Task ProcessResult(ISqlQueryReader queryReader) => Task.CompletedTask;

        public Task Execute() => QueryContext
            .ConnectionContext
            .Execute(this);
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
