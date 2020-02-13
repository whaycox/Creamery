using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Model.Domain
{
    public class Column
    {
        public string Name { get; set; }
        public bool IsIdentity { get; set; }
    }
}
