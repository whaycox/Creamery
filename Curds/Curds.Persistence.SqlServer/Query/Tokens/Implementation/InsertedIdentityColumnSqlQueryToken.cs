using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Query.Tokens.Implementation
{
    using Model.Domain;
    using Query.Domain;

    public class InsertedIdentityColumnSqlQueryToken : QualifiedObjectSqlQueryToken
    {
        public InsertedIdentityColumnSqlQueryToken(Column identityColumn)
            : base(
                new ObjectNameSqlQueryToken(nameof(SqlQueryKeyword.inserted)),
                new ObjectNameSqlQueryToken(identityColumn.Name))
        { }
    }
}
