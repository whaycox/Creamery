using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Parsing.CSV.Implementation
{
    public class ReadingOptions
    {
        private const char DefaultSeparator = ',';
        private const char DefaultQualifier = '"';

        public char Separator { get; set; }
        public char Qualifier { get; set; }
        public Encoding Encoding { get; set; }
        public NewLineHandler NewLine { get; set; }

        public ReadingOptions()
            : this(DefaultSeparator, DefaultQualifier, Encoding.Default, NewLineHandler.CurrentEnvironment)
        { }

        private ReadingOptions(char separator, char qualifier, Encoding encoding, NewLineHandler newLineHandler)
        {
            Separator = separator;
            Qualifier = qualifier;
            Encoding = encoding;
            NewLine = newLineHandler;
        }
    }
}
