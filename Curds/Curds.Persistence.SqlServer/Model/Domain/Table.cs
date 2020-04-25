using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Curds.Persistence.Model.Domain
{
    public class Table
    {
        public string Schema { get; set; }
        public string Name { get; set; }

        public List<Column> Columns { get; set; } = new List<Column>();
        public Column IdentityColumn => Columns.FirstOrDefault(column => column.IsIdentity);
        public IEnumerable<Column> IdentityColumns => Columns.Where(column => column.IsIdentity);
        public IEnumerable<Column> NotIdentityColumns => Columns.Where(column => !column.IsIdentity);
    }
}
