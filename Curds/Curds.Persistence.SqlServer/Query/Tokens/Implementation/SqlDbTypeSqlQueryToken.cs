using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Query.Tokens.Implementation
{
    using Query.Abstraction;

    public class SqlDbTypeSqlQueryToken : BaseSqlQueryToken
    {
        public SqlDbTypeSqlQueryToken(ISqlQueryTokenFactory tokenFactory)
            : base(tokenFactory)
        { }

        public override void AcceptFormatVisitor(ISqlQueryFormatVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}
