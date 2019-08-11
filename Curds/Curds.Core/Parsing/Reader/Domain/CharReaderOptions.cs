using System.Text;

namespace Curds.Parsing.Reader.Domain
{
    using Abstraction;

    public class CharReaderOptions : ReaderOptions, ICharReaderOptions
    {
        public const int DefaultCharLookahead = 2; //We default to two characters ahead so we can always know if the next "char" is a newline
        public static Encoding DefaultEncoding = Encoding.UTF8;

        public Encoding TextEncoding { get; set; } = DefaultEncoding;
        public NewLineHandler NewLine { get; set; }

        public CharReaderOptions()
        {
            Lookaheads = DefaultCharLookahead;
        }
    }
}
