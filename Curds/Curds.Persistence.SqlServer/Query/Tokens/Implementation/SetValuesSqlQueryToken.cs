using System.Collections.Generic;

namespace Curds.Persistence.Query.Tokens.Implementation
{
    using Query.Abstraction;

    public class SetValuesSqlQueryToken : BaseSqlQueryToken
    {
        public List<ISqlQueryToken> SetValueTokens { get; } = new List<ISqlQueryToken>();

        public SetValuesSqlQueryToken(
            ISqlQueryTokenFactory tokenFactory,
            IEnumerable<ISqlQueryToken> setValueTokens)
            : base(tokenFactory)
        {
            SetValueTokens.AddRange(setValueTokens);
        }

        public override void AcceptFormatVisitor(ISqlQueryFormatVisitor visitor) => visitor.VisitSetValues(this);
    }
}
