using System.Text;

namespace Curds.Parsing.Reader.Abstraction
{
    using Domain;

    public interface IReaderOptions
    {
        int BufferSize { get; }
        int CharLookahead { get; }
        Encoding TextEncoding { get; }
        NewLineHandler NewLine { get; set; }
    }
}
