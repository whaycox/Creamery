using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Query.Domain
{
    public class Table
    {
        public string Schema { get; set; }
        public string Name { get; set; }

        public List<Column> Columns { get; set; } = new List<Column>();
    }
}
