namespace Curds.Parsing.Reader.Abstraction
{
    public interface IReaderOptions
    {
        int BufferSize { get; }
        int Lookaheads { get; }
    }
}
