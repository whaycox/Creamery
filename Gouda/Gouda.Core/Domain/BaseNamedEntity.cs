using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Domain
{
    public abstract class BaseNamedEntity : BaseEntity
    {
        public string Name { get; set; }
    }
}
