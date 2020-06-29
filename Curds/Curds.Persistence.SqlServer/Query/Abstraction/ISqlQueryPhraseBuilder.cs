using System.Collections.Generic;

namespace Curds.Persistence.Query.Abstraction
{
    using Query.Abstraction;
    using Query.Domain;

    public interface ISqlQueryPhraseBuilder
    {
        ISqlQueryToken CreateTemporaryIdentityToken(ISqlTable table);
        ISqlQueryToken OutputToTemporaryIdentityToken(ISqlTable table);
        ISqlQueryToken DropTemporaryIdentityToken(ISqlTable table);
        IEnumerable<ISqlQueryToken> SelectNewIdentitiesToken(ISqlTable table);

        ISqlQueryToken InsertToTableToken(ISqlTable table);
        IEnumerable<ISqlQueryToken> ValueEntitiesToken(ISqlQueryParameterBuilder parameterBuilder, IEnumerable<ValueEntity> valueEntities);
        ISqlQueryToken SelectColumnsToken(IEnumerable<ISqlColumn> columns);
        ISqlQueryToken DeleteTableToken(ISqlTable table);
        ISqlQueryToken UpdateTableToken(ISqlTable table);
        ISqlQueryToken SetValuesToken(IEnumerable<ISqlQueryToken> setValueTokens);
        IEnumerable<ISqlQueryToken> FromUniverseTokens(ISqlUniverse universe);
    }
}
