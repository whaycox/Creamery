using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Curds.Domain.Parsing.CSV;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Infrastructure.Parsing.CSV.Tests.Cases.TextParser
{
    using TestFiles;

    public class Large : ParsingCase
    {
        private const int ExpectedWidth = 15;

        private int[] Totals = new int[ExpectedWidth];

        public override ReadingOptions Options => null;

        public override int ExpectedRowCount => 1003;

        public Large()
            : base(Files.PrepareStream(Files.LargeCSV))
        { }

        public override int ExpectedRowLength(int rowIndex) => ExpectedWidth;

        public override void VerifyContent(int rowIndex, List<Cell> cells)
        {
            if (rowIndex < RowsToSum)
                AddRow(Totals, cells);
            else if (rowIndex == SumRowIndex)
                CompareSums(Totals, cells);
        }
        private const int RowsToSum = 1000;
        private void AddRow(int[] totalArray, List<Cell> rowCells)
        {
            for (int i = 0; i < rowCells.Count; i++)
                totalArray[i] += ConvertCell(rowCells[i]);
        }
        private int ConvertCell(Cell parsedCell) => int.Parse(parsedCell.Value);
        private int SumRowIndex => ExpectedRowCount - 1;
        private void CompareSums(int[] csvTotals, List<Cell> sumRow)
        {
            for (int i = 0; i < csvTotals.Length; i++)
                Assert.AreEqual(ConvertCell(sumRow[i]), csvTotals[i]);
        }
    }
}
