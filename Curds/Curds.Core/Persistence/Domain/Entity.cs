using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Domain
{
    public abstract class Entity : BaseEntity
    {
        public int ID { get; set; }
    }
}
