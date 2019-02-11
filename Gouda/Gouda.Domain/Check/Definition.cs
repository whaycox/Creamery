using Curds.Domain;
using Curds.Domain.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gouda.Domain.Check
{
    using Enumerations;

    public class Definition : NamedEntity
    {
        public Status Status { get; set; }
        public int SatelliteID { get; set; }
        public List<int> ArgumentIDs { get; set; }
        public Guid CheckID { get; set; }
        public TimeSpan RescheduleSpan { get; set; }

        public Definition()
        {
            ArgumentIDs = new List<int>();
        }

        public override Entity Clone() => CloneInternal(new Definition());
        protected Definition CloneInternal(Definition clone)
        {
            clone.SatelliteID = SatelliteID;
            clone.Status = Status;
            clone.CheckID = CheckID;
            clone.RescheduleSpan = RescheduleSpan;
            clone.ArgumentIDs = ArgumentIDs.ToList();
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

                toReturn = PrimeHashCode(toReturn);
                toReturn = IncrementHashCode(toReturn, CheckID.GetHashCode());

                toReturn = PrimeHashCode(toReturn);
                toReturn = IncrementHashCode(toReturn, RescheduleSpan.GetHashCode());

                if (ArgumentIDs != null)
                {
                    toReturn = PrimeHashCode(toReturn);
                    toReturn = IncrementHashCode(toReturn, ArgumentIDs.Count.GetHashCode());
                    foreach (int argumentID in ArgumentIDs)
                    {
                        toReturn = PrimeHashCode(toReturn);
                        toReturn = IncrementHashCode(toReturn, argumentID.GetHashCode());
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
            if (toTest.CheckID != CheckID)
                return false;
            if (toTest.RescheduleSpan != RescheduleSpan)
                return false;
            if (!toTest.ArgumentIDs.CompareTwoLists(ArgumentIDs))
                return false;
            return base.Equals(obj);
        }
    }
}
