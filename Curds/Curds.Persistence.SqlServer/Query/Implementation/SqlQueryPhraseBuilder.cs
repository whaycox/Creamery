using System.Collections.Generic;
using System.Linq;

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

        public ISqlQueryToken CreateTableToken(ISqlTable table) => TokenFactory.Phrase(
            TokenFactory.Keyword(SqlQueryKeyword.CREATE),
            TokenFactory.Keyword(SqlQueryKeyword.TABLE),
            TokenFactory.TableDefinition(table));

        public ISqlQueryToken DropTableToken(ISqlTable table) => TokenFactory.Phrase(
            TokenFactory.Keyword(SqlQueryKeyword.DROP),
            TokenFactory.Keyword(SqlQueryKeyword.TABLE),
            TokenFactory.TableName(table, useAlias: false));

        public ISqlQueryToken OutputToTemporaryIdentityToken(ISqlTable table) => TokenFactory.Phrase(
            TokenFactory.Keyword(SqlQueryKeyword.OUTPUT),
            TokenFactory.InsertedIdentityName(table),
            TokenFactory.Keyword(SqlQueryKeyword.INTO),
            TokenFactory.TableName(table.InsertedIdentityTable, useAlias: false));

        public ISqlQueryToken InsertToTableToken(ISqlTable table) => TokenFactory.Phrase(
            TokenFactory.Keyword(SqlQueryKeyword.INSERT),
            TokenFactory.TableName(table, false),
            TokenFactory.GroupedList(
                table.NonIdentities.Select(column => TokenFactory.ColumnName(column, false))));

        public ISqlQueryToken ValueEntitiesToken(ISqlQueryParameterBuilder parameterBuilder, IEnumerable<ValueEntity> valueEntities) => TokenFactory.Phrase(
            TokenFactory.Keyword(SqlQueryKeyword.VALUES),
            TokenFactory.UngroupedList(
                valueEntities.Select(valueEntity => TokenFactory.ValueEntity(parameterBuilder, valueEntity))));

        public ISqlQueryToken SelectColumnsToken(IEnumerable<ISqlColumn> columns) => TokenFactory.Phrase(
            TokenFactory.Keyword(SqlQueryKeyword.SELECT),
            TokenFactory.UngroupedList(
                columns.Select(column => TokenFactory.ColumnName(column))));

        public ISqlQueryToken FromTableToken(ISqlTable table) => TokenFactory.Phrase(
            TokenFactory.Keyword(SqlQueryKeyword.FROM),
            TokenFactory.TableName(table));

        public ISqlQueryToken JoinTableToken(ISqlJoinClause joinClause) => TokenFactory.Phrase(
            TokenFactory.Keyword(SqlQueryKeyword.JOIN),
            TokenFactory.TableName(joinClause.JoinedTable),
            TokenFactory.JoinClause(joinClause));

        public ISqlQueryToken DeleteTableToken(ISqlTable table) => TokenFactory.Phrase(
            TokenFactory.Keyword(SqlQueryKeyword.DELETE),
            TokenFactory.TableName(table, useSqlName: false));

        public ISqlQueryToken UpdateTableToken(ISqlTable table, IEnumerable<ISqlQueryToken> setValueTokens) => TokenFactory.Phrase(
            TokenFactory.Keyword(SqlQueryKeyword.UPDATE),
            TokenFactory.TableName(table, useSqlName: false),
            TokenFactory.Keyword(SqlQueryKeyword.SET),
            TokenFactory.UngroupedList(setValueTokens));
    }
}
