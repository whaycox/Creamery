using Curds.Domain.Parsing.CSV;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Curds.Infrastructure.Parsing.CSV.Tests.Cases.TextParser
{
    public class CustomSeparator : ParsingCase
    {
        private const char WeirdSeparator = 'þ';
        private const char WeirdQualifier = 'ÿ';
        
        public static CustomSeparator DelimiterOnly => new CustomSeparator(DelimiterOnlyCSV, DelimiterOptions);
        private const string DelimiterOnlyCSV = "FirstValue,ÿSecoþndÿÿValueÿ,ÿThird\r\nValueÿ,";
        private static ReadingOptions DelimiterOptions => new ReadingOptions() { Qualifier = WeirdQualifier };

        public static CustomSeparator SeparatorOnly => new CustomSeparator(SeparatorOnlyCSV, SeparatorOptions);
        private const string SeparatorOnlyCSV = "FirstValueþ\"SecoþndÿValue\"þ\"Third\r\nValue\"þ";
        private static ReadingOptions SeparatorOptions => new ReadingOptions() { Separator = WeirdSeparator };

        public static CustomSeparator Both => new CustomSeparator(BothCSV, BothOptions);
        private const string BothCSV = "FirstValueþÿSecoþndÿÿValueÿþÿThird\r\nValueÿþ";
        private static ReadingOptions BothOptions => new ReadingOptions() { Separator = WeirdSeparator, Qualifier = WeirdQualifier };

        public override ReadingOptions Options { get; }

        public override int ExpectedRowCount => 1;

        public CustomSeparator(string source, ReadingOptions options)
            : base(PrepareTextStream(source, Encoding.Default))
        {
            Options = options;
        }

        public override int ExpectedRowLength(int rowIndex) => 4;

        public override void VerifyContent(int rowIndex, List<Cell> cells)
        {
            for (int i = 0; i < cells.Count; i++)
                VerifyCell(i, cells[i]);
        }
        private void VerifyCell(int cellIndex, Cell parsedCell)
        {
            switch (cellIndex)
            {
                case 0:
                    Assert.AreEqual("FirstValue", parsedCell.Value);
                    break;
                case 1:
                    Assert.AreEqual("SecoþndÿValue", parsedCell.Value);
                    break;
                case 2:
                    Assert.AreEqual("Third\r\nValue", parsedCell.Value);
                    break;
                case 3:
                    Assert.AreEqual(string.Empty, parsedCell.Value);
                    break;
                default:
                    throw new InvalidOperationException("Unrecognized cell");
            }
        }
    }
}
