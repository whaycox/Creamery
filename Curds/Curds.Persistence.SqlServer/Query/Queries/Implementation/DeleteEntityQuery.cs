using System.Collections.Generic;

namespace Curds.Persistence.Query.Queries.Implementation
{
    using Abstraction;
    using Persistence.Abstraction;

    internal class DeleteEntityQuery<TDataModel, TEntity> : BaseSqlQuery<TDataModel>
        where TDataModel : IDataModel
        where TEntity : IEntity
    {
        public ISqlTable DeletedTable { get; }
        public ISqlUniverse<TDataModel> Source { get; }

        public DeleteEntityQuery(
            ISqlQueryContext<TDataModel> queryContext,
            ISqlTable deletedTable,
            ISqlUniverse<TDataModel> source)
            : base(queryContext)
        {
            DeletedTable = deletedTable;
            Source = source;
        }

        protected override IEnumerable<ISqlQueryToken> GenerateTokens()
        {
            yield return PhraseBuilder.DeleteTableToken(DeletedTable);
            foreach (ISqlQueryToken token in Source.Tokens)
                yield return token;
        }
    }
}
