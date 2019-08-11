﻿using System.IO;

namespace Curds.Parsing.Reader.Implementation
{
    using Abstraction;
    using Domain;

    public class CharBuffer : BufferedReader<char>
    {
        private StreamReader Reader { get; }

        public CharBuffer(Stream inputStream, ICharReaderOptions options)
            : base(options)
        {
            Reader = new StreamReader(inputStream, options.TextEncoding);
        }

        protected override int PopulateBuffer(char[] buffer) => Reader.Read(buffer, 0, buffer.Length);

        private bool IsDisposed = false;
        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                base.Dispose(disposing);
                if (disposing)
                {
                    Reader.Dispose();
                    IsDisposed = true;
                }
            }
        }
    }
}
