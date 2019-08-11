using System;
using System.IO;

namespace Curds.Parsing.Reader.Domain
{
    using Abstraction;

    public abstract class BufferedReader<T> : IDisposable where T : struct
    {
        private bool IsStarted = false;
        private int CurrentIndex = 0;
        private bool OnFinalBlock = false;
        private int FinalBlockSize = -1;

        private int Lookaheads { get; }
        private int FillIndex => Buffer.Length - Lookaheads - 1;

        private T[] Buffer { get; }
        private T?[] Tail { get; set; }

        private int LastIndex => (OnFinalBlock ? FinalBlockSize : Buffer.Length) - 1;

        public bool IsConsumed => OnFinalBlock && CurrentIndex > LastIndex;

        public T? this[int offset]
        {
            get
            {
                CheckStart();
                if (offset < 0 || offset > Lookaheads)
                    throw new IndexOutOfRangeException();

                if (IsConsumed)
                    return null;
                return Fetch(CalculateIndex(offset));
            }
        }
        private int CalculateIndex(int offset) => offset + CurrentIndex;

        public BufferedReader(IReaderOptions options)
        {
            if (options.BufferSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(options.BufferSize));
            if (options.Lookaheads >= options.BufferSize)
                throw new ArgumentOutOfRangeException(nameof(options.Lookaheads));

            Buffer = new T[options.BufferSize];
            Lookaheads = options.Lookaheads;
        }

        private void CheckStart()
        {
            if (!IsStarted)
            {
                FillBuffer();
                IsStarted = true;
            }
        }

        public T Advance()
        {
            CheckStart();
            T toReturn = AdvanceInternal();
            FillBufferIfNecessary();
            return toReturn;
        }
        private T AdvanceInternal()
        {
            if (IsConsumed)
                throw new InvalidOperationException("Cannot advance when the buffer is consumed");

            T toReturn = Fetch(CurrentIndex).Value;
            IncrementIndex();
            return toReturn;
        }
        private void IncrementIndex() => CurrentIndex++;

        private T? Fetch(int index)
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

            int elementsFilled = PopulateBuffer(Buffer);
            if (elementsFilled < Buffer.Length)
            {
                OnFinalBlock = true;
                FinalBlockSize = elementsFilled;
            }
        }
        protected abstract int PopulateBuffer(T[] buffer);

        private void FillTail()
        {
            T?[] tail = new T?[Lookaheads];
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
        protected virtual void Dispose(bool disposing) { }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion
    }
}
