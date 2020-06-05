using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Curds.Persistence.Query.Queries.Implementation
{
    using Abstraction;
    using Persistence.Domain;
    using Model.Domain;
    using Persistence.Abstraction;
    using Model.Abstraction;

    internal class DeleteEntityQuery<TModel, TEntity> : BaseSqlQuery<TModel, TEntity>
        where TModel : IDataModel
        where TEntity : IEntity
    {
        public ISqlTable DeletedTable { get; set; }
        public ISqlUniverse<TEntity> Source { get; set; }

        public DeleteEntityQuery(ISqlQueryContext<TModel> queryContext)
            : base(queryContext)
        { }

        protected override IEnumerable<ISqlQueryToken> GenerateTokens()
        {
            yield return DeleteTableToken(DeletedTable);
            foreach (ISqlQueryToken token in FromUniverseTokens(Source))
                yield return token;
        }

        public override Task ProcessResult(ISqlQueryReader queryReader)
        {
            throw new NotImplementedException();
        }
    }
}
