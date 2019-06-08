using System.Collections.Generic;

namespace Curds.Parsing.CSV.Mock
{
    public class Row : Domain.Row
    {
        private static readonly List<Cell> Cells = new List<Cell>()
        {
            { new Cell() },
            { new Cell() },
            { new Cell() },
            { new Cell() },
            { new Cell() },
        };
        public static int ExpectedLength => Cells.Count;

        public Row()
            : base(Cells)
        { }
    }
}
