using System;
using System.IO;
using System.Collections.Generic;

namespace Curds.Parsing.Reader.Implementation
{
    using Abstraction;

    public class CharBuffer : IDisposable
    {
        private Stream Input { get; }
        private StreamReader StreamReader { get; }

        private int CurrentIndex = 0;
        private bool OnFinalBlock = false;
        private int FinalBlockSize = -1;

        private int Lookaheads { get; }
        private int FillIndex => Buffer.Length - Lookaheads - 1;

        private char[] Buffer { get; }
        private char?[] Tail { get; set; }

        private int LastIndex => (OnFinalBlock ? FinalBlockSize : Buffer.Length) - 1;

        public bool IsConsumed => OnFinalBlock && CurrentIndex > LastIndex;

        public char? this[int offset]
        {
            get
            {
                if (offset < 0 || offset > Lookaheads)
                    throw new IndexOutOfRangeException();

                if (IsConsumed)
                    return null;
                return Fetch(CalculateIndex(offset));
            }
        }
        private int CalculateIndex(int offset) => offset + CurrentIndex;

        public CharBuffer(Stream inputStream, IReaderOptions options)
        {
            if (options.BufferSize <= 0 || options.BufferSize <= options.CharLookahead)
                throw new ArgumentOutOfRangeException(nameof(options.BufferSize));

            Input = inputStream ?? throw new ArgumentNullException(nameof(inputStream));
            StreamReader = new StreamReader(Input, options.TextEncoding);
            Buffer = new char[options.BufferSize];
            Lookaheads = options.CharLookahead;

            FillBuffer();
        }

        public char Advance()
        {
            char toReturn = AdvanceInternal();
            FillBufferIfNecessary();
            return toReturn;
        }
        private char AdvanceInternal()
        {
            if (IsConsumed)
                throw new InvalidOperationException("Cannot advance when the buffer is consumed");

            char toReturn = Fetch(CurrentIndex).Value;
            IncrementIndex();
            return toReturn;
        }
        private void IncrementIndex() => CurrentIndex++;

        private char? Fetch(int index)
        {
            if (index < 0)
                return Tail[CalculateTailIndex(index)];
            else if (index > LastIndex)
                return null;
            else
                return Buffer[index];
        }
        private int CalculateTailIndex(int rawIndex) => Tail.Length + rawIndex;

        private void FillBufferIfNecessary()
        {
            if (IsFillNecessary)
            {
                FillTail();
                FillBuffer();
            }
        }
        private bool IsFillNecessary => !OnFinalBlock && CurrentIndex > FillIndex;
        private void FillBuffer()
        {
            if (OnFinalBlock)
                throw new InvalidOperationException("Cannot fill when the final block has already been reached");

            int charsRead = StreamReader.ReadBlock(Buffer, 0, Buffer.Length);
            if (charsRead < Buffer.Length)
            {
                OnFinalBlock = true;
                FinalBlockSize = charsRead;
            }
        }

        private void FillTail()
        {
            char?[] tail = new char?[Lookaheads];
            for (int i = 0; i < Lookaheads; i++)
            {
                tail[i] = AdvanceInternal();
                if (IsConsumed)
                    break;
            }
            Tail = tail;
            CurrentIndex = -Lookaheads;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Input.Dispose();
                    StreamReader.Dispose();
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
