using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Curds.Parsing.CSV.Domain
{
    public class Row : IEnumerable<Cell>
    {
        private List<Cell> Cells = new List<Cell>();

        public Row(IEnumerable<Cell> cells = null)
        {
            if (cells != null)
                Cells.AddRange(cells);
        }

        public List<Cell> RetrieveCells() => Cells.ToList();

        public IEnumerator<Cell> GetEnumerator()
        {
            for (int i = 0; i < Cells.Count; i++)
                yield return Cells[i];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < Cells.Count; i++)
            {
                if (i != 0)
                    builder.Append(',');
                builder.Append(Cells[i].ToString());
            }
            return builder.ToString();
        }
    }
}
