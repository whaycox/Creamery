using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Parsing.CSV.Domain
{
    using Reader.Domain;

    public class CSVOptions : Abstraction.ICSVOptions
    {
        private const char DefaultSeparator = ',';
        private const char DefaultQualifier = '"';

        public int BufferSize { get; set; } = CharReaderOptions.DefaultBufferSize;
        public int Lookaheads { get; set; } = CharReaderOptions.DefaultCharLookahead;
        public Encoding TextEncoding { get; set; } = CharReaderOptions.DefaultEncoding;
        public NewLineHandler NewLine { get; set; } = NewLineHandler.CurrentEnvironment;

        public char Separator { get; set; } = DefaultSeparator;
        public char Qualifier { get; set; } = DefaultQualifier;
    }
}
