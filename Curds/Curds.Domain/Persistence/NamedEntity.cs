using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Domain.Persistence
{
    public abstract class NamedEntity : Entity
    {
        public string Name { get; set; }

        public override int GetHashCode()
        {
            unchecked
            {
                int toReturn = HashPrime1;

                toReturn *= HashPrime2;
                toReturn += base.GetHashCode();

                toReturn *= HashPrime2;
                if (Name != null)
                    toReturn += Name.GetHashCode();

                return toReturn;
            }
        }
        public override bool Equals(object obj)
        {
            NamedEntity toTest = obj as NamedEntity;
            if (toTest == null)
                return false;
            if (!toTest.Name.CompareWithNull(Name))
                return false;
            return base.Equals(obj);
        }
    }
}
