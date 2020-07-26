using System;
using System.Collections.Generic;
using System.Linq;

namespace Curds.Persistence.Query.Tokens.Implementation
{
    using Query.Abstraction;
    using Query.Domain;

    public class BooleanCombinationSqlQueryToken : RedirectedSqlQueryToken
    {
        private static ConstantSqlQueryToken OpeningToken { get; } = new ConstantSqlQueryToken("(");
        private static ConstantSqlQueryToken ClosingToken { get; } = new ConstantSqlQueryToken(")");
        private static Dictionary<BooleanCombination, SqlQueryKeyword> KeywordMap { get; } = new Dictionary<BooleanCombination, SqlQueryKeyword>
        {
            { BooleanCombination.And, SqlQueryKeyword.AND },
            { BooleanCombination.Or, SqlQueryKeyword.OR },
        };

        public ISqlQueryToken Operation { get; }
        public List<ISqlQueryToken> Elements { get; }

        public BooleanCombinationSqlQueryToken(
            ISqlQueryTokenFactory tokenFactory,
            BooleanCombination operation,
            IEnumerable<ISqlQueryToken> elements)
            : base(tokenFactory)
        {
            Operation = OperationKeyword(operation);
            Elements = elements.ToList();
        }
        private ISqlQueryToken OperationKeyword(BooleanCombination operation)
        {
            if (!KeywordMap.TryGetValue(operation, out SqlQueryKeyword keyword))
                throw new ArgumentException($"Unsupported operation: {operation}");
            return TokenFactory.Keyword(keyword);
        }

        protected override ISqlQueryToken RedirectedToken()
        {
            List<ISqlQueryToken> wrappedElements = new List<ISqlQueryToken>();
            for (int i = 0; i < Elements.Count; i++)
            {
                if (i == 0)
                    wrappedElements.Add(WrapElement(Elements[i]));
                else
                    wrappedElements.Add(TokenFactory.Phrase(
                        Operation,
                        WrapElement(Elements[i])));
            }
            return TokenFactory.GroupedList(wrappedElements, false);
        }
        private ISqlQueryToken WrapElement(ISqlQueryToken element) => TokenFactory.Phrase(
            OpeningToken,
            element,
            ClosingToken);
    }
}
