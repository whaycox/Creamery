using System;
using System.IO;

namespace Curds.Parsing.CSV.Implementation
{
    using Abstraction;
    using Reader.Implementation;

    public class Interpreter
    {
        private CharBuffer Buffer { get; }
        private ICSVOptions Options { get; }

        public bool IsEmpty => Buffer.IsConsumed;

        private char? First => Buffer[0];
        private char? Second => Buffer[1];
        private char? Third => Buffer[2];

        public string Current => ParseCharCombo(First, Second);
        public string Next => ParseCharCombo(Second, Third);
        private string ParseCharCombo(char? first, char? second) => first == null ? null : Options.NewLine.IsNewLine(first.Value, second) ? Environment.NewLine : first.ToString();

        public bool IsAtNewLine => Current == Environment.NewLine;
        public bool NextIsNewLine => Next == Environment.NewLine;

        public char Separator => Options.Separator;
        public bool IsAtSeparator => First == null ? false : First.Value == Separator;
        public bool NextIsSeparator => Second == null ? false : Second.Value == Separator;

        public char Qualifier => Options.Qualifier;
        public bool IsAtQualifier => First == null ? false : First.Value == Qualifier;
        public bool NextIsQualifier => Second == null ? false : Second.Value == Qualifier;

        public bool IsAtEscapedQualifier => IsAtQualifier && NextIsQualifier;
        public bool IsAtQualifiedEnding => IsAtQualifier && (NextIsSeparator || NextIsNewLine || Next == null);

        public Interpreter(Stream inputStream, ICSVOptions options)
        {
            if (options.CharLookahead < 3)
                throw new ArgumentOutOfRangeException(nameof(options.CharLookahead));

            Options = options;
            Buffer = new CharBuffer(inputStream, Options);
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
                Buffer.Advance();
        }
    }
}
