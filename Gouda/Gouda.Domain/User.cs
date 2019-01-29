using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain.Persistence;

namespace Gouda.Domain
{
    public class User : NamedEntity
    {
        public override Entity Clone()
        {
            User clone = new User();
            clone.ID = ID;
            clone.Name = Name;

            return clone;
        }
    }
}
