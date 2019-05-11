using System;
using System.IO;

namespace Curds.Parsing.CSV.Implementation
{
    internal class CSVInterpreter
    {
        private ReadingOptions Options { get; }
        private ReadingBuffer Buffer { get; }

        public bool IsEmpty => Buffer.BufferConsumed;

        public string Current => ParseCharCombo(Buffer.First, Buffer.Next);
        public string Next => ParseCharCombo(Buffer.Next, Buffer.Third);
        private string ParseCharCombo(char? first, char? second) => first == null ? null : Options.NewLine.IsNewLine(first.Value, second) ? Environment.NewLine : first.ToString();

        public bool IsAtNewLine => Current == Environment.NewLine;
        public bool NextIsNewLine => Next == Environment.NewLine;

        public char Separator => Options.Separator;
        public bool IsAtSeparator => Buffer.First == null ? false : Buffer.First.Value == Separator;
        public bool NextIsSeparator => Buffer.Next == null ? false : Buffer.Next.Value == Separator;

        public char Qualifier => Options.Qualifier;
        public bool IsAtQualifier => Buffer.First == null ? false : Buffer.First.Value == Qualifier;
        public bool NextIsQualifier => Buffer.Next == null ? false : Buffer.Next.Value == Qualifier;

        public bool IsAtEscapedQualifier => IsAtQualifier && NextIsQualifier;
        public bool IsAtQualifiedEnding => IsAtQualifier && (NextIsSeparator || NextIsNewLine || Next == null);

        public CSVInterpreter(Stream source, ReadingOptions options)
        {
            Options = options ?? throw new ArgumentNullException(nameof(options));
            Buffer = new ReadingBuffer(source, Options.Encoding);
        }

        public string Read()
        {
            string toReturn = Current;
            if (IsAtNewLine)
                ConsumeNChars(Options.NewLine.NewLineLength);
            else
                ConsumeNChars();
            return toReturn;
        }
        private void ConsumeNChars(int numberToConsume = 1)
        {
            for (int i = 0; i < numberToConsume; i++)
                Buffer.AdvanceReadBuffer();
        }
    }
}
