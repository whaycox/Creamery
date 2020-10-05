using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Curds.Persistence.Query.Queries.Implementation
{
    using Domain;
    using Persistence.Abstraction;
    using Query.Abstraction;

    internal class InsertQuery<TModel, TEntity> : BaseSqlQuery<TModel>
        where TModel : IDataModel
        where TEntity : IEntity
    {
        private ISqlTable Table { get; }
        private bool HasIdentity => Table.Identity != null;

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
            ISqlTable temporaryInsertedIdentities = Table.InsertedIdentityTable;

            if (HasIdentity)
                yield return PhraseBuilder.CreateTableToken(temporaryInsertedIdentities);

            yield return PhraseBuilder.InsertToTableToken(Table);

            if (HasIdentity)
                yield return PhraseBuilder.OutputToTemporaryIdentityToken(Table);

            yield return PhraseBuilder.ValueEntitiesToken(ParameterBuilder, ValueEntities);

            if (HasIdentity)
            {
                yield return PhraseBuilder.SelectColumnsToken(temporaryInsertedIdentities.Columns);
                yield return PhraseBuilder.FromTableToken(temporaryInsertedIdentities);
                yield return PhraseBuilder.DropTableToken(temporaryInsertedIdentities);
            }
        }

        public override async Task ProcessResult(ISqlQueryReader queryReader)
        {
            if (HasIdentity)
            {
                for (int i = 0; i < Entities.Count; i++)
                    await AssignIdentity(queryReader, Entities[i]);
                if (await queryReader.Advance())
                    throw new InvalidOperationException("There were more new identities than inserted entities");
            }
        }
        private async Task AssignIdentity(ISqlQueryReader queryReader, TEntity entity)
        {
            if (!await queryReader.Advance())
                throw new InvalidOperationException("There were less new identities than inserted entities");
            Table.AssignIdentities(queryReader, entity);
        }
    }
}
