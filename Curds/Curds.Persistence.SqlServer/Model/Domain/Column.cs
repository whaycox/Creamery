using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Curds.Persistence.Model.Domain
{
    public class Column
    {
        public SqlDbType SqlType { get; set; }
        public string Name { get; set; }
        public bool IsIdentity { get; set; }
    }
}
