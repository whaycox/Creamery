using System.Text;

namespace Curds.Parsing.Reader.Domain
{
    using Abstraction;

    public class ReaderOptions : IReaderOptions
    {
        public const int DefaultBufferSize = 65535;
        public const int DefaultCharLookahead = 3; //We default to three characters ahead so we can always know if the next "char" is a newline
        public static Encoding DefaultEncoding = Encoding.UTF8;

        public int BufferSize { get; set; } = DefaultBufferSize;
        public int CharLookahead { get; set; } = DefaultCharLookahead;
        public Encoding TextEncoding { get; set; } = DefaultEncoding;
        public NewLineHandler NewLine { get; set; }
    }
}
