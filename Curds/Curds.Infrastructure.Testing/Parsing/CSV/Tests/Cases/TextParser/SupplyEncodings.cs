using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain.Parsing.CSV;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Infrastructure.Parsing.CSV.Tests.Cases.TextParser
{
    using TestFiles;

    public class SupplyEncodings : ParsingCase
    {
        private const int ANSICodePage = 1252;
        private static Encoding ANSIEncoding => CodePagesEncodingProvider.Instance.GetEncoding(ANSICodePage);
        public static SupplyEncodings ANSI => new SupplyEncodings(Files.ANSICSV, ANSIEncoding);

        private static Encoding UTF8Encoding => Encoding.UTF8;
        public static SupplyEncodings UTF8 => new SupplyEncodings(Files.UTF8CSV, UTF8Encoding);

        private static Encoding UCS2BEEncoding => Encoding.BigEndianUnicode;
        public static SupplyEncodings UCS2BE => new SupplyEncodings(Files.UCS2BECSV, UCS2BEEncoding);

        private static Encoding UCS2LEEncoding => Encoding.Unicode;
        public static SupplyEncodings UCS2LE => new SupplyEncodings(Files.UCS2LECSV, UCS2LEEncoding);

        private ReadingOptions OptionsFromEncoding(Encoding textEncoding) => new ReadingOptions() { Encoding = textEncoding };

        public override ReadingOptions Options { get; }

        public override int ExpectedRowCount => 3;

        public SupplyEncodings(string fileName, Encoding textEncoding)
            : base(Files.PrepareStream(fileName))
        {
            Options = OptionsFromEncoding(textEncoding);
        }

        public override int ExpectedRowLength(int rowIndex)
        {
            switch (rowIndex)
            {
                case 0:
                case 1:
                    return 4;
                case 2:
                    return 1;
                default:
                    throw new InvalidOperationException("Unrecognized row");
            }
        }

        public override void VerifyContent(int rowIndex, List<Cell> cells)
        {
            switch (rowIndex)
            {
                case 0:
                    Assert.AreEqual("Test utilizing some extended chars", cells[0].Value);
                    Assert.AreEqual("þýüûúùøAÿ", cells[1].Value);
                    Assert.AreEqual("þýüûúùøAÿ", cells[2].Value);
                    Assert.AreEqual("þýüûúùøAÿ", cells[3].Value);
                    break;
                case 1:
                    Assert.AreEqual("þýüûúùøAÿ", cells[0].Value);
                    Assert.AreEqual("þýüûúùøAÿ", cells[1].Value);
                    Assert.AreEqual("þýüûúùøAÿ", cells[2].Value);
                    Assert.AreEqual(string.Empty, cells[3].Value);
                    break;
                case 2:
                    Assert.AreEqual("þýüûúùøAÿþýüûúùøAÿþýüûúùøAÿþýüûúùøAÿ", cells[0].Value);
                    break;
                default:
                    throw new InvalidOperationException("Unrecognized row");
            }
        }
    }
}
