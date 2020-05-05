using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Query.Tokens.Implementation
{
    using Model.Abstraction;
    using Query.Domain;
    using Query.Abstraction;

    public class InsertedIdentityColumnSqlQueryToken : QualifiedObjectSqlQueryToken
    {
        public InsertedIdentityColumnSqlQueryToken(ISqlColumn identity)
            : base(
                new ObjectNameSqlQueryToken(nameof(SqlQueryKeyword.inserted)),
                new ObjectNameSqlQueryToken(identity.Name))
        { }
    }
}
