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

        public override int GetHashCode()
        {
            unchecked
            {
                int toReturn = HashPrime1;

                toReturn *= HashPrime2;
                toReturn += ID.GetHashCode();

                return toReturn;
            }
        }

        public override bool Equals(object obj)
        {
            Entity toTest = obj as Entity;
            if (toTest == null)
                return false;
            return toTest.ID == ID;
        }
    }
}
