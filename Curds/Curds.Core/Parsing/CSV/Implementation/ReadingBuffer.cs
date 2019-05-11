using System;
using System.IO;
using System.Text;

namespace Curds.Parsing.CSV.Implementation
{
    public class ReadingBuffer
    {
        private const int DefaultBufferSizeInChars = 65536;
        private const int TailSizeInChars = 2; //We keep a tail of two so that we can always check three chars ahead for new-lines

        private char[] Buffer = null;
        private char[] PreviousBufferTail = null;

        public bool BufferConsumed => EndOfStreamReached && CurrentBufferIndex > LastIndex;
        private bool EndOfStreamReached = false;
        private int CurrentBufferIndex = 0;
        private int LastIndex => (EndOfStreamReached ? FinalBufferBlockSize : Buffer.Length) - 1;
        private int FinalBufferBlockSize = -1;

        private StreamReader Reader { get; }

        public char? First => Fetch(0);
        public char? Next => Fetch(1);
        public char? Third => Fetch(2);

        public ReadingBuffer(Stream stream, Encoding encoding)
            : this(DefaultBufferSizeInChars, stream, encoding)
        { }

        public ReadingBuffer(int bufferSize, Stream stream, Encoding encoding)
        {
            if (bufferSize <= TailSizeInChars)
                throw new ArgumentOutOfRangeException(nameof(bufferSize));

            Buffer = new char[bufferSize];
            Reader = new StreamReader(stream ?? throw new ArgumentNullException(nameof(stream)), encoding);
            FillBuffer();
        }

        private char? Fetch(int offset)
        {
            int fetchIndex = CurrentBufferIndex + offset;
            if (!CheckFetchIndex(fetchIndex))
                return null;
            else if (fetchIndex < 0)
                return PreviousBufferTail[TailSizeInChars - Math.Abs(fetchIndex)];
            else
                return Buffer[fetchIndex];
        }
        private bool CheckFetchIndex(int fetchIndex)
        {
            if (fetchIndex < 0 && Math.Abs(fetchIndex) > TailSizeInChars)
                return false;
            else if (fetchIndex > LastIndex)
                return false;
            else
                return true;
        }

        public char? AdvanceReadBuffer()
        {
            if (BufferConsumed)
                return null;
            char toReturn = AdvanceBufferInternal();
            CheckTail();
            return toReturn;
        }
        private char AdvanceBufferInternal()
        {
            char toReturn = First.Value;
            CurrentBufferIndex++;
            FillBufferIfNecessary();
            return toReturn;
        }

        private void FillBufferIfNecessary()
        {
            if (!EndOfStreamReached && IsFillNecessary)
                FillBuffer();
        }
        private bool IsFillNecessary => CurrentBufferIndex >= Buffer.Length;
        private void FillBuffer()
        {
            if (EndOfStreamReached)
                throw new InvalidOperationException("Cannot fill the buffer when the end of stream is reached");

            CurrentBufferIndex = 0;
            int charsRead = Reader.ReadBlock(Buffer, CurrentBufferIndex, Buffer.Length);
            if (charsRead < Buffer.Length)
            {
                EndOfStreamReached = true;
                FinalBufferBlockSize = charsRead;
            }
        }

        private void CheckTail()
        {
            FillTailIfNecessary();
            ChopTailIfNecessary();
        }
        private void FillTailIfNecessary()
        {
            if (IsTailEligible)
                FillTail();
        }
        private bool IsTailEligible => !EndOfStreamReached && CurrentBufferIndex >= Buffer.Length - TailSizeInChars;
        private void FillTail()
        {
            PreviousBufferTail = new char[TailSizeInChars];
            for (int i = 0; i < TailSizeInChars; i++)
                PreviousBufferTail[i] = AdvanceBufferInternal();
            CurrentBufferIndex = -TailSizeInChars;
        }
        private void ChopTailIfNecessary()
        {
            if (PreviousBufferTail != null && CurrentBufferIndex >= 0)
                PreviousBufferTail = null;
        }
    }
}
