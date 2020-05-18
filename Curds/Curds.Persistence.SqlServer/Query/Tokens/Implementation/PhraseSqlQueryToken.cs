using System.Collections.Generic;
using System.Linq;

namespace Curds.Persistence.Query.Tokens.Implementation
{
    using Query.Abstraction;

    internal class PhraseSqlQueryToken : BaseSqlQueryToken
    {
        private static ConstantSqlQueryToken SpaceToken { get; } = new ConstantSqlQueryToken(" ");

        public List<ISqlQueryToken> Tokens { get; }

        public PhraseSqlQueryToken(params ISqlQueryToken[] tokens)
        {
            Tokens = tokens.ToList();
        }

        public override void AcceptFormatVisitor(ISqlQueryFormatVisitor visitor)
        {
            for (int i = 0; i < Tokens.Count; i++)
            {
                if (i != 0)
                    SpaceToken.AcceptFormatVisitor(visitor);
                Tokens[i].AcceptFormatVisitor(visitor);
            }
        }
    }
}
