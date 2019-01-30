using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain.Persistence;

namespace Gouda.Domain.Communication
{
    public abstract class Contact : NamedEntity, ICronEntity
    {
        public int UserID { get; set; }
        public string CronString { get; set; }

        protected Contact CloneInternal(Contact clone)
        {
            clone.UserID = UserID;
            clone.CronString = CronString;
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
                toReturn = IncrementHashCode(toReturn, UserID.GetHashCode());

                toReturn = PrimeHashCode(toReturn);
                if (CronString != null)
                    toReturn = IncrementHashCode(toReturn, CronString.GetHashCode());

                return toReturn;
            }
        }
        public override bool Equals(object obj)
        {
            Contact toTest = obj as Contact;
            if (toTest == null)
                return false;
            if (toTest.UserID != UserID)
                return false;
            if (!toTest.CronString.CompareWithNull(CronString))
                return false;
            return base.Equals(obj);
        }
    }
}
