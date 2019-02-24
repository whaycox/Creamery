using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Domain.Persistence
{
    public abstract class NamedEntity : Entity
    {
        public string Name { get; set; }
    }
}
