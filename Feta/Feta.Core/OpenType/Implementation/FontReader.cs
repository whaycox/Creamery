using Curds.Parsing.Reader.Domain;
using Curds.Parsing.Reader.Implementation;
using System;
using System.IO;
using System.Text;

namespace Feta.OpenType.Implementation
{
    using Abstraction;

    public class FontReader : IFontReader, IDisposable
    {
        private const int BufferSize = 65535;

        private BufferedReader Reader { get; }

        public ITableCollection Tables { get; } = new TableCollection();
        public IOffsetRegistry Offsets { get; } = new OffsetRegistry();

        public bool IsConsumed => Reader.IsConsumed;
        public uint CurrentOffset { get; private set; }

        public FontReader(Stream stream)
        {
            Reader = new BufferedReader(stream, DefaultOptions);
        }

        private ReaderOptions DefaultOptions = new ReaderOptions
        {
            BufferSize = BufferSize,
            Lookaheads = 0,
        };

        public byte ReadByte()
        {
            CurrentOffset++;
            return Reader.Advance();
        }
        private byte[] ReadNBytes(int numberToRead)
        {
            byte[] toReturn = new byte[numberToRead];
            for (int i = 0; i < numberToRead; i++)
                toReturn[i] = ReadByte();
            return toReturn;
        }
        private byte[] ReadNContiguousBytes(int numberToRead)
        {
            byte[] toReturn = new byte[numberToRead];
            for (int i = 0; i < numberToRead; i++)
                toReturn[i] = ReadByte();

            if (BitConverter.IsLittleEndian)
                Array.Reverse(toReturn);

            return toReturn;
        }

        public ushort ReadUInt16() => BitConverter.ToUInt16(ReadNContiguousBytes(2), 0);
        public uint ReadUInt32() => BitConverter.ToUInt32(ReadNContiguousBytes(4), 0);
        public string ReadTag() => Encoding.ASCII.GetString(ReadNBytes(4));

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Reader?.Dispose();
                }
                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion
    }
}
