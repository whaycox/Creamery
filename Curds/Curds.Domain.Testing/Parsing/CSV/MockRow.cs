using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Domain.Parsing.CSV
{
    public class MockRow : Row
    {
        private static readonly List<Cell> Cells = new List<Cell>()
        {
            { new MockCell() },
            { new MockCell() },
            { new MockCell() },
            { new MockCell() },
            { new MockCell() },
        };
        public static int ExpectedLength => Cells.Count;

        public MockRow()
            : base(Cells)
        { }
    }
}
