using System;
using System.Collections.Generic;

namespace Curds.Cron.Token.Mock
{
    using Enumeration;

    public class Basic : Domain.Basic
    {
        public override int AbsoluteMin { get; } = int.MinValue;
        public override int AbsoluteMax { get; } = int.MaxValue;

        private Token _currentType = Token.Minute;
        public override Token TokenType => _currentType;

        public Basic(Range.Mock.Basic mockRange)
            : base(new Range.Mock.Basic[] { mockRange })
        { }

        public void SetTokenType(Token newType) => _currentType = newType;
    }
}
