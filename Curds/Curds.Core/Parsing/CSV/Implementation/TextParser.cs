using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Curds.Parsing.CSV.Implementation
{
    using Abstraction;
    using Domain;

    public class TextParser : ICSVParser
    {
        private ReadingOptions Options { get; }

        public TextParser()
            : this(new ReadingOptions())
        { }

        public TextParser(ReadingOptions options)
        {
            Options = options;
        }

        public IEnumerable<Row> Parse(Stream stream) => ParseInternal(new CSVInterpreter(stream, Options));

        private IEnumerable<Row> ParseInternal(CSVInterpreter interpreter)
        {
            while (!interpreter.IsEmpty)
                yield return ParseRow(interpreter);
        }

        private Row ParseRow(CSVInterpreter interpreter)
        {
            List<Cell> cells = new List<Cell>();
            while (!interpreter.IsEmpty && !interpreter.IsAtNewLine)
            {
                FinishPreviousCell(interpreter, cells);
                cells.Add(ParseCell(interpreter));
            }
            FinishRow(interpreter);

            return new Row(cells);
        }

        private void FinishPreviousCell(CSVInterpreter interpreter, List<Cell> cells)
        {
            if (cells.Any())
            {
                if (!interpreter.IsAtSeparator)
                    throw new FormatException("Did not find a separator in between cells");

                ThrowAwayOne(interpreter);
            }
        }
        private void ThrowAwayOne(CSVInterpreter interpreter) => interpreter.Read();
        private void FinishRow(CSVInterpreter interpreter)
        {
            if (!interpreter.IsEmpty)
            {
                if (!interpreter.IsAtNewLine)
                    throw new FormatException("Did not find a New Line in between rows");

                ThrowAwayOne(interpreter);
            }
        }

        private Cell ParseCell(CSVInterpreter interpreter)
        {
            if (interpreter.IsAtQualifier)
                return new Cell(ReadQualifiedCell(interpreter));
            else
                return new Cell(ReadUnqualifiedCell(interpreter));
        }

        private string ReadQualifiedCell(CSVInterpreter interpreter)
        {
            if (!interpreter.IsAtQualifier)
                throw new InvalidOperationException($"A qualified cell must begin with the qualifier {interpreter.Qualifier}");
            ThrowAwayOne(interpreter); //We don't need the qualifiers themselves

            StringBuilder cellBuilder = new StringBuilder();
            while (!interpreter.IsEmpty && !interpreter.IsAtQualifiedEnding)
            {
                if (interpreter.IsAtEscapedQualifier)
                    ThrowAwayOne(interpreter);
                cellBuilder.Append(interpreter.Read());
            }

            if (!interpreter.IsEmpty)
                ThrowAwayOne(interpreter); //We don't need the qualifiers themselves
            return cellBuilder.ToString();
        }

        private string ReadUnqualifiedCell(CSVInterpreter interpreter)
        {
            StringBuilder cellBuilder = new StringBuilder();
            while (!interpreter.IsEmpty && !interpreter.IsAtNewLine && !interpreter.IsAtSeparator)
                cellBuilder.Append(interpreter.Read());

            return cellBuilder.ToString();
        }
    }
}
