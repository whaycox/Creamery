using System.Text;

namespace Curds.Parsing.Reader.Abstraction
{
    using Domain;

    public interface ICharReaderOptions : IReaderOptions
    {
        Encoding TextEncoding { get; }
        NewLineHandler NewLine { get; set; }
    }
}
