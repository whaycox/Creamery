using Curds.Domain.Persistence;
using System.Collections.Generic;
using System.Linq;
using Curds.Domain;

namespace Gouda.Domain.Check
{
    public class Argument : NamedEntity
    {
        public int DefinitionID { get; set; }
        public string Value { get; set; }

        public override Entity Clone() => CloneInternal(new Argument());
        protected Argument CloneInternal(Argument clone)
        {
            clone.DefinitionID = DefinitionID;
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
                toReturn = IncrementHashCode(toReturn, DefinitionID.GetHashCode());

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
            if (toTest.DefinitionID != DefinitionID)
                return false;
            if (!toTest.Value.CompareWithNull(Value))
                return false;
            return base.Equals(obj);
        }

        public static Dictionary<string, string> Compile(IEnumerable<Argument> arguments) => arguments?.ToDictionary(k => k.Name, v => v.Value) ?? new Dictionary<string, string>();
    }
}
