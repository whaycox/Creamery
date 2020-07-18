using System.Collections.Generic;

namespace Curds.Persistence.Query.Implementation
{
    using Abstraction;
    using Domain;

    internal class SqlQueryPhraseBuilder : ISqlQueryPhraseBuilder
    {
        private ISqlQueryTokenFactory TokenFactory { get; }

        public SqlQueryPhraseBuilder(ISqlQueryTokenFactory tokenFactory)
        {
            TokenFactory = tokenFactory;
        }

        public ISqlQueryToken CreateTemporaryIdentityToken(ISqlTable table) => TokenFactory.Phrase(
            TokenFactory.Keyword(SqlQueryKeyword.CREATE),
            TokenFactory.Keyword(SqlQueryKeyword.TABLE),
            TokenFactory.TemporaryIdentityName(table),
            TokenFactory.ColumnList(new[] { table.Identity }, true));

        public ISqlQueryToken OutputToTemporaryIdentityToken(ISqlTable table) => TokenFactory.Phrase(
            TokenFactory.Keyword(SqlQueryKeyword.OUTPUT),
            TokenFactory.InsertedIdentityName(table),
            TokenFactory.Keyword(SqlQueryKeyword.INTO),
            TokenFactory.TemporaryIdentityName(table));

        public ISqlQueryToken DropTemporaryIdentityToken(ISqlTable table) => TokenFactory.Phrase(
            TokenFactory.Keyword(SqlQueryKeyword.DROP),
            TokenFactory.Keyword(SqlQueryKeyword.TABLE),
            TokenFactory.TemporaryIdentityName(table));

        public IEnumerable<ISqlQueryToken> SelectNewIdentitiesToken(ISqlTable table) => new ISqlQueryToken[]
        {
            SelectColumnsToken(new[]{ table.Identity }),
            TokenFactory.Phrase(
                    TokenFactory.Keyword(SqlQueryKeyword.FROM),
                    TokenFactory.TemporaryIdentityName(table)),
        };

        public ISqlQueryToken InsertToTableToken(ISqlTable table) => TokenFactory.Phrase(
            TokenFactory.Keyword(SqlQueryKeyword.INSERT),
            TokenFactory.QualifiedObjectName(table),
            TokenFactory.ColumnList(table.NonIdentities, false));

        public IEnumerable<ISqlQueryToken> ValueEntitiesToken(ISqlQueryParameterBuilder parameterBuilder, IEnumerable<ValueEntity> valueEntities) => new ISqlQueryToken[]
        {
            TokenFactory.Keyword(SqlQueryKeyword.VALUES),
            TokenFactory.ValueEntities(parameterBuilder, valueEntities)
        };

        public ISqlQueryToken SelectColumnsToken(IEnumerable<ISqlColumn> columns) => TokenFactory.Phrase(
            TokenFactory.Keyword(SqlQueryKeyword.SELECT),
            TokenFactory.SelectList(columns));

        public ISqlQueryToken DeleteTableToken(ISqlTable table) => TokenFactory.Phrase(
            TokenFactory.Keyword(SqlQueryKeyword.DELETE),
            TokenFactory.QualifiedObjectName(table));

        public ISqlQueryToken UpdateTableToken(ISqlTable table) => TokenFactory.Phrase(
            TokenFactory.Keyword(SqlQueryKeyword.UPDATE),
            TokenFactory.QualifiedObjectName(table));

        public ISqlQueryToken SetValuesToken(IEnumerable<ISqlQueryToken> setValueTokens) => TokenFactory.Phrase(
            TokenFactory.Keyword(SqlQueryKeyword.SET),
            TokenFactory.SetValues(setValueTokens));
    }
}
