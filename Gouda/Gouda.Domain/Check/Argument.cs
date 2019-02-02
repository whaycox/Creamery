using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain.Persistence;
using System.Linq;

namespace Gouda.Domain.Check
{
    public class Argument : NamedEntity
    {
        public string Value { get; set; }

        public override Entity Clone() => CloneInternal(new Argument());
        protected Argument CloneInternal(Argument clone)
        {
            clone.Value = Value;
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
                if (Value != null)
                    toReturn = IncrementHashCode(toReturn, Value.GetHashCode());

                return toReturn;
            }
        }
        public override bool Equals(object obj)
        {
            Argument toTest = obj as Argument;
            if (toTest == null)
                return false;
            if (!toTest.Value.CompareWithNull(Value))
                return false;
            return base.Equals(obj);
        }
    }
}
