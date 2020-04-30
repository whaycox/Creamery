using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Query.Tokens.Implementation
{
    using Model.Abstraction;
    using Query.Domain;

    public class InsertedIdentityColumnSqlQueryToken : QualifiedObjectSqlQueryToken
    {
        public InsertedIdentityColumnSqlQueryToken(IValueModel identity)
            : base(
                new ObjectNameSqlQueryToken(nameof(SqlQueryKeyword.inserted)),
                new ObjectNameSqlQueryToken(identity.Name))
        { }
    }
}
