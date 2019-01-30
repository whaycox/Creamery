using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Domain.Persistence
{
    public abstract class Entity
    {
        protected const int HashPrime1 = 37;
        protected const int HashPrime2 = 397;

        public int ID { get; set; }

        public abstract Entity Clone();
        protected Entity CloneInternal(Entity clone)
        {
            clone.ID = ID;
            return clone;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int toReturn = StartHashCode();

                toReturn = PrimeHashCode(toReturn);
                toReturn = IncrementHashCode(toReturn, ID.GetHashCode());

                return toReturn;
            }
        }
        protected int StartHashCode() => HashPrime1;
        protected int PrimeHashCode(int currentHashCode) => unchecked(currentHashCode * HashPrime2);
        protected int IncrementHashCode(int currentHashCode, int incrementValue) => unchecked(currentHashCode + incrementValue);


        public override bool Equals(object obj)
        {
            Entity toTest = obj as Entity;
            if (toTest == null)
                return false;
            return toTest.ID == ID;
        }
    }
}
