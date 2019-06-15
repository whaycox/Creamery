using System.Collections.Generic;

namespace Gouda.Communication
{
    using Abstraction;
    using Check.Data.Domain;
    using Check.Domain;
    using Domain;

    public static class Parsers
    {
        private static readonly List<IParser> _parsers = new List<IParser>
        {
            new AcknowledgementParser(),
            new ErrorParser(),
            new RequestParser(),
            new SeriesParser(),
            new SetParser(),
        };
        public static List<IParser> DefaultParsers => new List<IParser>(_parsers);
    }
}
