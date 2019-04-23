using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Domain.Persistence
{
    public abstract class Entity : BaseEntity
    {
        public int ID { get; set; }
    }
}
