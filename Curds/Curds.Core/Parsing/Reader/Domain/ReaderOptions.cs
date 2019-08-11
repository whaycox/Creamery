namespace Curds.Parsing.Reader.Domain
{
    using Abstraction;

    public class ReaderOptions : IReaderOptions
    {
        public const int DefaultBufferSize = 65535;
        public const int DefaultLookaheads = 16;

        public int BufferSize { get; set; } = DefaultBufferSize;
        public int Lookaheads { get; set; } = DefaultLookaheads;
    }
}
