using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Query.Domain
{
    using Persistence.Domain;

    public class ValueEntity
    {
        public List<Value> Values { get; set; } = new List<Value>();
    }
}
