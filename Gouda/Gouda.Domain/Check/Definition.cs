using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain.Persistence;
using System.Linq;

namespace Gouda.Domain.Check
{
    using Enumerations;
    using Communication;

    public abstract class Definition : NamedEntity
    {
        public Status Status { get; private set; }
        public int SatelliteID { get; set; }
        public List<Argument> Arguments { get; set; }

        public Request Request => new Request(Name, CompileArguments(Arguments));
        private Dictionary<string, string> CompileArguments(List<Argument> arguments) => arguments.ToDictionary(k => k.Name, v => v.Value);

        public void Update(Status newStatus) => Status = newStatus;

        public override Entity Clone() => CloneInternal(Default);
        protected abstract Definition Default { get; }
        protected Definition CloneInternal(Definition clone)
        {
            clone.SatelliteID = SatelliteID;
            clone.Status = Status;
            clone.Arguments = Arguments?.Select(a => a.Clone() as Argument).ToList();
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
                toReturn = IncrementHashCode(toReturn, Status.GetHashCode());

                toReturn = PrimeHashCode(toReturn);
                toReturn = IncrementHashCode(toReturn, SatelliteID.GetHashCode());

                if (Arguments != null)
                {
                    toReturn = PrimeHashCode(toReturn);
                    toReturn = IncrementHashCode(toReturn, Arguments.Count.GetHashCode());
                    foreach (Argument argument in Arguments)
                    {
                        toReturn = PrimeHashCode(toReturn);
                        toReturn = IncrementHashCode(toReturn, argument.GetHashCode());
                    }
                }

                return toReturn;
            }
        }

        public override bool Equals(object obj)
        {
            Definition toTest = obj as Definition;
            if (toTest == null)
                return false;
            if (toTest.Status != Status)
                return false;
            if (toTest.SatelliteID != SatelliteID)
                return false;
            if (!toTest.Arguments.CompareTwoLists(Arguments))
                return false;
            return base.Equals(obj);
        }

        public abstract Status Evaluate(Response response);
    }

    public abstract class Definition<T> : Definition where T : Response
    {
        public sealed override Status Evaluate(Response response) => Evaluate(BuildResponse(response));

        public abstract T BuildResponse(Response response);
        protected abstract Status Evaluate(T response);
    }
}
