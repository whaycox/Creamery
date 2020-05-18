using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Curds.Persistence.Query.Queries.Implementation
{
    using Abstraction;
    using Domain;
    using Persistence.Abstraction;

    internal class InsertQuery<TModel, TEntity> : BaseSqlQuery<TModel>
        where TModel : IDataModel
        where TEntity : IEntity
    {
        private ISqlTable Table { get; }

        public List<TEntity> Entities { get; set; } = new List<TEntity>(); 
        private IEnumerable<ValueEntity> ValueEntities => Entities
            .Select(entity => Table.BuildValueEntity(entity));

        public InsertQuery(ISqlQueryContext<TModel> queryContext)
            : base(queryContext)
        {
            Table = queryContext.AddTable<TEntity>();
        }

        protected override IEnumerable<ISqlQueryToken> GenerateTokens()
        {
            yield return CreateTemporaryIdentityToken;
            yield return InsertToTableToken;
            yield return OutputToTemporaryIdentityToken;
            foreach (ISqlQueryToken token in ValueEntitiesToken)
                yield return token;
            foreach (ISqlQueryToken token in SelectNewIdentitiesToken)
                yield return token;
            yield return DropTemporaryIdentityToken;
        }
        private ISqlQueryToken CreateTemporaryIdentityToken => TokenFactory.Phrase(
            TokenFactory.Keyword(SqlQueryKeyword.CREATE),
            TokenFactory.Keyword(SqlQueryKeyword.TABLE),
            TokenFactory.TemporaryIdentityName(Table),
            TokenFactory.ColumnList(new[] { Table.Identity }, true));
        private ISqlQueryToken InsertToTableToken => TokenFactory.Phrase(
            TokenFactory.Keyword(SqlQueryKeyword.INSERT),
            TokenFactory.QualifiedObjectName(Table),
            TokenFactory.ColumnList(Table.NonIdentities, false));
        private ISqlQueryToken OutputToTemporaryIdentityToken => TokenFactory.Phrase(
            TokenFactory.Keyword(SqlQueryKeyword.OUTPUT),
            TokenFactory.InsertedIdentityName(Table),
            TokenFactory.Keyword(SqlQueryKeyword.INTO),
            TokenFactory.TemporaryIdentityName(Table));
        private IEnumerable<ISqlQueryToken> ValueEntitiesToken => new ISqlQueryToken[]
        {
            TokenFactory.Keyword(SqlQueryKeyword.VALUES),
            TokenFactory.ValueEntities(ParameterBuilder, ValueEntities)
        };
        private IEnumerable<ISqlQueryToken> SelectNewIdentitiesToken => new ISqlQueryToken[]
        {
            SelectColumnsToken(new[]{ Table.Identity }),
            TokenFactory.Phrase(
                    TokenFactory.Keyword(SqlQueryKeyword.FROM),
                    TokenFactory.TemporaryIdentityName(Table)),
        };
        private ISqlQueryToken DropTemporaryIdentityToken => TokenFactory.Phrase(
            TokenFactory.Keyword(SqlQueryKeyword.DROP),
            TokenFactory.Keyword(SqlQueryKeyword.TABLE),
            TokenFactory.TemporaryIdentityName(Table));

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
