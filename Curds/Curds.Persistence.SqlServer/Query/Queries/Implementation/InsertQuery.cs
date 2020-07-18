using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Curds.Persistence.Query.Queries.Implementation
{
    using Query.Abstraction;
    using Domain;
    using Persistence.Abstraction;
    using Abstraction;

    internal class InsertQuery<TModel, TEntity> : BaseSqlQuery<TModel>
        where TModel : IDataModel
        where TEntity : IEntity
    {
        private ISqlTable Table { get; }

        public List<TEntity> Entities { get; } = new List<TEntity>(); 
        private IEnumerable<ValueEntity> ValueEntities => Entities
            .Select(entity => Table.BuildValueEntity(entity))
            .ToList();

        public InsertQuery(ISqlQueryContext<TModel> queryContext)
            : base(queryContext)
        {
            Table = queryContext.AddTable<TEntity>();
        }

        protected override IEnumerable<ISqlQueryToken> GenerateTokens()
        {
            yield return PhraseBuilder.CreateTemporaryIdentityToken(Table);
            yield return PhraseBuilder.InsertToTableToken(Table);
            yield return PhraseBuilder.OutputToTemporaryIdentityToken(Table);
            foreach (ISqlQueryToken token in PhraseBuilder.ValueEntitiesToken(ParameterBuilder, ValueEntities))
                yield return token;
            foreach (ISqlQueryToken token in PhraseBuilder.SelectNewIdentitiesToken(Table))
                yield return token;
            yield return PhraseBuilder.DropTemporaryIdentityToken(Table);
        }

        public override async Task ProcessResult(ISqlQueryReader queryReader)
        {
            for (int i = 0; i < Entities.Count; i++)
                await AssignIdentity(queryReader, Entities[i]);
            if (await queryReader.Advance())
                throw new InvalidOperationException("There were more new identities than inserted entities");
        }
        private async Task AssignIdentity(ISqlQueryReader queryReader, TEntity entity)
        {
            if (!await queryReader.Advance())
                throw new InvalidOperationException("There were less new identities than inserted entities");
            Table.AssignIdentities(queryReader, entity);
        }
    }
}
