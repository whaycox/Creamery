using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Domain.Persistence
{
    public abstract class NamedEntity : Entity
    {
        public string Name { get; set; }

        protected NamedEntity CloneInternal(NamedEntity clone)
        {
            clone.Name = Name;
            base.CloneInternal(clone);
            return clone;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int toReturn = StartHashCode();

                toReturn = PrimeHashCode(toReturn);
                toReturn = IncrementHashCode(toReturn, base.GetHashCode());

                toReturn = PrimeHashCode(toReturn);
                if (Name != null)
                    toReturn = IncrementHashCode(toReturn, Name.GetHashCode());

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
