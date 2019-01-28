using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain.Parsing.CSV;
using System.IO;

namespace Curds.Infrastructure.Parsing.CSV
{
    public abstract class ParsingCase
    {
        public static Stream PrepareTextStream(string source, Encoding textEncoding) => new MemoryStream(textEncoding.GetBytes(source));

        public Stream Source { get; }
        public abstract ReadingOptions Options { get; }
        public abstract int ExpectedRowCount { get; }

        public ParsingCase(Stream source)
        {
            Source = source;
        }

        public abstract int ExpectedRowLength(int rowIndex);
        public abstract void VerifyContent(int rowIndex, List<Cell> cells);
    }
}
