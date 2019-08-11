using System.IO;

namespace Curds.Parsing.Reader.Implementation
{
    using Abstraction;
    using Domain;

    public class BufferedReader : BufferedReader<byte>
    {
        private Stream Stream { get; }

        public BufferedReader(Stream stream, IReaderOptions options)
            : base(options)
        {
            Stream = stream;
        }

        protected override int PopulateBuffer(byte[] buffer) => Stream.Read(buffer, 0, buffer.Length);

        private bool IsDisposed = false;
        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                base.Dispose(disposing);
                if (disposing)
                {
                    Stream.Dispose();
                    IsDisposed = true;
                }
            }
        }
    }
}
