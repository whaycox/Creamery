using System;
using System.IO;
using System.Text;

namespace Feta.OpenType.Domain
{
    using Enumerations;

    public class FontWriter : IDisposable
    {
        private const int TagLength = 4;

        private Stream Stream { get; }

        public FontWriter(Stream stream)
        {
            Stream = stream;
        }

        public void WriteSfntVersion(SfntVersion version) => WriteUInt32((uint)version);
        public void WriteUInt32(uint value)
        {
            byte[] toWrite = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(toWrite);

            Stream.Write(toWrite, 0, toWrite.Length);
        }
        public void WriteUInt16(ushort value)
        {
            byte[] toWrite = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(toWrite);

            Stream.Write(toWrite, 0, toWrite.Length);
        }
        public void WriteTag(string tag)
        {
            if (string.IsNullOrWhiteSpace(tag) || tag.Length != TagLength)
                throw new ArgumentException(nameof(tag));

            byte[] bytes = Encoding.ASCII.GetBytes(tag);
            Stream.Write(bytes, 0, bytes.Length);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Stream?.Dispose();
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
